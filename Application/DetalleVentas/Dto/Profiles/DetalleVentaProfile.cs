using Application.DetalleVentas.Dto;
using AutoMapper;
using Domain;   // 👈 Esto faltaba

namespace Application.DetalleVentas.Dtos.Profiles
{
    public class DetalleVentaProfile : Profile
    {
        public DetalleVentaProfile()
        {
            // DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDto>().ReverseMap();
            CreateMap<DetalleVenta, DetalleVentaSaveDto>().ReverseMap();

        }
    }
}
