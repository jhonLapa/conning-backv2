using Application.Exceptions;
using Application.Mantenedores.Dtos.AportesEmpleadores;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class AportesEmpleadorService : IAportesEmpleadorService
    {
        private readonly IAportesEmpleadorRepositorio _aportesEmpleadorRepositorio;
        private readonly IMapper _mapper;

        public AportesEmpleadorService(IAportesEmpleadorRepositorio AportesEmpleadorRepositorio, IMapper mapper)
        {
            _aportesEmpleadorRepositorio = AportesEmpleadorRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<AportesEmpleadorDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _aportesEmpleadorRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<AportesEmpleadorDto>>(response.Data);

            return new PaginadoResponse<AportesEmpleadorDto>(data, response.Meta);
        }
        public async Task<OperationResult<AportesEmpleadorDto>> CreateAsync(AportesEmpleadorSaveDto saveDto)
        {
            var aportesEmpleador = _mapper.Map<AportesEmpleador>(saveDto);
            aportesEmpleador.FechaCreacion = DateTime.Now;
            aportesEmpleador.IdUsuarioCreacion = 1;
            aportesEmpleador.Estado = 1;

            await _aportesEmpleadorRepositorio.SaveAsync(aportesEmpleador);

            return new OperationResult<AportesEmpleadorDto>()
            {
                Data = _mapper.Map<AportesEmpleadorDto>(aportesEmpleador),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<AportesEmpleadorDto>> DisabledAsync(int id)
        {
            var aportesEmpleador = await _aportesEmpleadorRepositorio.FindByIdAsync(id);

            if (aportesEmpleador == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            aportesEmpleador.Estado = aportesEmpleador.Estado == 1 ? 0 : 1;
            aportesEmpleador.FechaModificacion = DateTime.Now;

            await _aportesEmpleadorRepositorio.SaveAsync(aportesEmpleador);

            return new OperationResult<AportesEmpleadorDto>()
            {
                Data = _mapper.Map<AportesEmpleadorDto>(aportesEmpleador),
                Message = aportesEmpleador.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<AportesEmpleadorDto>> EditAsync(int id, AportesEmpleadorSaveDto saveDto)
        {
            var aportesEmpleador = await _aportesEmpleadorRepositorio.FindByIdAsync(id);

            if (aportesEmpleador == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            aportesEmpleador.FechaModificacion = DateTime.Now;
            aportesEmpleador.IdUsuarioModificacion = 1;

            _mapper.Map(saveDto, aportesEmpleador);

            await _aportesEmpleadorRepositorio.SaveAsync(aportesEmpleador);

            return new OperationResult<AportesEmpleadorDto>()
            {
                Data = _mapper.Map<AportesEmpleadorDto>(aportesEmpleador),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<AportesEmpleadorDto>> FindAllAsync()
        {
            var response = await _aportesEmpleadorRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<AportesEmpleadorDto>>(response);
        }

        public async Task<AportesEmpleadorDto> FindByIdAsync(int id)
        {
            var aportesEmpleador = await _aportesEmpleadorRepositorio.FindByIdAsync(id);

            if (aportesEmpleador == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<AportesEmpleadorDto>(aportesEmpleador);
        }
    }
}
