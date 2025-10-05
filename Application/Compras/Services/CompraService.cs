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
            var existeProveedor = await _proveedorRepositorio.FindByIdAsync(saveDto.IdProveedor);

            if (existeProveedor == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            // Mapeamos la compra


            var compra = _mapper.Map<Compra>(saveDto);

            compra.FechaEmision = saveDto.FechaEmision;
            compra.FechaCreacion = DateTime.Now;
            // Guardamos la compra principal
            await _compraRepositorio.SaveAsync(compra);

            // Guardamos los detalles
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

                };

                await _detalleCompraRepositorio.SaveAsync(detalle);
            }

            // Si la forma de pago es crédito
            if (saveDto.FormaPago.ToLower() == "credito" && saveDto.PagosCredito != null)
            {
                foreach (var pago in saveDto.PagosCredito)
                {
                    var pagoCredito = new Domain.PagoCompraCredito
                    {
                        IdCompra = compra.IdCompra,
                        FechaVencimiento = pago.FechaVencimiento,
                        MontoCuota = pago.MontoCuota,
                        EstadoPago = "PENDIENTE", // PENDIENTE,
                        FechaCreacion = DateTime.Now

                    };

                    await _pagoCompraCreditoRepositorio.SaveAsync(pagoCredito);
                }
            }

            return new OperationResult<CompraDto>
            {
                Data = _mapper.Map<CompraDto>(compra),
                Message = "Compra registrada correctamente"
            };
        }

    }
}


