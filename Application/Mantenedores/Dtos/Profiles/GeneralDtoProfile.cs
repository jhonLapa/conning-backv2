using Application.Mantenedores.Dtos.Bancks;
using Application.Mantenedores.Dtos.Documentos;
using Application.Mantenedores.Dtos.Pensiones;
using Application.Mantenedores.Dtos.Proyectos;
using Application.Mantenedores.Dtos.Roles;
using Application.Usuarios.Dto;
using AutoMapper;
using Domain;

namespace Application.Mantenedores.Dtos.Profiles
{
    public class GeneralDtoProfile : Profile
    {
        public GeneralDtoProfile()
        {
            // Documento 
            CreateMap<Documento, DocumentoDto>().ReverseMap();
            CreateMap<Documento, DocumentoSaveDto>().ReverseMap();

            // Banco
            CreateMap<Banck, BanckDto>().ReverseMap();
            CreateMap<Banck, BanckSaveDto>().ReverseMap();

            //Pension 
            CreateMap<Pension, PensionDto>().ReverseMap();
            CreateMap<Pension, PensionSaveDto>().ReverseMap();

            //Proyecto
            CreateMap<Proyecto, ProyectoDto>().ReverseMap();
            CreateMap<Proyecto, ProyectoSaveDto>().ReverseMap();

            //Rol 
            CreateMap<Rol , RoleDto>().ReverseMap();
            CreateMap<Rol , RoleSaveDto>().ReverseMap();




        }
    }
}
