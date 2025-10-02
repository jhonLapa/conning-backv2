using Application.Mantenedores.Dtos.AportesEmpleadores;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AportesEmpleadorController : ControllerBase
    {
        private readonly IAportesEmpleadorService _aportesEmpleadorService;
        public AportesEmpleadorController(IAportesEmpleadorService aportesEmpleadorService) => _aportesEmpleadorService = aportesEmpleadorService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<AportesEmpleadorDto>>>> Get()
        {

            var response = await _aportesEmpleadorService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<AportesEmpleadorDto>>> Get(int id)
        {
            var response = await _aportesEmpleadorService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AportesEmpleadorDto>>>> Post([FromBody] AportesEmpleadorSaveDto request)
        {

            var response = await _aportesEmpleadorService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AportesEmpleadorDto>>>> Put(int id, [FromBody] AportesEmpleadorSaveDto request)
        {

            var response = await _aportesEmpleadorService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<AportesEmpleadorDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _aportesEmpleadorService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AportesEmpleadorDto>>>> Delete(int id)
        {
            var response = await _aportesEmpleadorService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }
    }
}
