using Application.Exceptions;
using Application.Usuarios.Dto;
using Application.Usuarios.Services.Interface;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Usuarios.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        public UserService(
            IMapper mapper,
            IUsuarioRepositorio usuarioRepositorio, 
            IConfiguration configuration, 
            ILogger<UserService> logger)
        {
            _mapper = mapper;
            _usuarioRepositorio = usuarioRepositorio;
            _configuration = configuration;
            _logger = logger;
        }

 
        public async Task<IReadOnlyList<UserDto>> FindAllAsync()
        {
            var response = await _usuarioRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<UserDto>>(response);
        }

        public async Task<UserDto> FindByIdAsync(int id)
        {
            var response = await _usuarioRepositorio.FindByIdAsync(id);

            return  _mapper.Map<UserDto>(response);
        }


        
       
    }
}
