using Application.Mantenedores.Dtos.Planillas;
using Application.Planillas.Dto;
using Application.Planillas.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanillaController : ControllerBase
    {
        private readonly IPlanillaServices _planillaService;
        public PlanillaController(IPlanillaServices PlanillaService) => _planillaService = PlanillaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<PlanillaDto>>>> Get()
        {

            var response = await _planillaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<PlanillaDto>>, Ok<OperationResult<PlanillaDto>>>> Get(int id)
        {
            var response = await _planillaService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<PlanillaDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna planilla con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<PlanillaDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PlanillaDto>>>> Post([FromBody] PlanillaSaveDto request)
        {

            var response = await _planillaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PlanillaDto>>>> Put(int id, [FromBody] PlanillaSaveDto request)
        {

            var response = await _planillaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<PlanillaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _planillaService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PlanillaDto>>>> Delete(int id)
        {
            var response = await _planillaService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }


        [HttpPost("RegistroCompleto")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PlanillaDto>>>> PostCompleta([FromBody] PlanillaFormDataDto request)
        {
            if (request == null)
            {
                return TypedResults.BadRequest();
            }

            var response = await _planillaService.CreatePlanillaCompletaAsync(request);
            if (response != null) return TypedResults.Ok(response);

            return TypedResults.Ok(response);
        }


        [HttpGet("BusquedaPaginadoProyectoTrabajador")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<PlanillaDto>>>> BusquedaPaginadoProyectoTrabajador([FromQuery] PaginationRequest dto, int idTrabajador, int idProyecto)
        {
            var response = await _planillaService.BusquedaPaginadoProyectoTrabajador(dto, idTrabajador, idProyecto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


    }
}
