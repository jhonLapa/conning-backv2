
using Application.Mantenedores.Dtos.EntidadPrevisionals;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntidadPrevisionalController : ControllerBase
    {
        private readonly IEntidadPrevisionalService _entidadPrevisionalService;
        public EntidadPrevisionalController(IEntidadPrevisionalService entidadPrevisionalService) => _entidadPrevisionalService = entidadPrevisionalService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<EntidadPrevisionalDto>>>> Get()
        {

            var response = await _entidadPrevisionalService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<EntidadPrevisionalDto>>> Get(int id)
        {
            var response = await _entidadPrevisionalService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EntidadPrevisionalDto>>>> Post([FromBody] EntidadPrevisionalSaveDto request)
        {

            var response = await _entidadPrevisionalService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EntidadPrevisionalDto>>>> Put(int id, [FromBody] EntidadPrevisionalSaveDto request)
        {

            var response = await _entidadPrevisionalService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<EntidadPrevisionalDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _entidadPrevisionalService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EntidadPrevisionalDto>>>> Delete(int id)
        {
            var response = await _entidadPrevisionalService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

    }
}
