using Application.Mantenedores.Dtos.Afectacions;
using Application.Mantenedores.Dtos.Banks;
using Application.Mantenedores.Dtos.Categorys;
using Application.Mantenedores.Dtos.DocumentTypes;
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
            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
            CreateMap<DocumentType, DocumentTypeSaveDto>().ReverseMap();

            // Banco
            CreateMap<Bank, BankDto>().ReverseMap();
            CreateMap<Bank, BankSaveDto>().ReverseMap();

            //Pension 
            CreateMap<Pension, PensionDto>().ReverseMap();
            CreateMap<Pension, PensionSaveDto>().ReverseMap();

            //Proyecto
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, ProjectSaveDto>().ReverseMap();


            //Rol 
            CreateMap<Rol , RoleDto>().ReverseMap();
            CreateMap<Rol , RoleSaveDto>().ReverseMap();


            // categoria
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategorySaveDto>().ReverseMap();

            // Afectacion
            CreateMap<Afectacion, AfectacionDto>().ReverseMap();
            CreateMap<Afectacion, AfectacionSaveDto>().ReverseMap();

            // Grupo Concepto
            CreateMap<GrupoConcepto, GrupoConceptoDto>().ReverseMap();
            CreateMap<GrupoConcepto, GrupoConceptoSaveDto>().ReverseMap();
        }
    }
}
