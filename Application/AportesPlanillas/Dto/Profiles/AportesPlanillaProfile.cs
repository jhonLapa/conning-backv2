using Application.AportesPlanillas.Dto;
using AutoMapper;
using Domain;

namespace Application.AportesPlanillas.Dtos.Profiles
{
    public class AportesPlanillaProfile : Profile
    {
        public AportesPlanillaProfile()
        {
            // AportesPlanilla básica
            CreateMap<AportesPlanilla, AportesPlanillaDto>().ReverseMap();
            CreateMap<AportesPlanilla, AportesPlanillaSaveDto>().ReverseMap();
            CreateMap<AportesPlanilla, AportesPlanillaSelectDto>().ReverseMap();


        }
    }
}