using Application.DetalleCompras.Dto;
using Application.DetalleCompras.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.DetalleCompras.Service
{
    public class DetalleCompraService : IDetalleCompraServices
    {
        private readonly IDetalleCompraRepositorio _detalleCompraRepositorio;
        private readonly IMapper _mapper;

        public DetalleCompraService(IDetalleCompraRepositorio DetalleCompraRepositorio, IMapper mapper)
        {
            _detalleCompraRepositorio = DetalleCompraRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<DetalleCompraDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _detalleCompraRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<DetalleCompraDto>>(response.Data);

            return new PaginadoResponse<DetalleCompraDto>(data, response.Meta);
        }
        

        public async Task<OperationResult<DetalleCompraDto>> CreateAsync(DetalleCompraSaveDto saveDto)
        {
            var detalleCompra = _mapper.Map<Domain.DetalleCompra>(saveDto);

            await _detalleCompraRepositorio.SaveAsync(detalleCompra);

            return new OperationResult<DetalleCompraDto>()
            {
                Data = _mapper.Map<DetalleCompraDto>(detalleCompra),
                Message = "Creado con Exito",
            };
        }

        public async Task<OperationResult<DetalleCompraDto>> DisabledAsync(int id)
        {
            var detalleCompra = await _detalleCompraRepositorio.FindByIdAsync(id);
            if (detalleCompra == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<DetalleCompraDto>()
            {
                Data = _mapper.Map<DetalleCompraDto>(detalleCompra),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<DetalleCompraDto>> EditAsync(int id, DetalleCompraSaveDto saveDto)
        {
            var detalleCompra = await _detalleCompraRepositorio.FindByIdAsync(id);

            if (detalleCompra == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            _mapper.Map(saveDto, detalleCompra);

            await _detalleCompraRepositorio.SaveAsync(detalleCompra);

            return new OperationResult<DetalleCompraDto>()
            {
                Data = _mapper.Map<DetalleCompraDto>(detalleCompra),
                Message = "Actualizado con exito",
            };

        }

        public async Task<IReadOnlyList<DetalleCompraDto>> FindAllAsync()
        {
            var response = await _detalleCompraRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DetalleCompraDto>>(response);
        }

        public async Task<DetalleCompraDto> FindByIdAsync(int id)
        {
            var response = await _detalleCompraRepositorio.FindByIdAsync(id);

            return _mapper.Map<DetalleCompraDto>(response);
        }

        public async Task<OperationResult<List<DetalleCompraDto>>> ObtenerPorCompraAsync(int id)
        {
            var response = await _detalleCompraRepositorio.ObtenerPorCompraAsync(id);

            if (response == null || !response.Any())
            {
                return new OperationResult<List<DetalleCompraDto>>
                {
                    Data = new List<DetalleCompraDto>(),
                    Message = $"No existen  datos registrados para la compra con Id {id}"
                };
            }

            return new OperationResult<List<DetalleCompraDto>>
            {
                Data = _mapper.Map<List<DetalleCompraDto>>(response),
                Message = "Datos no encontrados"
            };
        }

    }
}


