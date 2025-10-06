using Application.Ventas.Dto;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProyectoEncargados.Dto.Profiles
{
    public class ProyectoEncargadoProfile :Profile
    {
        public ProyectoEncargadoProfile()
        {
            // ProyectoEncargado básica
            CreateMap<ProyectoEncargado, ProyectoEncargadoDto>().ReverseMap();
            CreateMap<ProyectoEncargado, ProyectoEncargadoSaveDto>().ReverseMap();
            CreateMap<ProyectoEncargado, ProyectoEncargadoSelectDto>().ReverseMap();
        }
    }
}
