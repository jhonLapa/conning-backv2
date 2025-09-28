using Application.Exceptions;
using Application.Mantenedores.Dtos.EntidadPrevisionals;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class EntidadPrevisionalService : IEntidadPrevisionalService
    {
        private readonly IEntidadPrevisionalRepositorio _entidadPrevisionalRepositorio;
        private readonly IMapper _mapper;

        public EntidadPrevisionalService(IEntidadPrevisionalRepositorio EntidadPrevisionalRepositorio, IMapper mapper)
        {
            _entidadPrevisionalRepositorio = EntidadPrevisionalRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<EntidadPrevisionalDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _entidadPrevisionalRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<EntidadPrevisionalDto>>(response.Data);

            return new PaginadoResponse<EntidadPrevisionalDto>(data, response.Meta);
        }

        public async Task<OperationResult<EntidadPrevisionalDto>> CreateAsync(EntidadPrevisionalSaveDto saveDto)
        {
            var entidadPrevisional = _mapper.Map<EntidadPrevisional>(saveDto);
            entidadPrevisional.FechaCreacion = DateTime.Now;
            entidadPrevisional.IdUsuarioCreacion = 1;

            await _entidadPrevisionalRepositorio.SaveAsync(entidadPrevisional);

            return new OperationResult<EntidadPrevisionalDto>()
            {
                Data = _mapper.Map<EntidadPrevisionalDto>(entidadPrevisional),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<EntidadPrevisionalDto>> DisabledAsync(int id)
        {
            var entidadPrevisional = await _entidadPrevisionalRepositorio.FindByIdAsync(id) ?? throw new NotFoundCoreException("Registro no encontrado con ese Id");

            entidadPrevisional.Estado = entidadPrevisional.Estado == 1 ? 0 : 1;
            entidadPrevisional.FechaModificacion = DateTime.Now;

            await _entidadPrevisionalRepositorio.SaveAsync(entidadPrevisional);

            return new OperationResult<EntidadPrevisionalDto>()
            {
                Data = _mapper.Map<EntidadPrevisionalDto>(entidadPrevisional),
                Message = entidadPrevisional.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<EntidadPrevisionalDto>> EditAsync(int id, EntidadPrevisionalSaveDto saveDto)
        {
            var entidadPrevisional = await _entidadPrevisionalRepositorio.FindByIdAsync(id);

            if (entidadPrevisional == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            entidadPrevisional.FechaModificacion = DateTime.Now;
            entidadPrevisional.IdUsuarioModificacion = 1;

            _mapper.Map(saveDto, entidadPrevisional);

            await _entidadPrevisionalRepositorio.SaveAsync(entidadPrevisional);

            return new OperationResult<EntidadPrevisionalDto>()
            {
                Data = _mapper.Map<EntidadPrevisionalDto>(entidadPrevisional),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<EntidadPrevisionalDto>> FindAllAsync()
        {
            var response = await _entidadPrevisionalRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<EntidadPrevisionalDto>>(response);
        }

        public async Task<EntidadPrevisionalDto> FindByIdAsync(int id)
        {
            var entidadPrevisional = await _entidadPrevisionalRepositorio.FindByIdAsync(id);

            if (entidadPrevisional == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<EntidadPrevisionalDto>(entidadPrevisional);
        }
    }
}


