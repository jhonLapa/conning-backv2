using Application.Exceptions;
using Application.Mantenedores.Dtos.RegimenesPrevisionales;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class RegimenPrevisionalService : IRegimenPrevisionalService
    {
        private readonly IRegimenPrevisionalRepositorio _regimenPrevisionalRepositorio;
        private readonly IMapper _mapper;

        public RegimenPrevisionalService(IRegimenPrevisionalRepositorio AfectacionRepositorio, IMapper mapper)
        {
            _regimenPrevisionalRepositorio = AfectacionRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<RegimenPrevisionalDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _regimenPrevisionalRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<RegimenPrevisionalDto>>(response.Data);

            return new PaginadoResponse<RegimenPrevisionalDto>(data, response.Meta);
        }

        public async Task<OperationResult<RegimenPrevisionalDto>> CreateAsync(RegimenPrevisionalSaveDto saveDto)
        {
            var regimenPrevisional = _mapper.Map<RegimenPrevisional>(saveDto);
             regimenPrevisional.Estado = 1;

            await _regimenPrevisionalRepositorio.SaveAsync(regimenPrevisional);

            return new OperationResult<RegimenPrevisionalDto>()
            {
                Data = _mapper.Map<RegimenPrevisionalDto>(regimenPrevisional),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<RegimenPrevisionalDto>> DisabledAsync(int id)
        {
            var regimenPrevisional = await _regimenPrevisionalRepositorio.FindByIdAsync(id) ?? throw new NotFoundCoreException("Registro no encontrado con ese Id");
            
            regimenPrevisional.Estado = regimenPrevisional.Estado == 1 ? 0 : 1;
 
            await _regimenPrevisionalRepositorio.SaveAsync(regimenPrevisional);

            return new OperationResult<RegimenPrevisionalDto>()
            {
                Data = _mapper.Map<RegimenPrevisionalDto>(regimenPrevisional),
                Message = regimenPrevisional.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<RegimenPrevisionalDto>> EditAsync(int id, RegimenPrevisionalSaveDto saveDto)
        {
            var regimenPrevisional = await _regimenPrevisionalRepositorio.FindByIdAsync(id);

            if (regimenPrevisional == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

          

            _mapper.Map(saveDto, regimenPrevisional);

            await _regimenPrevisionalRepositorio.SaveAsync(regimenPrevisional);

            return new OperationResult<RegimenPrevisionalDto>()
            {
                Data = _mapper.Map<RegimenPrevisionalDto>(regimenPrevisional),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<RegimenPrevisionalDto>> FindAllAsync()
        {
            var response = await _regimenPrevisionalRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<RegimenPrevisionalDto>>(response);
        }

        public async Task<RegimenPrevisionalDto> FindByIdAsync(int id)
        {
            var regimenPrevisional = await _regimenPrevisionalRepositorio.FindByIdAsync(id);

            if (regimenPrevisional == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<RegimenPrevisionalDto>(regimenPrevisional);
        }
    }
}
