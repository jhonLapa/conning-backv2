using Application.Ventas.Dto;
using AutoMapper;

namespace Application.Ventas.Dtos.Profiles
{
    public class VentaProfile : Profile
    {
        public VentaProfile()
        {
            // Venta
            CreateMap<Domain.Venta, VentaDto>().ReverseMap();
            CreateMap<Domain.Venta, VentaSaveDto>().ReverseMap();
            CreateMap<Domain.Venta, VentaSelectDto>().ReverseMap();
            CreateMap<Domain.Venta, VentaCompletoSaveDto>().ReverseMap();

            // DetalleVenta <-> DetallesVentaSaveDto
            CreateMap<Domain.DetalleVenta, DetallesVentaSaveDto>().ReverseMap();

            // PagoVentaCredito <-> PagosVentaCreditoSaveDto
            CreateMap<Domain.PagoVentaCredito, PagosVentaCreditoSaveDto>().ReverseMap();
        }
    }
}
