using Application.DetallePlanillas.Dto;
using Application.DetallePlanillas.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePlanillaController : ControllerBase
    {
        private readonly IDetallePlanillaServices _detallePlanillaService;
        public DetallePlanillaController(IDetallePlanillaServices DetallePlanillaService) => _detallePlanillaService = DetallePlanillaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<DetallePlanillaDto>>>> Get()
        {

            var response = await _detallePlanillaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<DetallePlanillaDto>>, Ok<OperationResult<DetallePlanillaDto>>>> Get(int id)
        {
            var response = await _detallePlanillaService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<DetallePlanillaDto>
                {
                    Data = null,
                    Message = $"No se encontró ningun dato con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<DetallePlanillaDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DetallePlanillaDto>>>> Post([FromBody] DetallePlanillaSaveDto request)
        {

            var response = await _detallePlanillaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DetallePlanillaDto>>>> Put(int id, [FromBody] DetallePlanillaSaveDto request)
        {

            var response = await _detallePlanillaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }



        [HttpGet("Planilla/{idPlanilla}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<DetallePlanillaDto>>>,
            Ok<OperationResult<List<DetallePlanillaDto>>>>> ObtenerPorPlanillaAsync(int idPlanilla)
        {
            var result = await _detallePlanillaService.ObtenerPorPlanillaAsync(idPlanilla);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }

        [HttpGet("TrabajadorProyecto/{idTrabajadorProyecto}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<DetallePlanillaDto>>>,
            Ok<OperationResult<List<DetallePlanillaDto>>>>> ObtenerPorTrabajadorProyectoAsync(int idTrabajadorProyecto)
        {
            var result = await _detallePlanillaService.ObtenerPorTrabajadorProyectoAsync(idTrabajadorProyecto);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }


        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<DetallePlanillaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _detallePlanillaService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DetallePlanillaDto>>>> Delete(int id)
        {
            var response = await _detallePlanillaService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }


    }
}
