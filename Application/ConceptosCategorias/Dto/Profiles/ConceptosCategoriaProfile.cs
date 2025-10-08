using Application.ConceptosCategorias.Dto;
using AutoMapper;
using Domain;

namespace Application.ConceptosCategorias.Dtos.Profiles
{
    public class ConceptosCategoriaProfile : Profile
    {
        public ConceptosCategoriaProfile()
        {
            // ConceptosCategoria básica
            CreateMap<ConceptosCategoria, ConceptosCategoriaDto>().ReverseMap();
            CreateMap<ConceptosCategoria, ConceptosCategoriaSaveDto>().ReverseMap();
            CreateMap<ConceptosCategoria, ConceptosCategoriaSelectDto>().ReverseMap();


        }
    }
}