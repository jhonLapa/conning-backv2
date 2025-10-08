using Application.Planillas.Dto;
using Application.Planillas.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Planillas.Services
{
    public class PlanillaService : IPlanillaServices
    {
        private readonly IPlanillaRepositorio _planillaRepositorio;
        private readonly IMapper _mapper;

        public PlanillaService(IPlanillaRepositorio PlanillaRepositorio, IMapper mapper)
        {
            _planillaRepositorio = PlanillaRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<PlanillaDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _planillaRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<PlanillaDto>>(response.Data);

            return new PaginadoResponse<PlanillaDto>(data, response.Meta);
        }


        public async Task<OperationResult<PlanillaDto>> CreateAsync(PlanillaSaveDto saveDto)
        {
            var planilla = _mapper.Map<Domain.Planilla>(saveDto);


            await _planillaRepositorio.SaveAsync(planilla);

            return new OperationResult<PlanillaDto>()
            {
                Data = _mapper.Map<PlanillaDto>(planilla),
                Message = "Se ha Creado",

            };

        }

        public async Task<OperationResult<PlanillaDto>> DisabledAsync(int id)
        {
            var planilla = await _planillaRepositorio.FindByIdAsync(id);
            if (planilla == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<PlanillaDto>()
            {
                Data = _mapper.Map<PlanillaDto>(planilla),
                Message = "Se ha Desactivado",

            };
        }

        public async Task<OperationResult<PlanillaDto>> EditAsync(int id, PlanillaSaveDto saveDto)
        {
            var planilla = await _planillaRepositorio.FindByIdAsync(id);

            if (planilla == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, planilla);

            await _planillaRepositorio.SaveAsync(planilla);

            return new OperationResult<PlanillaDto>()
            {
                Data = _mapper.Map<PlanillaDto>(planilla),
                Message = "Se ha actualizado",

            };

        }

        public async Task<IReadOnlyList<PlanillaDto>> FindAllAsync()
        {
            var response = await _planillaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<PlanillaDto>>(response);
        }

        public async Task<PlanillaDto> FindByIdAsync(int id)
        {
            var response = await _planillaRepositorio.FindByIdAsync(id);

            return _mapper.Map<PlanillaDto>(response);
        }


    }
}

