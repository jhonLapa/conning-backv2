using Application.PagoCompraCreditos.Dto;
using AutoMapper;

namespace Application.PagoCompraCreditos.Dtos.Profiles
{
    public class PagoCompraCreditoProfile : Profile
    {
        public PagoCompraCreditoProfile()
        {
            // PagoCompraCredito
            CreateMap<Domain.PagoCompraCredito, PagoCompraCreditoDto>().ReverseMap();
            CreateMap<Domain.PagoCompraCredito, PagoCompraCreditoSaveDto>().ReverseMap();

        }
    }
}
