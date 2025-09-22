using Application.Auth.Dto;
using Application.Auth.Services.Interfaces;
using Application.Usuarios.Dto;
using Application.Usuarios.Services.Interface;
using DinsidesBack.Exceptions;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IJwtServices _jwtServices;
        public AuthController(IConfiguration configuration, IUserService userService, IJwtServices jwtServices)
        {
            _configuration = configuration;
            _userService = userService;
            _jwtServices = jwtServices;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperationResult<LoginDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorValidationModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        public async Task<Results<BadRequest, Ok<OperationResult<LoginDto>>>> Post([FromBody] LoginRequest userAuthDto)
        {
            var userSecurity = await _userService.LoginAsync(userAuthDto);
            return TypedResults.Ok(userSecurity);
        }
    }

}
