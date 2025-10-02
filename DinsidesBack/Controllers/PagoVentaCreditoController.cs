using Application.PagoVentaCreditos.Dto;
using Application.PagoVentaCreditos.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoVentaCreditoController : ControllerBase
    {
        private readonly IPagoVentaCreditoServices _pagoVentaCreditoService;
        public PagoVentaCreditoController(IPagoVentaCreditoServices PagoVentaCreditoService) => _pagoVentaCreditoService = PagoVentaCreditoService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<PagoVentaCreditoDto>>>> Get()
        {

            var response = await _pagoVentaCreditoService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<PagoVentaCreditoDto>>, Ok<OperationResult<PagoVentaCreditoDto>>>> Get(int id)
        {
            var response = await _pagoVentaCreditoService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<PagoVentaCreditoDto>
                {
                    Data = null,
                    Message = $"No se encontró ningun dato con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<PagoVentaCreditoDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PagoVentaCreditoDto>>>> Post([FromBody] PagoVentaCreditoSaveDto request)
        {

            var response = await _pagoVentaCreditoService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PagoVentaCreditoDto>>>> Put(int id, [FromBody] PagoVentaCreditoSaveDto request)
        {

            var response = await _pagoVentaCreditoService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }



        [HttpGet("venta/{idVenta}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<PagoVentaCreditoDto>>>,
            Ok<OperationResult<List<PagoVentaCreditoDto>>>>> ObtenerPorVentaAsync(int idVenta)
        {
            var result = await _pagoVentaCreditoService.ObtenerPorVentaAsync(idVenta);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }


        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<PagoVentaCreditoDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _pagoVentaCreditoService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PagoVentaCreditoDto>>>> Delete(int id)
        {
            var response = await _pagoVentaCreditoService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }


    }
}
