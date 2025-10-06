using Application.Ventas.Dto;
using AutoMapper;
using Domain;

namespace Application.Ventas.Dtos.Profiles
{
    public class VentaProfile : Profile
    {
        public VentaProfile()
        {
            // Venta básica
            CreateMap<Venta, VentaDto>().ReverseMap();
            CreateMap<Venta, VentaSaveDto>().ReverseMap();
            CreateMap<Venta, VentaSelectDto>().ReverseMap();

            // ⚙️ VentaCompletoSaveDto <-> Venta
            // Se ignoran las colecciones Detalles y PagosCredito para evitar duplicados
            CreateMap<VentaCompletoSaveDto, Venta>()
                .ForMember(dest => dest.Detalles, opt => opt.Ignore())
                .ForMember(dest => dest.PagosCredito, opt => opt.Ignore())
                .ReverseMap();

            // ✅ DetalleVenta <-> DetallesVentaSaveDto
            CreateMap<DetalleVenta, DetallesVentaSaveDto>().ReverseMap();

            // ✅ PagoVentaCredito <-> PagosVentaCreditoSaveDto
            CreateMap<PagoVentaCredito, PagosVentaCreditoSaveDto>().ReverseMap();
        }
    }
}
