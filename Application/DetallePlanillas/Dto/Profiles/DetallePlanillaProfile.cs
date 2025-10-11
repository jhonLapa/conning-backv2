using Application.DetallePlanillas.Dto;
using Application.Permissions.Dto;
using AutoMapper;
using Domain;

namespace Application.DetallePlanillas.Dtos.Profiles
{
    public class DetallePlanillaProfile : Profile
    {
        public DetallePlanillaProfile()
        {

            CreateMap<DetallePlanilla, DetallePlanillaDto>().ReverseMap();
            CreateMap<DetallePlanilla, DetallePlanillaSaveDto>().ReverseMap();
            CreateMap<DetallePlanilla, DetallePlanillaSelectDto>().ReverseMap();
        }
    }
}

