using Application.Compras.Dto;
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
        }
    }
}

