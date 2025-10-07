using Application.AportesSindicatos.Dto;
using Application.AportesSindicatos.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AportesSindicatoController : ControllerBase
    {
        private readonly IAportesSindicatoServices _aportesSindicatoService;
        public AportesSindicatoController(IAportesSindicatoServices AportesSindicatoService) => _aportesSindicatoService = AportesSindicatoService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<AportesSindicatoDto>>>> Get()
        {

            var response = await _aportesSindicatoService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<AportesSindicatoDto>>, Ok<OperationResult<AportesSindicatoDto>>>> Get(int id)
        {
            var response = await _aportesSindicatoService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<AportesSindicatoDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna aportesSindicato con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<AportesSindicatoDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AportesSindicatoDto>>>> Post([FromBody] AportesSindicatoSaveDto request)
        {

            var response = await _aportesSindicatoService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AportesSindicatoDto>>>> Put(int id, [FromBody] AportesSindicatoSaveDto request)
        {

            var response = await _aportesSindicatoService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<AportesSindicatoSelectDto>>>> SelSelectActivoect()
        {

            var response = await _aportesSindicatoService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<AportesSindicatoDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _aportesSindicatoService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AportesSindicatoDto>>>> Delete(int id)
        {
            var response = await _aportesSindicatoService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

    }
}
