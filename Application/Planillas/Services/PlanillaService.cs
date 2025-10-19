using Application.Exceptions;
using Application.Mantenedores.Dtos.Planillas;
using Application.Planillas.Dto;
using Application.Planillas.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.Planillas.Services
{
    public class PlanillaService : IPlanillaServices
    {
        private readonly IPlanillaRepositorio _planillaRepositorio;
        private readonly IMapper _mapper;
        private readonly IDetallePlanillaRepositorio _detallePlanillaRepositorio;
        private readonly IAportesPlanillaRepositorio _aportesPlanillaRepositorio;

        public PlanillaService(IPlanillaRepositorio PlanillaRepositorio, IAportesPlanillaRepositorio aportesPlanillaRepositorio,  IDetallePlanillaRepositorio DetallePlanillaRepositorio, IMapper mapper)
        {
            _planillaRepositorio = PlanillaRepositorio;
            _detallePlanillaRepositorio = DetallePlanillaRepositorio;
            _aportesPlanillaRepositorio = aportesPlanillaRepositorio;
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
            var planilla = _mapper.Map<Planilla>(saveDto);


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

        public async Task<OperationResult<PlanillaDto>> CreatePlanillaCompletaAsync(PlanillaFormDataDto dto)
        {
            try
            {
                // 1️⃣ Crear o actualizar PLANILLA
                var planilla = _mapper.Map<Planilla>(dto.Planilla);
                planilla.FechaCreacion = planilla.FechaCreacion == default ? DateTime.Now : planilla.FechaCreacion;
                planilla.UsuarioCreacion ??= "system";
                await _planillaRepositorio.SaveAsync(planilla);

                // 2️⃣ DETALLE PLANILLA
                foreach (var detalleDto in dto.Detalle)
                {
                    var detalle = _mapper.Map<DetallePlanilla>(detalleDto);
                    detalle.IdPlanilla = planilla.IdPlanilla;
                    detalle.FechaCreacion = detalle.FechaCreacion == default ? DateTime.Now : detalle.FechaCreacion;
                    detalle.UsuarioCreacion ??= planilla.UsuarioCreacion;
                    await _detallePlanillaRepositorio.SaveAsync(detalle);
                }

                // 3️⃣ APORTES PLANILLA
                foreach (var aporteDto in dto.Aportes)
                {
                    var aporte = _mapper.Map<AportesPlanilla>(aporteDto);
                    aporte.IdPlanilla = planilla.IdPlanilla;
                    await _aportesPlanillaRepositorio.SaveAsync(aporte);
                }


                var resultDto = _mapper.Map<PlanillaDto>(planilla);
                return new OperationResult<PlanillaDto>
                {
                    Success = true,
                    Message = "Planilla registrada correctamente.",
                    Data = resultDto
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<PlanillaDto>
                {
                    Success = false,
                    Message = $"Error al registrar planilla: {ex.Message}"
                };
            }
        }




    }
}

