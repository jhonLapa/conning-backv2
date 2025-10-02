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
            CreateMap<Domain.DetalleVenta, DetalleVentaDto>().ReverseMap();
            CreateMap<Domain.DetalleVenta, DetalleVentaSaveDto>().ReverseMap();

        }
    }
}
