using Application.Exceptions;
using Application.Ventas.Dto;
using Application.Ventas.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.Ventas.Servicess
{
    public class VentaService : IVentaServices
    {
        private readonly IVentaRepositorio _ventaRepositorio;
        private readonly IDetalleVentaRepositorio _detalleVentaRepositorio;
        private readonly IPagoVentaCreditoRepositorio _pagoVentaCreditoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMapper _mapper;

        public VentaService(
            IVentaRepositorio ventaRepositorio,
            IDetalleVentaRepositorio detalleVentaRepositorio,
            IPagoVentaCreditoRepositorio pagoVentaCreditoRepositorio,
            IClienteRepositorio ClienteRepositorio,
            IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _pagoVentaCreditoRepositorio = pagoVentaCreditoRepositorio;
            _clienteRepositorio = ClienteRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<VentaDto>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false)
        {
            var response = await _ventaRepositorio.BusquedaPaginado(dto, descargarTodo);

            var data = _mapper.Map<ICollection<VentaDto>>(response.Data);

            return new PaginadoResponse<VentaDto>(data, response.Meta);
        }


        public async Task<OperationResult<VentaDto>> CreateAsync(VentaSaveDto saveDto)
        {
            var venta = _mapper.Map<Venta>(saveDto);

            venta.FechaCreacion = DateTime.Now;

            await _ventaRepositorio.SaveAsync(venta);

            return new OperationResult<VentaDto>()
            {
                Data = _mapper.Map<VentaDto>(venta),
                Message = "Se ha Creado",
            };

        }


        public async Task<OperationResult<VentaDto>> DisabledAsync(int id)
        {
            var proyectoEncargado = await _ventaRepositorio.FindByIdAsync(id);

            if (proyectoEncargado == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            proyectoEncargado.Estado = proyectoEncargado.Estado == 1 ? 0 : 1;


            await _ventaRepositorio.SaveAsync(proyectoEncargado);

            return new OperationResult<VentaDto>()
            {
                Data = _mapper.Map<VentaDto>(proyectoEncargado),
                Message = proyectoEncargado.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }


        public async Task<OperationResult<VentaDto>> EditAsync(int id, VentaSaveDto saveDto)
        {
            var venta = await _ventaRepositorio.FindByIdAsync(id);

            if (venta == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, venta);

            await _ventaRepositorio.SaveAsync(venta);

            return new OperationResult<VentaDto>()
            {
                Data = _mapper.Map<VentaDto>(venta),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<VentaDto>> FindAllAsync()
        {
            var response = await _ventaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<VentaDto>>(response);
        }

        public async Task<VentaDto> FindByIdAsync(int id)
        {
            var response = await _ventaRepositorio.FindByIdAsync(id);

            return _mapper.Map<VentaDto>(response);
        }

        public async Task<IReadOnlyList<VentaSelectDto>> SelectActivo()
        {
            var response = await _ventaRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<VentaSelectDto>>(response);
        }

        public async Task<OperationResult<List<VentaDto>>> FindByClienteIdAsync(int clienteId)
        {
            var ventas = await _ventaRepositorio.FindByClienteIdAsync(clienteId);

            if (ventas == null || !ventas.Any())
            {
                return new OperationResult<List<VentaDto>>
                {
                    Data = new List<VentaDto>(),
                    Message = $"No existen ventas registradas para el cliente con Id {clienteId}"
                };
            }

            return new OperationResult<List<VentaDto>>
            {
                Data = _mapper.Map<List<VentaDto>>(ventas),
                Message = "Ventas encontradas"
            };
        }

        public async Task<OperationResult<VentaDto>> CreateWithDetailsAsync(VentaCompletoSaveDto saveDto)
        {
            // ---------------------------------------------------------
            // 🔍 Validar cliente
            // ---------------------------------------------------------
            var cliente = await _clienteRepositorio.FindByIdAsync(saveDto.IdCliente);
            if (cliente == null)
                throw new NotFoundCoreException("Cliente no encontrado con el id especificado.");

            // ---------------------------------------------------------
            // 🚫 Validar que no exista otra venta con la misma serie y número
            // ---------------------------------------------------------
            var comprobanteExistente = await _ventaRepositorio.FindByNumeroComprobanteAsync(
                saveDto.Serie,
                saveDto.Numero,
                saveDto.IdTipoComprobante,
                saveDto.IdVenta == 0 ? null : saveDto.IdVenta
            );

            if (comprobanteExistente != null)
                throw new NotFoundCoreException($"Ya existe una venta registrada con el comprobante {saveDto.Serie}-{saveDto.Numero}.");

            Venta venta;

            // ---------------------------------------------------------
            // 🟢 Si IdVenta == 0 → Crear nueva
            // ---------------------------------------------------------
            if (saveDto.IdVenta == 0)
            {
                venta = _mapper.Map<Venta>(saveDto);
                venta.FechaEmision = saveDto.FechaEmision  ;
                venta.FechaCreacion = DateTime.Now;
                venta.UsuarioCreacion = saveDto.UsuarioCreacion;

                await _ventaRepositorio.SaveAsync(venta);
            }
            // ---------------------------------------------------------
            // 🟠 Si IdVenta existe → Editar
            // ---------------------------------------------------------
            else
            {
                venta = await _ventaRepositorio.FindByIdAsync(saveDto.IdVenta);
                if (venta == null)
                    throw new NotFoundCoreException($"Venta con id {saveDto.IdVenta} no encontrada.");

                // Actualizar campos principales
                venta.IdTipoComprobante = saveDto.IdTipoComprobante;
                venta.Serie = saveDto.Serie;
                venta.Numero = saveDto.Numero;
                venta.FechaEmision = saveDto.FechaEmision ?? venta.FechaEmision;
                venta.IdCliente = saveDto.IdCliente;
                venta.FormaPago = saveDto.FormaPago;
                venta.TipoMoneda = saveDto.TipoMoneda;
                venta.Observacion = saveDto.Observacion;
                venta.SubTotal = saveDto.SubTotal;
                venta.Descuentos = saveDto.Descuentos;
                venta.ValorPago = saveDto.ValorPago;
                venta.Igv = saveDto.Igv;
                venta.ImporteTotal = saveDto.ImporteTotal;
                venta.FechaModificacion = DateTime.Now;
                venta.UsuarioModificacion = saveDto.UsuarioModificacion;
                venta.IdProyecto = saveDto.IdProyecto;
                venta.Estado = saveDto.Estado;

                await _ventaRepositorio.SaveAsync(venta);

                // 🔥 Limpiar detalles y pagos anteriores
                await _detalleVentaRepositorio.DeleteByVentaIdAsync(venta.IdVenta);
                await _pagoVentaCreditoRepositorio.DeleteByVentaIdAsync(venta.IdVenta);
            }

            // ---------------------------------------------------------
            // 🧾 Guardar detalles
            // ---------------------------------------------------------
            if (saveDto.Detalles != null && saveDto.Detalles.Any())
            {
                foreach (var det in saveDto.Detalles)
                {
                    var detalle = new DetalleVenta
                    {
                        IdVenta = venta.IdVenta,
                        Cantidad = det.Cantidad,
                        UnidadMedida = det.UnidadMedida,
                        Descripcion = det.Descripcion,
                        ValorUnitario = det.ValorUnitario,
                        ValorTotal = det.ValorTotal,
                        FechaCreacion = DateTime.Now
                    };
                    await _detalleVentaRepositorio.SaveAsync(detalle);
                }
            }

            // ---------------------------------------------------------
            // 💳 Guardar pagos crédito (solo si aplica)
            // ---------------------------------------------------------
            if (saveDto.FormaPago.Equals("credito", StringComparison.OrdinalIgnoreCase) &&
                saveDto.PagosCredito != null && saveDto.PagosCredito.Any())
            {
                foreach (var pago in saveDto.PagosCredito)
                {
                    var pagoCredito = new PagoVentaCredito
                    {
                        IdVenta = venta.IdVenta,
                        FechaVencimiento = pago.FechaVencimiento ?? DateTime.Now,
                        MontoCuota = pago.MontoCuota,
                        EstadoPago = "PENDIENTE",
                        FechaCreacion = DateTime.Now
                    };
                    await _pagoVentaCreditoRepositorio.SaveAsync(pagoCredito);
                }
            }

            // ---------------------------------------------------------
            // 🟩 Retornar resultado
            // ---------------------------------------------------------
            return new OperationResult<VentaDto>
            {
                Data = _mapper.Map<VentaDto>(venta),
                Success = true,
                Message = saveDto.IdVenta == 0
                    ? "Venta registrada correctamente."
                    : "Venta actualizada correctamente."
            };
        }


    }
}


