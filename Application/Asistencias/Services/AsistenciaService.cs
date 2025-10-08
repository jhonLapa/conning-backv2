using Application.Exceptions;
using Application.Asistencias.Dto;
using Application.Asistencias.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Asistencias.Servicess
{
    public class AsistenciaService : IAsistenciaServices
    {
        private readonly IAsistenciaRepositorio _asistenciaRepositorio;

        private readonly IMapper _mapper;

        public AsistenciaService(
            IAsistenciaRepositorio asistenciaRepositorio,

            IMapper mapper)
        {
            _asistenciaRepositorio = asistenciaRepositorio;

            _mapper = mapper;
        }

        public async Task<PaginadoResponse<AsistenciaDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _asistenciaRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<AsistenciaDto>>(response.Data);

            return new PaginadoResponse<AsistenciaDto>(data, response.Meta);
        }


        public async Task<OperationResult<AsistenciaDto>> CreateAsync(AsistenciaSaveDto saveDto)
        {
            var asistencia = _mapper.Map<Asistencia>(saveDto);

            asistencia.FechaCreacion = DateTime.Now;

            await _asistenciaRepositorio.SaveAsync(asistencia);

            return new OperationResult<AsistenciaDto>()
            {
                Data = _mapper.Map<AsistenciaDto>(asistencia),
                Message = "Se ha Creado",
            };

        }

        public async Task<OperationResult<AsistenciaDto>> DisabledAsync(int id)
        {
            var asistencia = await _asistenciaRepositorio.FindByIdAsync(id);
            if (asistencia == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<AsistenciaDto>()
            {
                Data = _mapper.Map<AsistenciaDto>(asistencia),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<AsistenciaDto>> EditAsync(int id, AsistenciaSaveDto saveDto)
        {
            var asistencia = await _asistenciaRepositorio.FindByIdAsync(id);

            if (asistencia == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, asistencia);

            await _asistenciaRepositorio.SaveAsync(asistencia);

            return new OperationResult<AsistenciaDto>()
            {
                Data = _mapper.Map<AsistenciaDto>(asistencia),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<AsistenciaDto>> FindAllAsync()
        {
            var response = await _asistenciaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<AsistenciaDto>>(response);
        }

        public async Task<AsistenciaDto> FindByIdAsync(int id)
        {
            var response = await _asistenciaRepositorio.FindByIdAsync(id);

            return _mapper.Map<AsistenciaDto>(response);
        }

        public async Task<IReadOnlyList<AsistenciaSelectDto>> SelectActivo()
        {
            var response = await _asistenciaRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<AsistenciaSelectDto>>(response);
        }

    }
}

