using Application.Usuarios.Dto;
using Application.Usuarios.Services.Interface;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUserService _usuarioService;

        public UsuarioController(IUserService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<UserDto>>>> Put(int id, [FromBody] UserRolSaveDto request)
        {

            var response = await _usuarioService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<UserDto>>>> Post([FromBody] UserRolSaveDto request)
        {

            var response = await _usuarioService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<UserDto>>> Get(int id)
        {
            var response = await _usuarioService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<UserDto>>>> Get()
        {

            var response = await _usuarioService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
