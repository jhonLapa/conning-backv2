using Application.Exceptions;
using Application.Ventas.Dto;
using Application.Ventas.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.Venta.Services
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

        public async Task<PaginadoResponse<VentaDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _ventaRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<VentaDto>>(response.Data);

            return new PaginadoResponse<VentaDto>(data, response.Meta);
        }


        public async Task<OperationResult<VentaDto>> CreateAsync(VentaSaveDto saveDto)
        {
            var venta = _mapper.Map<Domain.Venta>(saveDto);

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
            var venta = await _ventaRepositorio.FindByIdAsync(id);
            if (venta == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<VentaDto>()
            {
                Data = _mapper.Map<VentaDto>(venta),
                Message = "Se ha Desactivado",
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
            var existeCliente = await _clienteRepositorio.FindByIdAsync(saveDto.IdCliente);

            if (existeCliente == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            // Mapeamos la venta

            var venta = _mapper.Map<Domain.Venta>(saveDto);

            venta.FechaEmision = saveDto.FechaEmision ?? DateTime.Now;
            venta.FechaCreacion = DateTime.Now;
            // Guardamos la venta principal
            await _ventaRepositorio.SaveAsync(venta);

            // Guardamos los detalles
            foreach (var det in saveDto.Detalles)
            {
                var detalle = new Domain.DetalleVenta
                {
                    IdVenta = venta.IdVenta,
                    Cantidad = det.Cantidad,
                    UnidadMedida = det.UnidadMedida,
                    Descripcion = det.Descripcion,
                    ValorUnitario = det.ValorUnitario,
                    ValorTotal = det.ValorTotal,

                };

                await _detalleVentaRepositorio.SaveAsync(detalle);
            }

            // Si la forma de pago es crédito
            if (saveDto.FormaPago.ToLower() == "credito" && saveDto.PagosCredito != null)
            {
                foreach (var pago in saveDto.PagosCredito)
                {
                    var pagoCredito = new Domain.PagoVentaCredito
                    {
                        IdVenta = venta.IdVenta,
                        FechaVencimiento = pago.FechaVencimiento ?? DateTime.Now,
                        MontoCuota = pago.MontoCuota,
                        EstadoPago = "PENDIENTE", // PENDIENTE,
                        FechaCreacion = DateTime.Now

                    };

                    await _pagoVentaCreditoRepositorio.SaveAsync(pagoCredito);
                }
            }

            return new OperationResult<VentaDto>
            {
                Data = _mapper.Map<VentaDto>(venta),
                Message = "Venta registrada correctamente"
            };
        }

    }
}


