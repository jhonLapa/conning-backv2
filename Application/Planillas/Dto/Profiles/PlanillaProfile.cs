using Application.Planillas.Dto;
using AutoMapper;
using Domain;   // 👈 Esto faltaba

namespace Application.Planillas.Dtos.Profiles
{
    public class PlanillaProfile : Profile
    {
        public PlanillaProfile()
        {
            // Planilla
            CreateMap<Planilla, PlanillaDto>().ReverseMap();
            CreateMap<Planilla, PlanillaSaveDto>().ReverseMap();

        }
    }
}
