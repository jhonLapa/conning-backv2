using Application.DetalleVentas.Dto;
using Application.DetalleVentas.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.DetalleVenta.Services
{
    public class DetalleVentaService : IDetalleVentaServices
    {
        private readonly IDetalleVentaRepositorio _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public DetalleVentaService(IDetalleVentaRepositorio DetalleVentaRepositorio, IMapper mapper)
        {
            _detalleVentaRepositorio = DetalleVentaRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<DetalleVentaDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _detalleVentaRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<DetalleVentaDto>>(response.Data);

            return new PaginadoResponse<DetalleVentaDto>(data, response.Meta);
        }


        public async Task<OperationResult<DetalleVentaDto>> CreateAsync(DetalleVentaSaveDto saveDto)
        {
            var detalleVenta = _mapper.Map<Domain.DetalleVenta>(saveDto);


            await _detalleVentaRepositorio.SaveAsync(detalleVenta);

            return new OperationResult<DetalleVentaDto>()
            {
                Data = _mapper.Map<DetalleVentaDto>(detalleVenta),
                Message = "Se ha Creado",
            };

        }

        public async Task<OperationResult<DetalleVentaDto>> DisabledAsync(int id)
        {
            var detalleVenta = await _detalleVentaRepositorio.FindByIdAsync(id);
            if (detalleVenta == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<DetalleVentaDto>()
            {
                Data = _mapper.Map<DetalleVentaDto>(detalleVenta),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<DetalleVentaDto>> EditAsync(int id, DetalleVentaSaveDto saveDto)
        {
            var detalleVenta = await _detalleVentaRepositorio.FindByIdAsync(id);

            if (detalleVenta == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, detalleVenta);

            await _detalleVentaRepositorio.SaveAsync(detalleVenta);

            return new OperationResult<DetalleVentaDto>()
            {
                Data = _mapper.Map<DetalleVentaDto>(detalleVenta),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<DetalleVentaDto>> FindAllAsync()
        {
            var response = await _detalleVentaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DetalleVentaDto>>(response);
        }

        public async Task<DetalleVentaDto> FindByIdAsync(int id)
        {
            var response = await _detalleVentaRepositorio.FindByIdAsync(id);

            return _mapper.Map<DetalleVentaDto>(response);
        }

     

        public async Task<OperationResult<List<DetalleVentaDto>>> ObtenerPorVentaAsync(int id)
        {
            var response = await _detalleVentaRepositorio.ObtenerPorVentaAsync(id);

            if (response == null || !response.Any())
            {
                return new OperationResult<List<DetalleVentaDto>>
                {
                    Data = new List<DetalleVentaDto>(),
                    Message = $"No existen  datos registrados para la venta con Id {id}"
                };
            }

            return new OperationResult<List<DetalleVentaDto>>
            {
                Data = _mapper.Map<List<DetalleVentaDto>>(response),
                Message = "Datos no encontrados"
            };
        }

    }
}

