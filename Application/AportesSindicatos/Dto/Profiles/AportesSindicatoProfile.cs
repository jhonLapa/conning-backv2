using Application.AportesSindicatos.Dto;
using AutoMapper;
using Domain;

namespace Application.AportesSindicatos.Dtos.Profiles
{
    public class AportesSindicatoProfile : Profile
    {
        public AportesSindicatoProfile()
        {
            // AportesSindicato básica
            CreateMap<AportesSindicato, AportesSindicatoDto>().ReverseMap();
            CreateMap<AportesSindicato, AportesSindicatoSaveDto>().ReverseMap();
            CreateMap<AportesSindicato, AportesSindicatoSelectDto>().ReverseMap();


        }
    }
}