using Application.EntidadComisiones.Dto;
using Application.EntidadComisiones.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.EntidadComision.Services
{
    public class EntidadComisionService : IEntidadComisionServices
    {
        private readonly IEntidadComisionRepositorio _entidadComision;
        private readonly IMapper _mapper;

        public EntidadComisionService(IEntidadComisionRepositorio EntidadComisionRepositorio, IMapper mapper)
        {
            _entidadComision = EntidadComisionRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<EntidadComisionDto>> CreateAsync(EntidadComisionSaveDto saveDto)
        {
            var EntidadComision = _mapper.Map<Domain.EntidadComision>(saveDto);


            await _entidadComision.SaveAsync(EntidadComision);

            return new OperationResult<EntidadComisionDto>()
            {
                Data = _mapper.Map<EntidadComisionDto>(EntidadComision),
                Message = "Se ha Creado",
            };

        }

        public async Task<OperationResult<EntidadComisionDto>> DisabledAsync(int id)
        {
            var EntidadComisions = await _entidadComision.FindByIdAsync(id);
            if (EntidadComisions == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<EntidadComisionDto>()
            {
                Data = _mapper.Map<EntidadComisionDto>(EntidadComisions),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<EntidadComisionDto>> EditAsync(int id, EntidadComisionSaveDto saveDto)
        {
            var EntidadComisions = await _entidadComision.FindByIdAsync(id);

            if (EntidadComisions == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, EntidadComisions);

            await _entidadComision.SaveAsync(EntidadComisions);

            return new OperationResult<EntidadComisionDto>()
            {
                Data = _mapper.Map<EntidadComisionDto>(EntidadComisions),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<EntidadComisionDto>> FindAllAsync()
        {
            var response = await _entidadComision.FindAllAsync();

            return _mapper.Map<IReadOnlyList<EntidadComisionDto>>(response);
        }

        public async Task<EntidadComisionDto> FindByIdAsync(int id)
        {
            var response = await _entidadComision.FindByIdAsync(id);

            return _mapper.Map<EntidadComisionDto>(response);
        }
    }
}
