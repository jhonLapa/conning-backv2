using Application.PagoVentaCreditos.Dto;
using AutoMapper;
using Domain;

namespace Application.PagoVentaCreditos.Dtos.Profiles
{
    public class PagoVentaCreditoProfile : Profile
    {
        public PagoVentaCreditoProfile()
        {
            // PagoVentaCredito
            CreateMap<PagoVentaCredito, PagoVentaCreditoDto>().ReverseMap();
            CreateMap<PagoVentaCredito, PagoVentaCreditoSaveDto>().ReverseMap();

        }
    }
}
