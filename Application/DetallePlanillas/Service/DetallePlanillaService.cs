using Application.DetallePlanillas.Dto;
using Application.DetallePlanillas.Services.Interfaces;
using Application.Exceptions;
using Application.Permissions.Dto;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.DetallePlanillas.Service
{
    public class DetallePlanillaService : IDetallePlanillaServices
    {
        private readonly IDetallePlanillaRepositorio _detallePlanillaRepositorio;
        private readonly IMapper _mapper;

        public DetallePlanillaService(IDetallePlanillaRepositorio DetallePlanillaRepositorio, IMapper mapper)
        {
            _detallePlanillaRepositorio = DetallePlanillaRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<DetallePlanillaDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _detallePlanillaRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<DetallePlanillaDto>>(response.Data);

            return new PaginadoResponse<DetallePlanillaDto>(data, response.Meta);
        }


        public async Task<OperationResult<DetallePlanillaDto>> CreateAsync(DetallePlanillaSaveDto saveDto)
        {
            var detallePlanilla = _mapper.Map<DetallePlanilla>(saveDto);

            await _detallePlanillaRepositorio.SaveAsync(detallePlanilla);

            return new OperationResult<DetallePlanillaDto>()
            {
                Data = _mapper.Map<DetallePlanillaDto>(detallePlanilla),
                Message = "Creado con Exito",
            };
        }

        public async Task<OperationResult<DetallePlanillaDto>> DisabledAsync(int id)
        {
            var detallePlanilla = await _detallePlanillaRepositorio.FindByIdAsync(id);
            if (detallePlanilla == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<DetallePlanillaDto>()
            {
                Data = _mapper.Map<DetallePlanillaDto>(detallePlanilla),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<DetallePlanillaDto>> EditAsync(int id, DetallePlanillaSaveDto saveDto)
        {
            var detallePlanilla = await _detallePlanillaRepositorio.FindByIdAsync(id);

            if (detallePlanilla == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            _mapper.Map(saveDto, detallePlanilla);

            await _detallePlanillaRepositorio.SaveAsync(detallePlanilla);

            return new OperationResult<DetallePlanillaDto>()
            {
                Data = _mapper.Map<DetallePlanillaDto>(detallePlanilla),
                Message = "Actualizado con exito",
            };

        }

        public async Task<IReadOnlyList<DetallePlanillaDto>> FindAllAsync()
        {
            var response = await _detallePlanillaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DetallePlanillaDto>>(response);
        }

        public async Task<DetallePlanillaDto> FindByIdAsync(int id)
        {
            var response = await _detallePlanillaRepositorio.FindByIdAsync(id);

            return _mapper.Map<DetallePlanillaDto>(response);
        }

        public async Task<OperationResult<List<DetallePlanillaDto>>> ObtenerPorPlanillaAsync(int id)
        {
            var response = await _detallePlanillaRepositorio.ObtenerPorPlanillaAsync(id);

            if (response == null || !response.Any())
            {
                return new OperationResult<List<DetallePlanillaDto>>
                {
                    Data = new List<DetallePlanillaDto>(),
                    Message = $"No existen  datos registrados para la compra con Id {id}"
                };
            }

            return new OperationResult<List<DetallePlanillaDto>>
            {
                Data = _mapper.Map<List<DetallePlanillaDto>>(response),
                Message = "Datos no encontrados"
            };
        }

        public async Task<OperationResult<List<DetallePlanillaDto>>> ObtenerPorTrabajadorProyectoAsync(int id)
        {
            var response = await _detallePlanillaRepositorio.ObtenerPorTrabajadorProyectoAsync(id);

            if (response == null || !response.Any())
            {
                return new OperationResult<List<DetallePlanillaDto>>
                {
                    Data = new List<DetallePlanillaDto>(),
                    Message = $"No existen  datos registrados para la compra con Id {id}"
                };
            }

            return new OperationResult<List<DetallePlanillaDto>>
            {
                Data = _mapper.Map<List<DetallePlanillaDto>>(response),
                Message = "Datos no encontrados"
            };
        }

        public async Task<IReadOnlyList<DetallePlanillaDto>> SelectActivo()
        {
            var response = await _detallePlanillaRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<DetallePlanillaDto>>(response);
        }

    }
}


