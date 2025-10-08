using Application.AportesSindicatos.Dto;
using Application.AportesSindicatos.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.AportesSindicatos.Servicess
{
    public class AportesSindicatoService : IAportesSindicatoServices
    {
        private readonly IAportesSindicatoRepositorio _aportesSindicatoRepositorio;
        private readonly IProyectoRepositorio _projectRepositorio;
        private readonly IMapper _mapper;

        public AportesSindicatoService(
            IAportesSindicatoRepositorio aportesSindicatoRepositorio,
            IProyectoRepositorio ProjectRepositorio,
            IMapper mapper)
        {
            _aportesSindicatoRepositorio = aportesSindicatoRepositorio;
            _projectRepositorio = ProjectRepositorio;
            _mapper = mapper;
        }



        public async Task<PaginadoResponse<AportesSindicatoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _aportesSindicatoRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<AportesSindicatoDto>>(response.Data);

            return new PaginadoResponse<AportesSindicatoDto>(data, response.Meta);
        }


        public async Task<OperationResult<AportesSindicatoDto>> CreateAsync(AportesSindicatoSaveDto saveDto)
        {

            var project = await _projectRepositorio.FindByIdAsync(saveDto.IdProyecto);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese Id de proyecto");

            var aportesSindicato = _mapper.Map<AportesSindicato>(saveDto);

            aportesSindicato.FechaCreacion = DateTime.Now;
            aportesSindicato.Estado = 0;

            await _aportesSindicatoRepositorio.SaveAsync(aportesSindicato);

            return new OperationResult<AportesSindicatoDto>()
            {
                Data = _mapper.Map<AportesSindicatoDto>(aportesSindicato),
                Message = "Se ha Creado",
            };

        }


        public async Task<OperationResult<AportesSindicatoDto>> DisabledAsync(int id)
        {
            var aportesSindicato = await _aportesSindicatoRepositorio.FindByIdAsync(id);

            if (aportesSindicato == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            aportesSindicato.Estado = aportesSindicato.Estado == 1 ? 0 : 1;


            await _aportesSindicatoRepositorio.SaveAsync(aportesSindicato);

            return new OperationResult<AportesSindicatoDto>()
            {
                Data = _mapper.Map<AportesSindicatoDto>(aportesSindicato),
                Message = aportesSindicato.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<AportesSindicatoDto>> EditAsync(int id, AportesSindicatoSaveDto saveDto)
        {
            var aportesSindicato = await _aportesSindicatoRepositorio.FindByIdAsync(id);

            if (aportesSindicato == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, aportesSindicato);

            await _aportesSindicatoRepositorio.SaveAsync(aportesSindicato);

            return new OperationResult<AportesSindicatoDto>()
            {
                Data = _mapper.Map<AportesSindicatoDto>(aportesSindicato),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<AportesSindicatoDto>> FindAllAsync()
        {
            var response = await _aportesSindicatoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<AportesSindicatoDto>>(response);
        }

        public async Task<AportesSindicatoDto> FindByIdAsync(int id)
        {
            var response = await _aportesSindicatoRepositorio.FindByIdAsync(id);

            return _mapper.Map<AportesSindicatoDto>(response);
        }

        public async Task<IReadOnlyList<AportesSindicatoSelectDto>> SelectActivo()
        {
            var response = await _aportesSindicatoRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<AportesSindicatoSelectDto>>(response);
        }

    }
}
