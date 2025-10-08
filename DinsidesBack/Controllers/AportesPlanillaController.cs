using Application.AportesPlanillas.Dto;
using Application.AportesPlanillas.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AportesPlanillaController : ControllerBase
    {
        private readonly IAportesPlanillaServices _aportesPlanillaService;
        public AportesPlanillaController(IAportesPlanillaServices AportesPlanillaService) => _aportesPlanillaService = AportesPlanillaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<AportesPlanillaDto>>>> Get()
        {

            var response = await _aportesPlanillaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<AportesPlanillaDto>>, Ok<OperationResult<AportesPlanillaDto>>>> Get(int id)
        {
            var response = await _aportesPlanillaService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<AportesPlanillaDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna aportesPlanilla con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<AportesPlanillaDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AportesPlanillaDto>>>> Post([FromBody] AportesPlanillaSaveDto request)
        {

            var response = await _aportesPlanillaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AportesPlanillaDto>>>> Put(int id, [FromBody] AportesPlanillaSaveDto request)
        {

            var response = await _aportesPlanillaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<AportesPlanillaSelectDto>>>> SelSelectActivoect()
        {

            var response = await _aportesPlanillaService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<AportesPlanillaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _aportesPlanillaService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AportesPlanillaDto>>>> Delete(int id)
        {
            var response = await _aportesPlanillaService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

    }
}