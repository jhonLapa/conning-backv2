using Application.ConceptoAfectacions.Dto;
using AutoMapper;
using Domain;   // 👈 Esto faltaba

namespace Application.ConceptoAfectacions.Dtos.Profiles
{
    public class ConceptoAfectacionProfile : Profile
    {
        public ConceptoAfectacionProfile()
        {
            // ConceptoAfectacion
            CreateMap<Domain.ConceptoAfectacion, ConceptoAfectacionDto>().ReverseMap();
            CreateMap<Domain.ConceptoAfectacion, ConceptoAfectacionSaveDto>().ReverseMap();
        }
    }
}