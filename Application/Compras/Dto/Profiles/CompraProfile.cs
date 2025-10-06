using Application.Compras.Dto;
using AutoMapper;
using Domain;

namespace Application.Compras.Dtos.Profiles
{
    public class CompraProfile : Profile
    {
        public CompraProfile()
        {
            // Compra básica (para DTOs simples)
            CreateMap<Compra, CompraDto>().ReverseMap();
            CreateMap<Compra, CompraSaveDto>().ReverseMap();
            CreateMap<Compra, CompraSelectDto>().ReverseMap();

            // ⚙️ CompraCompletoSaveDto <-> Compra
            // Se ignoran las colecciones Detalles y PagosCredito para evitar duplicados al mapear
            CreateMap<CompraCompletoSaveDto, Compra>()
                .ForMember(dest => dest.Detalles, opt => opt.Ignore())
                .ForMember(dest => dest.PagosCredito, opt => opt.Ignore())
                .ReverseMap();

            // ✅ DetalleCompra <-> DetallesCompraSaveDto
            CreateMap<DetalleCompra, DetallesCompraSaveDto>().ReverseMap();

            // ✅ PagoCompraCredito <-> PagosCompraCreditoSaveDto
            CreateMap<PagoCompraCredito, PagosCompraCreditoSaveDto>().ReverseMap();
        }
    }
}
