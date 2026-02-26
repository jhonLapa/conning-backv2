using AutoMapper;
using Domain;

namespace Application.Departamentos.Dto.Profiles
{
    public class DepartamentoProfile : Profile
    {
        public DepartamentoProfile()
        {
            CreateMap<Departamento, DepartamentoDto>();

            CreateMap<SaveDepartamento, Departamento>();
         
        }
    }
}