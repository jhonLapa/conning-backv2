using Application.Mantenedores.Dtos.AportesEmpleadores;
using Application.Mantenedores.Dtos.Bancos;
using Application.Mantenedores.Dtos.Categorias;
using Application.Mantenedores.Dtos.Clientes;
using Application.Mantenedores.Dtos.MovimientosEspeciales;
using Application.Mantenedores.Dtos.Proyectos;
using Application.Mantenedores.Dtos.RegimenesPrevisionales;
using Application.Mantenedores.Dtos.Roles;
using Application.Mantenedores.Dtos.TiposComprobantes;
using Application.Mantenedores.Dtos.TiposDocumento;
using Application.Mantenedores.Dtos.Trabajadores;
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
            CreateMap<TipoDocumento, TipoDocumentoSelectDto>().ReverseMap();

            // Banco
            CreateMap<Banco, BancoDto>().ReverseMap();
            CreateMap<Banco, BancoSaveDto>().ReverseMap();
            CreateMap<Banco, BancoSelectDto>().ReverseMap();

            //Proyecto
            CreateMap<Proyecto, ProyectoDto>().ReverseMap();
            CreateMap<Proyecto, ProyectoSaveDto>().ReverseMap();
            CreateMap<Proyecto, ProyectoSelectDto>().ReverseMap();

            //Rol 
            CreateMap<Rol , RoleDto>().ReverseMap();
            CreateMap<Rol , RoleSaveDto>().ReverseMap();


            // categoria
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaSaveDto>().ReverseMap();
            CreateMap<Categoria, CategoriaSelectDto>().ReverseMap();

            // RegimenPrevisional
            CreateMap<RegimenPrevisional, RegimenPrevisionalDto>().ReverseMap();
            CreateMap<RegimenPrevisional, RegimenPrevisionalSaveDto>().ReverseMap();
            CreateMap<RegimenPrevisional, RegimenPrevisionalSelectDto>().ReverseMap();


            // De entidad a DTO
            CreateMap<AportesEmpleador, AportesEmpleadorDto>().ReverseMap();
            CreateMap<AportesEmpleador, AportesEmpleadorSaveDto>().ReverseMap();
            CreateMap<AportesEmpleador, AportesEmpleadorSelectDto>().ReverseMap();

            // De entidad a DTO
            CreateMap<TipoComprobante, TipoComprobanteDto>().ReverseMap();
            CreateMap<TipoComprobante, TipoComprobanteSaveDto>().ReverseMap();
            CreateMap<TipoComprobante, TipoComprobanteSelectDto>().ReverseMap();

            // De entidad a DTO
            CreateMap<MovimientoEspecial, MovimientoEspecialDto>().ReverseMap();
            CreateMap<MovimientoEspecial, MovimientoEspecialSaveDto>().ReverseMap();
            CreateMap<MovimientoEspecial, MovimientoEspecialSelectDto>().ReverseMap();

            // Grupo Cliente
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Cliente, ClienteSaveDto>().ReverseMap();
            CreateMap<Cliente, ClienteSelectDto>().ReverseMap();

            // Grupo Trabajador
            CreateMap<Trabajador, TrabajadorDto>().ReverseMap();
            CreateMap<Trabajador, TrabajadorSaveDto>().ReverseMap();
            CreateMap<Trabajador, TrabajadorSelectDto>().ReverseMap();
        }
    }
}
