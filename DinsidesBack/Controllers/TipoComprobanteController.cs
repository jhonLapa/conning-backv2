using Application.Mantenedores.Dtos.TiposComprobantes;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoComprobanteController : ControllerBase
    {
        private readonly ITipoComprobanteService _tipoComprobanteService;
        public TipoComprobanteController(ITipoComprobanteService tipoComprobanteService) => _tipoComprobanteService = tipoComprobanteService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TipoComprobanteDto>>>> Get()
        {

            var response = await _tipoComprobanteService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<TipoComprobanteDto>>> Get(int id)
        {
            var response = await _tipoComprobanteService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TipoComprobanteDto>>>> Post([FromBody] TipoComprobanteSaveDto request)
        {

            var response = await _tipoComprobanteService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TipoComprobanteDto>>>> Put(int id, [FromBody] TipoComprobanteSaveDto request)
        {

            var response = await _tipoComprobanteService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<TipoComprobanteDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _tipoComprobanteService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TipoComprobanteDto>>>> Delete(int id)
        {
            var response = await _tipoComprobanteService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpGet("SelectActivos")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TipoComprobanteSelectDto>>>> SelectActivo()
        {

            var response = await _tipoComprobanteService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

    }
}
