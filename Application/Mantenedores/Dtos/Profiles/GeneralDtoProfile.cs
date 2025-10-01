using Application.Mantenedores.Dtos.RegimenesPrevisionales;
using Application.Mantenedores.Dtos.Bancos;
using Application.Mantenedores.Dtos.Categorias;
using Application.Mantenedores.Dtos.TiposDocumento;
using Application.Mantenedores.Dtos.EntidadPrevisionals;
using Application.Mantenedores.Dtos.GrupoConceptos;
using Application.Mantenedores.Dtos.Pensiones;
using Application.Mantenedores.Dtos.Projects;
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
            CreateMap<TipoDocumento, TipoDocumentoDto>().ReverseMap();
            CreateMap<TipoDocumento, TipoDocumentoSaveDto>().ReverseMap();

            // Banco
            CreateMap<Banco, BancoDto>().ReverseMap();
            CreateMap<Banco, BancoSaveDto>().ReverseMap();

            //Proyecto
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, ProjectSaveDto>().ReverseMap();


            //Rol 
            CreateMap<Rol , RoleDto>().ReverseMap();
            CreateMap<Rol , RoleSaveDto>().ReverseMap();


            // categoria
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaSaveDto>().ReverseMap();

            // RegimenPrevisional
            CreateMap<RegimenPrevisional, RegimenPrevisionalDto>().ReverseMap();
            CreateMap<RegimenPrevisional, RegimenPrevisionalSaveDto>().ReverseMap();




        }
    }
}
