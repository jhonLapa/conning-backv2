using Application.Empresas.Dto;
using AutoMapper;
using Domain;

namespace Application.Empresas.Dtos.Profiles
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            // Empresa
            CreateMap<Empresa, EmpresaDto>().ReverseMap();
            CreateMap<Empresa, EmpresaSaveDto>().ReverseMap();
        }
    }
}
 
