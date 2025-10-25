using Application.Exceptions;
using Application.Mantenedores.Dtos.Bancos;
using Application.Mantenedores.Dtos.Planillas;
using Application.Planillas.Dto;
using Application.Planillas.Services.Interfaces;
using AutoMapper;
using Domain;
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
            var bank = await _planillaRepositorio.FindByIdAsync(id);

            if (bank == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            bank.Estado = bank.Estado == 1 ? 0 : 1;
            await _planillaRepositorio.SaveAsync(bank);

            return new OperationResult<PlanillaDto>()
            {
                Data = _mapper.Map<PlanillaDto>(bank),
                Message = bank.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
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

                if (planilla.IdPlanilla > 0)
                {
                    // Si existe, actualizar
                    var existente = await _planillaRepositorio.FindByIdAsync(planilla.IdPlanilla);
                    if (existente == null)
                        return new OperationResult<PlanillaDto> { Success = false, Message = "Planilla no encontrada." };

                    _mapper.Map(dto.Planilla, existente);
                    await _planillaRepositorio.SaveAsync(existente);

                    // 🧹 Eliminar detalles y aportes anteriores
                    await _detallePlanillaRepositorio.DeleteRangeAsync(planilla.IdPlanilla);
                    await _aportesPlanillaRepositorio.DeleteRangeAsync(planilla.IdPlanilla);

                    planilla = existente;
                }
                else
                {
                    // Nueva planilla
                    planilla.Estado = 1;
                    planilla.FechaCreacion = DateTime.Now;
                    planilla.UsuarioCreacion ??= "system";
                    await _planillaRepositorio.SaveAsync(planilla);
                }

                // 2️⃣ Insertar nuevos DETALLES
                foreach (var detalleDto in dto.Detalle)
                {
                    var detalle = _mapper.Map<DetallePlanilla>(detalleDto);
                    detalle.IdPlanilla = planilla.IdPlanilla;
                    detalle.FechaCreacion = DateTime.Now;
                    detalle.UsuarioCreacion ??= planilla.UsuarioCreacion;

                    detalle.TotalMonto = detalle.TotalMonto == 0 ? detalleDto.TotalMonto : detalle.TotalMonto;
                    detalle.TotalHoras = detalle.TotalHoras == 0 ? detalleDto.TotalHoras : detalle.TotalHoras;
                    detalle.TotalDescuentos = detalle.TotalDescuentos == 0 ? detalleDto.TotalDescuentos : detalle.TotalDescuentos;

                    await _detallePlanillaRepositorio.SaveAsync(detalle);
                }
                // 3️⃣ Insertar nuevos APORTES
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
                    Message = planilla.IdPlanilla > 0 ? "Planilla actualizada correctamente." : "Planilla registrada correctamente.",
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

