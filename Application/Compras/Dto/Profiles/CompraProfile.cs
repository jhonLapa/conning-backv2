using Application.Compras.Dto;
using Application.DetalleCompras.Dto;
using AutoMapper;
using Domain;

namespace Application.Compras.Dtos.Profiles
{
    public class CompraProfile : Profile
    {
        public CompraProfile()
        {
            // Compra
            CreateMap<Compra, CompraDto>().ReverseMap();
            CreateMap<Compra, CompraSaveDto>().ReverseMap();
            CreateMap<Compra, CompraSelectDto>().ReverseMap();
            CreateMap<Compra, CompraCompletoSaveDto>().ReverseMap();

            // DetalleCompra <-> DetallesCompraSaveDto
            CreateMap<DetalleCompra, DetalleCompraSaveDto>().ReverseMap();

            // PagoCompraCredito <-> PagosCompraCreditoSaveDto
            CreateMap<Domain.PagoCompraCredito, PagosCompraCreditoSaveDto>().ReverseMap();
        }
    }
}

