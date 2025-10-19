using Application.Planillas.Dto;
using Application.AportesPlanillas.Dto;
using Application.DetallePlanillas.Dto;
using Application.Asistencias.Dto;
using Application.Mantenedores.Dtos.Proyectos;
using AutoMapper;
using Domain;

namespace Application.Planillas.Dtos.Profiles
{
    public class PlanillaProfile : Profile
    {
        public PlanillaProfile()
        {
            // ===========================================
            // 🔹 PLANILLA
            // ===========================================
            CreateMap<Planilla, PlanillaDto>()
                .ForMember(dest => dest.Proyecto, opt => opt.MapFrom(src => src.Proyecto))
                .ForMember(dest => dest.AportesPlanilla, opt => opt.MapFrom(src => src.AportesPlanilla))
                .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles))
                .ReverseMap();

            CreateMap<Planilla, PlanillaSaveDto>().ReverseMap();

            // ===========================================
            // 🔹 APORTE PLANILLA
            // ===========================================
            CreateMap<AportesPlanilla, AportesPlanillaDto>()
                .ForMember(dest => dest.IdAportePlanilla, opt => opt.MapFrom(src => src.IdAportePlanilla))
                .ForMember(dest => dest.TipoAporte, opt => opt.MapFrom(src => src.TipoAporte))
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto))
                .ForMember(dest => dest.FechaVencimiento, opt => opt.MapFrom(src => src.FechaVencimiento))
                .ForMember(dest => dest.FechaPago, opt => opt.MapFrom(src => src.FechaPago))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ReverseMap();

            // ===========================================
            // 🔹 DETALLE PLANILLA
            // ===========================================
            CreateMap<DetallePlanilla, DetallePlanillaDto>()
                .ForMember(dest => dest.IdDetallePlanilla, opt => opt.MapFrom(src => src.IdDetallePlanilla))
                .ForMember(dest => dest.IdTrabajadorProyecto, opt => opt.MapFrom(src => src.IdTrabajadorProyecto))
                .ForMember(dest => dest.DiasTrabajados, opt => opt.MapFrom(src => src.DiasTrabajados))
                .ForMember(dest => dest.HorasTrabajadas, opt => opt.MapFrom(src => src.HorasTrabajadas))
                .ForMember(dest => dest.TotalHoras, opt => opt.MapFrom(src => src.TotalHoras))
                .ForMember(dest => dest.TotalDescuentos, opt => opt.MapFrom(src => src.TotalDescuentos))
                .ForMember(dest => dest.Asistencias, opt => opt.MapFrom(src => src.Asistencias))
                .ReverseMap();

            // ===========================================
            // 🔹 ASISTENCIAS
            // ===========================================
            CreateMap<Asistencia, AsistenciaDto>()
                .ForMember(dest => dest.IdAsistencia, opt => opt.MapFrom(src => src.IdAsistencia))
                .ForMember(dest => dest.IdDetallePlanilla, opt => opt.MapFrom(src => src.IdDetallePlanilla))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.HorasTrabajadas, opt => opt.MapFrom(src => src.HorasTrabajadas))
                .ForMember(dest => dest.Observacion, opt => opt.MapFrom(src => src.Observacion))
                .ReverseMap();

            // ===========================================
            // 🔹 PROYECTO
            // ===========================================
            CreateMap<Proyecto, ProyectoDto>().ReverseMap();
        }
    }
}
