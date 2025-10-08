using Application.Asistencias.Dto;
using AutoMapper;
using Domain;

namespace Application.Asistencias.Dtos.Profiles
{
    public class AsistenciaProfile : Profile
    {
        public AsistenciaProfile()
        {
            // Asistencia básica
            CreateMap<Asistencia, AsistenciaDto>().ReverseMap();
            CreateMap<Asistencia, AsistenciaSaveDto>().ReverseMap();
            CreateMap<Asistencia, AsistenciaSelectDto>().ReverseMap();


        }
    }
}
