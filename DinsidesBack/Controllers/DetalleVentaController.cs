using Application.DetalleVentas.Dto;
using Application.DetalleVentas.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly IDetalleVentaServices _detalleVentaService;
        public DetalleVentaController(IDetalleVentaServices DetalleVentaService) => _detalleVentaService = DetalleVentaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<DetalleVentaDto>>>> Get()
        {

            var response = await _detalleVentaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<DetalleVentaDto>>, Ok<OperationResult<DetalleVentaDto>>>> Get(int id)
        {
            var response = await _detalleVentaService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<DetalleVentaDto>
                {
                    Data = null,
                    Message = $"No se encontró ningun dato con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<DetalleVentaDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DetalleVentaDto>>>> Post([FromBody] DetalleVentaSaveDto request)
        {

            var response = await _detalleVentaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DetalleVentaDto>>>> Put(int id, [FromBody] DetalleVentaSaveDto request)
        {

            var response = await _detalleVentaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

     

        [HttpGet("venta/{idVenta}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<DetalleVentaDto>>>,
            Ok<OperationResult<List<DetalleVentaDto>>>>> ObtenerPorVentaAsync(int idVenta)
        {
            var result = await _detalleVentaService.ObtenerPorVentaAsync(idVenta);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }


        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<DetalleVentaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _detalleVentaService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DetalleVentaDto>>>> Delete(int id)
        {
            var response = await _detalleVentaService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }


    }
}
