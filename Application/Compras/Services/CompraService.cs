using Application.Compras.Dto;
using Application.Compras.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Compras.Services
{
    public class CompraService : ICompraServices
    {
        private readonly ICompraRepositorio _compraRepositorio;
        private readonly IDetalleCompraRepositorio _detalleCompraRepositorio;
        private readonly IPagoCompraCreditoRepositorio _pagoCompraCreditoRepositorio;
        private readonly IProveedorRepositorio _proveedorRepositorio;
        private readonly IMapper _mapper;

        public CompraService(
            ICompraRepositorio compraRepositorio,
            IDetalleCompraRepositorio detalleCompraRepositorio,
            IPagoCompraCreditoRepositorio pagoCompraCreditoRepositorio,
            IProveedorRepositorio ProveedorRepositorio,
            IMapper mapper)
        {
            _compraRepositorio = compraRepositorio;
            _detalleCompraRepositorio = detalleCompraRepositorio;
            _pagoCompraCreditoRepositorio = pagoCompraCreditoRepositorio;
            _proveedorRepositorio = ProveedorRepositorio;
            _mapper = mapper;
        }


        public async Task<PaginadoResponse<CompraDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _compraRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<CompraDto>>(response.Data);

            return new PaginadoResponse<CompraDto>(data, response.Meta);
        }

        public async Task<OperationResult<CompraDto>> CreateAsync(CompraSaveDto saveDto)
        {
            var compra = _mapper.Map<Compra>(saveDto);

            compra.FechaCreacion = DateTime.Now;

            await _compraRepositorio.SaveAsync(compra);

            return new OperationResult<CompraDto>()
            {
                Data = _mapper.Map<CompraDto>(compra),
                Message = "Creado con Exito",
            };
        }

        public async Task<OperationResult<CompraDto>> DisabledAsync(int id)
        {
            var compra = await _compraRepositorio.FindByIdAsync(id);
            if (compra == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<CompraDto>()
            {
                Data = _mapper.Map<CompraDto>(compra),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<CompraDto>> EditAsync(int id, CompraSaveDto saveDto)
        {
            var compra = await _compraRepositorio.FindByIdAsync(id);

            if (compra == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            _mapper.Map(saveDto, compra);

            await _compraRepositorio.SaveAsync(compra);

            return new OperationResult<CompraDto>()
            {
                Data = _mapper.Map<CompraDto>(compra),
                Message = "actualizado con exito",
            };

        }

        public async Task<IReadOnlyList<CompraDto>> FindAllAsync()
        {
            var response = await _compraRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<CompraDto>>(response);
        }

        public async Task<CompraDto> FindByIdAsync(int id)
        {
            var response = await _compraRepositorio.FindByIdAsync(id);

            return _mapper.Map<CompraDto>(response);
        }

        public async Task<IReadOnlyList<CompraSelectDto>> SelectActivo()
        {
            var response = await _compraRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<CompraSelectDto>>(response);
        }

        public async Task<OperationResult<List<CompraDto>>> FindByProveedorIdAsync(int proveedorId)
        {
            var compras = await _compraRepositorio.FindByProveedorIdAsync(proveedorId);

            if (compras == null || !compras.Any())
            {
                return new OperationResult<List<CompraDto>>
                {
                    Data = new List<CompraDto>(),
                    Message = $"No existen compras registradas para el proveedor con Id {proveedorId}"
                };
            }

            return new OperationResult<List<CompraDto>>
            {
                Data = _mapper.Map<List<CompraDto>>(compras),
                Message = "Compras encontradas"
            };
        }

        public async Task<OperationResult<CompraDto>> CreateWithDetailsAsync(CompraCompletoSaveDto saveDto)
        {
            // ---------------------------------------------------------
            // 🔍 Validar proveedor
            // ---------------------------------------------------------
            var proveedor = await _proveedorRepositorio.FindByIdAsync(saveDto.IdProveedor);
            if (proveedor == null)
                throw new NotFoundCoreException("Proveedor no encontrado con el id especificado.");

            // ---------------------------------------------------------
            // 🚫 Validar que no exista otro comprobante con misma serie y número
            // ---------------------------------------------------------
            var comprobanteExistente = await _compraRepositorio.FindByNumeroComprobanteAsync(
                saveDto.Serie,
                saveDto.Numero,
                saveDto.IdTipoComprobante,
                saveDto.IdCompra == 0 ? null : saveDto.IdCompra);


            if (comprobanteExistente != null)
            {
                throw new NotFoundCoreException(
                    $"Ya existe una compra registrada con el comprobante {saveDto.Serie}-{saveDto.Numero}."
                );
            }

            Compra compra;

            // ---------------------------------------------------------
            // 🟢 Si IdCompra == 0 → Crear nueva
            // ---------------------------------------------------------
            if (saveDto.IdCompra == 0)
            {
                compra = _mapper.Map<Compra>(saveDto);
                compra.FechaEmision = saveDto.FechaEmision ?? DateTime.Now;
                compra.FechaCreacion = DateTime.Now;
                compra.UsuarioCreacion = saveDto.UsuarioCreacion;

                await _compraRepositorio.SaveAsync(compra);
            }
            // ---------------------------------------------------------
            // 🟠 Si IdCompra existe → Editar
            // ---------------------------------------------------------
            else
            {
                compra = await _compraRepositorio.FindByIdAsync(saveDto.IdCompra);
                if (compra == null)
                    throw new NotFoundCoreException($"Compra con id {saveDto.IdCompra} no encontrada.");

                compra.IdTipoComprobante = saveDto.IdTipoComprobante;
                compra.Serie = saveDto.Serie;
                compra.Numero = saveDto.Numero;
                compra.FechaEmision = saveDto.FechaEmision ?? compra.FechaEmision;
                compra.IdProveedor = saveDto.IdProveedor;
                compra.FormaPago = saveDto.FormaPago;
                compra.TipoMoneda = saveDto.TipoMoneda;
                compra.Observacion = saveDto.Observacion;
                compra.SubTotal = saveDto.SubTotal;
                compra.Descuentos = saveDto.Descuentos;
                compra.ValorCompra = saveDto.ValorCompra;
                compra.Igv = saveDto.Igv;
                compra.ImporteTotal = saveDto.ImporteTotal;
                compra.FechaModificacion = DateTime.Now;
                compra.UsuarioModificacion = saveDto.UsuarioModificacion;

                await _compraRepositorio.SaveAsync(compra);

                // 🔥 Limpiar detalles y pagos anteriores (solo si hay)
                await _detalleCompraRepositorio.DeleteByCompraIdAsync(compra.IdCompra);
                await _pagoCompraCreditoRepositorio.DeleteByCompraIdAsync(compra.IdCompra);
            }

            // ---------------------------------------------------------
            // 🧾 Guardar detalles
            // ---------------------------------------------------------
            if (saveDto.Detalles != null && saveDto.Detalles.Any())
            {
                foreach (var det in saveDto.Detalles)
                {
                    var detalle = new DetalleCompra
                    {
                        IdCompra = compra.IdCompra,
                        Cantidad = det.Cantidad,
                        UnidadMedida = det.UnidadMedida,
                        Descripcion = det.Descripcion,
                        ValorUnitario = det.ValorUnitario,
                        ValorTotal = det.ValorTotal,
                        FechaCreacion = DateTime.Now
                    };

                    await _detalleCompraRepositorio.SaveAsync(detalle);
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
                    var pagoCredito = new PagoCompraCredito
                    {
                        IdCompra = compra.IdCompra,
                        FechaVencimiento = pago.FechaVencimiento ?? DateTime.Now,
                        MontoCuota = pago.MontoCuota,
                        EstadoPago = "PENDIENTE",
                        FechaCreacion = DateTime.Now
                    };

                    await _pagoCompraCreditoRepositorio.SaveAsync(pagoCredito);
                }
            }

            // ---------------------------------------------------------
            // 🟩 Retornar resultado
            // ---------------------------------------------------------
            return new OperationResult<CompraDto>
            {
                Data = _mapper.Map<CompraDto>(compra),
                Success = true,
                Message = saveDto.IdCompra == 0
                    ? "Compra registrada correctamente."
                    : "Compra actualizada correctamente."
            };
        }

    }
}


