using Application.Mantenedores.Dtos.Documentos;
using Application.Mantenedores.Dtos.Proyectos;
using Application.Mantenedores.Services;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoService _proyectoService;

        public ProyectoController(IProyectoService proyectoService) => _proyectoService = proyectoService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<ProyectoDto>>>> Get()
        {

            var response = await _proyectoService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<ProyectoDto>>> Get(int id)
        {
            var response = await _proyectoService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ProyectoDto>>>> Post([FromBody] ProyectoSaveDto request)
        {

            var response = await _proyectoService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ProyectoDto>>>> Put(int id, [FromBody] ProyectoSaveDto request)
        {

            var response = await _proyectoService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

    }
}
