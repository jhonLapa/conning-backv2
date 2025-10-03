using Application.PagoVentaCreditos.Dto;
using AutoMapper;

namespace Application.PagoVentaCreditos.Dtos.Profiles
{
    public class PagoVentaCreditoProfile : Profile
    {
        public PagoVentaCreditoProfile()
        {
            // PagoVentaCredito
            CreateMap<Domain.PagoVentaCredito, PagoVentaCreditoDto>().ReverseMap();
            CreateMap<Domain.PagoVentaCredito, PagoVentaCreditoSaveDto>().ReverseMap();

        }
    }
}
