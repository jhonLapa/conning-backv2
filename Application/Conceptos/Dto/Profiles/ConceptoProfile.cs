
using Application.Conceptos.Dto;
using AutoMapper;

namespace Application.Conceptos.Dtos.Profiles
{
    public class ConceptoProfile : Profile
    {
        public ConceptoProfile()
        {
            CreateMap<Domain.Concepto, ConceptoDto>().ReverseMap();
            CreateMap<Domain.Concepto, ConceptoSaveDto>().ReverseMap();
        }
    }
}
