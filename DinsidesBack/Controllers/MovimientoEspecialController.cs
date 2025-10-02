using Application.Mantenedores.Dtos.Afectacions;
using Application.Mantenedores.Dtos.MovimientosEspeciales;
using Application.Mantenedores.Services;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoEspecialController : ControllerBase
    {
        private readonly IMovimientoEspecialService _movimientoEspecialService;
        public MovimientoEspecialController(IMovimientoEspecialService movimientoEspecialService) => _movimientoEspecialService = movimientoEspecialService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<MovimientoEspecialDto>>>> Get()
        {

            var response = await _movimientoEspecialService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<MovimientoEspecialDto>>> Get(int id)
        {
            var response = await _movimientoEspecialService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MovimientoEspecialDto>>>> Post([FromBody] MovimientoEspecialSaveDto request)
        {

            var response = await _movimientoEspecialService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MovimientoEspecialDto>>>> Put(int id, [FromBody] MovimientoEspecialSaveDto request)
        {

            var response = await _movimientoEspecialService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<MovimientoEspecialDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _movimientoEspecialService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MovimientoEspecialDto>>>> Delete(int id)
        {
            var response = await _movimientoEspecialService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }
    }
}
