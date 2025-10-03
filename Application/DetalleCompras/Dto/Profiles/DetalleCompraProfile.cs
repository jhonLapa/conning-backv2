using Application.DetalleCompras.Dto;
using AutoMapper;
using Domain;

namespace Application.DetalleCompras.Dtos.Profiles
{
    public class DetalleCompraProfile : Profile
    {
        public DetalleCompraProfile()
        {

            CreateMap<DetalleCompra, DetalleCompraDto>().ReverseMap();
            CreateMap<DetalleCompra, DetalleCompraSaveDto>().ReverseMap();
        }
    }
}

