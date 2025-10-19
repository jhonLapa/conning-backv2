using Application.AportesPlanillas.Dto;
using Application.DetallePlanillas.Dto;
using Application.Mantenedores.Dtos.Planillas; // DTOs de creación
using Application.Planillas.Dto;
using AutoMapper;
using Domain;

namespace Application.Planillas.Dtos.Profiles
{
    public class PlanillaProfile : Profile
    {
        public PlanillaProfile()
        {
            // ===========================================
            // 🔹 PLANILLA - Lectura
            // ===========================================
            CreateMap<Planilla, PlanillaDto>()
                .ForMember(dest => dest.Proyecto, opt => opt.MapFrom(src => src.Proyecto))
                .ForMember(dest => dest.AportesPlanilla, opt => opt.MapFrom(src => src.AportesPlanilla))
                .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles))
                .ReverseMap();

            // ===========================================
            // 🔹 PLANILLA - Creación
            // ===========================================
            CreateMap<PlanillaCreateDto, Planilla>()
                .ForMember(dest => dest.IdPlanilla, opt => opt.MapFrom(src => src.IdPlanilla))
                .ForMember(dest => dest.IdProyecto, opt => opt.MapFrom(src => src.IdProyecto))
                .ForMember(dest => dest.Mes, opt => opt.MapFrom(src => src.Mes))
                .ForMember(dest => dest.Anio, opt => opt.MapFrom(src => src.Anio))
                .ForMember(dest => dest.PeriodoInicio, opt => opt.MapFrom(src => src.PeriodoInicio))
                .ForMember(dest => dest.PeriodoFin, opt => opt.MapFrom(src => src.PeriodoFin))
                .ForMember(dest => dest.FechaPago, opt => opt.MapFrom(src => src.FechaPago))
                .ForMember(dest => dest.UsuarioCreacion, opt => opt.MapFrom(src => src.UsuarioCreacion))
                .ForMember(dest => dest.FrecuenciaPago, opt => opt.MapFrom(src => src.FrecuenciaPago))
                .ForMember(dest => dest.TotalHoras, opt => opt.MapFrom(src => src.TotalHoras))
                .ForMember(dest => dest.TotalGeneral, opt => opt.MapFrom(src => src.TotalGeneral))
                .ReverseMap();

            // ===========================================
            // 🔹 DETALLE PLANILLA - Creación
            // ===========================================
            CreateMap<DetallePlanillaCreateDto, DetallePlanilla>()
                .ForMember(dest => dest.IdDetallePlanilla, opt => opt.MapFrom(src => src.IdDetallePlanilla))
                .ForMember(dest => dest.IdPlanilla, opt => opt.MapFrom(src => src.IdPlanilla))
                .ForMember(dest => dest.IdTrabajadorProyecto, opt => opt.MapFrom(src => src.IdTrabajadorProyecto))
                .ForMember(dest => dest.DiasTrabajados, opt => opt.MapFrom(src => src.DiasTrabajados))
                .ForMember(dest => dest.HorasTrabajadas, opt => opt.MapFrom(src => src.HorasTrabajadas))
                .ForMember(dest => dest.TotalMonto, opt => opt.MapFrom(src => src.TotalMonto))
                .ForMember(dest => dest.TotalHoras, opt => opt.MapFrom(src => src.TotalHoras))
                .ForMember(dest => dest.TotalDescuentos, opt => opt.MapFrom(src => src.TotalDescuentos))
                .ForMember(dest => dest.UsuarioCreacion, opt => opt.MapFrom(src => src.UsuarioCreacion))
                .ReverseMap();

            // ===========================================
            // 🔹 APORTES PLANILLA - Creación
            // ===========================================
            CreateMap<AportePlanillaDto, AportesPlanilla>()
                .ForMember(dest => dest.IdAportePlanilla, opt => opt.MapFrom(src => src.IdAportePlanilla))
                .ForMember(dest => dest.IdPlanilla, opt => opt.MapFrom(src => src.IdPlanilla))
                .ForMember(dest => dest.TipoAporte, opt => opt.MapFrom(src => src.TipoAporte))
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto))
                .ForMember(dest => dest.FechaVencimiento, opt => opt.MapFrom(src => src.FechaVencimiento))
                .ForMember(dest => dest.FechaPago, opt => opt.MapFrom(src => src.FechaPago))
                .ReverseMap();

            // ===========================================
            // 🔹 DETALLE PLANILLA - Lectura
            // ===========================================
            CreateMap<DetallePlanilla, DetallePlanillaDto>().ReverseMap();

            // ===========================================
            // 🔹 APORTES PLANILLA - Lectura
            // ===========================================
            CreateMap<AportesPlanilla, AportesPlanillaDto>().ReverseMap();

            // ===========================================
            // 🔹 PLANILLA SAVE (usado en PUT)
            // ===========================================
            CreateMap<Planilla, PlanillaSaveDto>().ReverseMap();

            // ===========================================
            // 🔹 PROYECTO
            // ===========================================
            CreateMap<Proyecto, ProyectoDto>().ReverseMap();
        }
    }
}
