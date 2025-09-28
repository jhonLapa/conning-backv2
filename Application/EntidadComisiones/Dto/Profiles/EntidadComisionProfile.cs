using Application.EntidadComisiones.Dto;
using AutoMapper;
using Domain;   // 👈 Esto faltaba

namespace Application.EntidadComisions.Dtos.Profiles
{
    public class EntidadComisionProfile : Profile
    {
        public EntidadComisionProfile()
        {
            // EntidadComision
            CreateMap<Domain.EntidadComision, EntidadComisionDto>().ReverseMap();
            CreateMap<Domain.EntidadComision, EntidadComisionSaveDto>().ReverseMap();
        }
    }
}

