using Application.Mantenedores.Dtos.Trabajadores;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadorController : ControllerBase
    {
        private readonly ITrabajadorService _trabajadorService;
        public TrabajadorController(ITrabajadorService trabajadorService) => _trabajadorService = trabajadorService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TrabajadorDto>>>> Get()
        {

            var response = await _trabajadorService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<TrabajadorDto>>> Get(int id)
        {
            var response = await _trabajadorService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorDto>>>> Post([FromBody] TrabajadorSaveDto request)
        {

            var response = await _trabajadorService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorDto>>>> Put(int id, [FromBody] TrabajadorSaveDto request)
        {

            var response = await _trabajadorService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<TrabajadorDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _trabajadorService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorDto>>>> Delete(int id)
        {
            var response = await _trabajadorService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpGet("SelectActivos")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TrabajadorSelectDto>>>> SelectActivo()
        {

            var response = await _trabajadorService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpPost("with-accounts")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorDto>>>> PostWithAccounts([FromBody] TrabajadorWithAccountsSaveDto request)
        {
            var response = await _trabajadorService.CreateOrUpdateWithAccountsAsync(request);
            if (response != null) return TypedResults.Ok(response);
            return TypedResults.BadRequest();
        }

        [HttpGet("{id}/detalle-planilla")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDetallePlanilla(int id)
        {
            var result = await _trabajadorService.GetDetallePlanillaAsync(id);

            if (result.Success == true)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("SelectByProyecto/{idProyecto}")]
        [AllowAnonymous]
        public async Task<IActionResult> SelectByProyecto(int idProyecto)
        {
            var response = await _trabajadorService.SelectByProyecto(idProyecto);

            if (response != null)
                return Ok(response);

            return BadRequest();
        }

        [HttpGet("BusquedaPaginadoConPlanilla")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<TrabajadorDto>>>> BusquedaPaginadoConPlanilla(
                [FromQuery] PaginationRequest dto,
                DateTime? fechaInicio = null,
                DateTime? fechaFin = null)
        {
            var response = await _trabajadorService.BusquedaPaginadoConPlanilla(dto, fechaInicio, fechaFin);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


    }
}
