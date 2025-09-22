


using Application.ConfigAfectacions.Dto;
using Application.ConfigAfectacions.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.ConfigAfectacions.Services
{
    public class ConfigAfectacionService : IConfigAfectacionServices
    {
        private readonly IConfigAfectacionRepositorio _configAfectacionRepositorio;
        private readonly IMapper _mapper;

        public ConfigAfectacionService(IConfigAfectacionRepositorio configAfectacionRepositorio, IMapper mapper)
        {
            _configAfectacionRepositorio = configAfectacionRepositorio;
            _mapper = mapper;
        }


        public async Task<OperationResult<IEnumerable<ConfigAfectacionDto>>> SaveArrayAsync(IEnumerable<ConfigAfectacionSaveDto> saveDtos)
        {
            var resultList = new List<ConfigAfectacionDto>();

            foreach (var dto in saveDtos)
            {
                var entity = _mapper.Map<ConfigAfectacion>(dto);

                var existing = await _configAfectacionRepositorio
                                            .FindByEmpresaAndAfectacionAsync(dto.IdEmpresa, dto.IdAfectacion);

                if (existing != null)
                {
                    _mapper.Map(dto, existing);
                    existing.FechaModificacion = DateTime.Now;
                    existing.IdUsuarioModificacion = 1;

                    await _configAfectacionRepositorio.SaveAsync(existing);

                    // 👇 Agregar al resultado
                    resultList.Add(_mapper.Map<ConfigAfectacionDto>(existing));
                }
                else
                {
                    entity.FechaCreacion = DateTime.Now;
                    entity.IdUsuarioCreacion = 1;
                    entity.Estado = 1; // si en la BD es bit

                    await _configAfectacionRepositorio.SaveAsync(entity);

                    // 👇 Agregar al resultado
                    resultList.Add(_mapper.Map<ConfigAfectacionDto>(entity));
                }
            }

            return new OperationResult<IEnumerable<ConfigAfectacionDto>>()
            {
                Data = resultList,
                Message = "Configuraciones procesadas correctamente",
                State = true
            };
        }


        public async Task<OperationResult<ConfigAfectacionDto>> CreateAsync(ConfigAfectacionSaveDto saveDto)
        {
            var configAfectacion = _mapper.Map<Domain.ConfigAfectacion>(saveDto);


            await _configAfectacionRepositorio.SaveAsync(configAfectacion);

            return new OperationResult<ConfigAfectacionDto>()
            {
                Data = _mapper.Map<ConfigAfectacionDto>(configAfectacion),
                Message = "Se ha Creado",
                State = true,
            };

        }

        public async Task<OperationResult<ConfigAfectacionDto>> DisabledAsync(int id)
        {
            var configAfectacions = await _configAfectacionRepositorio.FindByIdAsync(id);
            if (configAfectacions == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<ConfigAfectacionDto>()
            {
                Data = _mapper.Map<ConfigAfectacionDto>(configAfectacions),
                Message = "Se ha Desactivado",
                State = true,
            };
        }

        public async Task<OperationResult<ConfigAfectacionDto>> EditAsync(int id, ConfigAfectacionSaveDto saveDto)
        {
            var configAfectacions = await _configAfectacionRepositorio.FindByIdAsync(id);

            if (configAfectacions == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, configAfectacions);

            await _configAfectacionRepositorio.SaveAsync(configAfectacions);

            return new OperationResult<ConfigAfectacionDto>()
            {
                Data = _mapper.Map<ConfigAfectacionDto>(configAfectacions),
                Message = "Se ha actualizado",
                State = true,
            };

        }

        public async Task<IReadOnlyList<ConfigAfectacionDto>> FindAllAsync()
        {
            var response = await _configAfectacionRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ConfigAfectacionDto>>(response);
        }

        public async Task<ConfigAfectacionDto> FindByIdAsync(int id)
        {
            var response = await _configAfectacionRepositorio.FindByIdAsync(id);

            return _mapper.Map<ConfigAfectacionDto>(response);
        }




    }
}