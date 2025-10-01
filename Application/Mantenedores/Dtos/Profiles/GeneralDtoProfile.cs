using Application.Mantenedores.Dtos.Bancos;
using Application.Mantenedores.Dtos.Categorias;
using Application.Mantenedores.Dtos.Proyectos;
using Application.Mantenedores.Dtos.RegimenesPrevisionales;
using Application.Mantenedores.Dtos.Roles;
using Application.Mantenedores.Dtos.TiposDocumento;
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
            CreateMap<Proyecto, ProyectoDto>().ReverseMap();
            CreateMap<Proyecto, ProyectoSaveDto>().ReverseMap();


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
