using Application.ConfigAfectacions.Dto;
using AutoMapper;

namespace Application.ConfigAfectacions.Dtos.Profiles
{
    public class ConfigAfectacionProfile : Profile
    {
        public ConfigAfectacionProfile()
        {
            CreateMap<Domain.ConfigAfectacion, ConfigAfectacionDto>().ReverseMap();
            CreateMap<Domain.ConfigAfectacion, ConfigAfectacionSaveDto>().ReverseMap();
        }
    }
}



