using Application.AportesPlanillas.Dto;
using Application.AportesPlanillas.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.AportesPlanillas.Servicess
{
    public class AportesPlanillaService : IAportesPlanillaServices
    {
        private readonly IAportesPlanillaRepositorio _aportesPlanillaRepositorio;

        private readonly IMapper _mapper;

        public AportesPlanillaService(
            IAportesPlanillaRepositorio aportesPlanillaRepositorio,

            IMapper mapper)
        {
            _aportesPlanillaRepositorio = aportesPlanillaRepositorio;

            _mapper = mapper;
        }

        public async Task<PaginadoResponse<AportesPlanillaDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _aportesPlanillaRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<AportesPlanillaDto>>(response.Data);

            return new PaginadoResponse<AportesPlanillaDto>(data, response.Meta);
        }


        public async Task<OperationResult<AportesPlanillaDto>> CreateAsync(AportesPlanillaSaveDto saveDto)
        {
            var aportesPlanilla = _mapper.Map<AportesPlanilla>(saveDto);

            aportesPlanilla.FechaVencimiento = DateTime.Now;

            await _aportesPlanillaRepositorio.SaveAsync(aportesPlanilla);

            return new OperationResult<AportesPlanillaDto>()
            {
                Data = _mapper.Map<AportesPlanillaDto>(aportesPlanilla),
                Message = "Se ha Creado",
            };

        }

        public async Task<OperationResult<AportesPlanillaDto>> DisabledAsync(int id)
        {
            var aportesPlanilla = await _aportesPlanillaRepositorio.FindByIdAsync(id);

            if (aportesPlanilla == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            aportesPlanilla.Estado = aportesPlanilla.Estado == 1 ? 0 : 1;


            await _aportesPlanillaRepositorio.SaveAsync(aportesPlanilla);

            return new OperationResult<AportesPlanillaDto>()
            {
                Data = _mapper.Map<AportesPlanillaDto>(aportesPlanilla),
                Message = aportesPlanilla.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<AportesPlanillaDto>> EditAsync(int id, AportesPlanillaSaveDto saveDto)
        {
            var aportesPlanilla = await _aportesPlanillaRepositorio.FindByIdAsync(id);

            if (aportesPlanilla == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, aportesPlanilla);

            await _aportesPlanillaRepositorio.SaveAsync(aportesPlanilla);

            return new OperationResult<AportesPlanillaDto>()
            {
                Data = _mapper.Map<AportesPlanillaDto>(aportesPlanilla),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<AportesPlanillaDto>> FindAllAsync()
        {
            var response = await _aportesPlanillaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<AportesPlanillaDto>>(response);
        }

        public async Task<AportesPlanillaDto> FindByIdAsync(int id)
        {
            var response = await _aportesPlanillaRepositorio.FindByIdAsync(id);

            return _mapper.Map<AportesPlanillaDto>(response);
        }

        public async Task<IReadOnlyList<AportesPlanillaSelectDto>> SelectActivo()
        {
            var response = await _aportesPlanillaRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<AportesPlanillaSelectDto>>(response);
        }

    }
}
