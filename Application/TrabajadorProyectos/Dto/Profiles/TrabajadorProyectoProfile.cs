using Application.TrabajadorProyectos.Dto;
using AutoMapper;
using Domain;

namespace Application.TrabajadorProyectos.Dtos.Profiles
{
    public class TrabajadorProyectoProfile : Profile
    {
        public TrabajadorProyectoProfile()
        {
            // TrabajadorProyecto básica
            CreateMap<TrabajadorProyecto, TrabajadorProyectoDto>().ReverseMap();
            CreateMap<TrabajadorProyecto, TrabajadorProyectoSaveDto>().ReverseMap();
            CreateMap<TrabajadorProyecto, TrabajadorProyectoSelectDto>().ReverseMap();


        }
    }
}