using Application.EntidadComisiones.Dto;
using Application.EntidadComisiones.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadComisionController : ControllerBase
    {
        private readonly IEntidadComisionServices _entidadComisionService;
        public EntidadComisionController(IEntidadComisionServices EntidadComisionService) => _entidadComisionService = EntidadComisionService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<EntidadComisionDto>>>> Get()
        {

            var response = await _entidadComisionService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<EntidadComisionDto>>> Get(int id)
        {
            var response = await _entidadComisionService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EntidadComisionDto>>>> Post([FromBody] EntidadComisionSaveDto request)
        {

            var response = await _entidadComisionService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EntidadComisionDto>>>> Put(int id, [FromBody] EntidadComisionSaveDto request)
        {

            var response = await _entidadComisionService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
