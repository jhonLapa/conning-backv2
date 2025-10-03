using Application.DetalleCompras.Dto;
using Application.DetalleCompras.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.DetalleCompras.Service
{
    public class DetalleCompraService : IDetalleCompraService
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
            var detalleCompra = _mapper.Map<DetalleCompra>(saveDto);

            await _detalleCompraRepositorio.SaveAsync(detalleCompra);

            return new OperationResult<DetalleCompraDto>()
            {
                Data = _mapper.Map<DetalleCompraDto>(detalleCompra),
                Message = "Creado con Exito",
                Success = true
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
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<DetalleCompraDto>> FindAllAsync()
        {
            var response = await _detalleCompraRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DetalleCompraDto>>(response);
        }

        public async Task<DetalleCompraDto> FindByIdAsync(int id)
        {
            var detalleCompra = await _detalleCompraRepositorio.FindByIdAsync(id);

            if (detalleCompra == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<DetalleCompraDto>(detalleCompra);
        }
    }
}


