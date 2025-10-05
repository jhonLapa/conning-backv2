using Application.PagoCompraCreditos.Dto;
using AutoMapper;
using Domain;

namespace Application.PagoCompraCreditos.Dtos.Profiles
{
    public class PagoCompraCreditoProfile : Profile
    {
        public PagoCompraCreditoProfile()
        {
            // PagoCompraCredito
            CreateMap<PagoCompraCredito, PagoCompraCreditoDto>().ReverseMap();
            CreateMap<PagoCompraCredito, PagoCompraCreditoSaveDto>().ReverseMap();

        }
    }
}
