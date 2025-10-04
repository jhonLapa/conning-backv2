using Application.PagoCompraCreditos.Dto;
using Application.PagoCompraCreditos.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoCompraCreditoController : ControllerBase
    {
        private readonly IPagoCompraCreditoServices _pagoCompraCreditoService;
        public PagoCompraCreditoController(IPagoCompraCreditoServices PagoCompraCreditoService) => _pagoCompraCreditoService = PagoCompraCreditoService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<PagoCompraCreditoDto>>>> Get()
        {

            var response = await _pagoCompraCreditoService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<PagoCompraCreditoDto>>, Ok<OperationResult<PagoCompraCreditoDto>>>> Get(int id)
        {
            var response = await _pagoCompraCreditoService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<PagoCompraCreditoDto>
                {
                    Data = null,
                    Message = $"No se encontró ningun dato con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<PagoCompraCreditoDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PagoCompraCreditoDto>>>> Post([FromBody] PagoCompraCreditoSaveDto request)
        {

            var response = await _pagoCompraCreditoService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PagoCompraCreditoDto>>>> Put(int id, [FromBody] PagoCompraCreditoSaveDto request)
        {

            var response = await _pagoCompraCreditoService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }



        [HttpGet("compra/{idCompra}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<PagoCompraCreditoDto>>>,
            Ok<OperationResult<List<PagoCompraCreditoDto>>>>> ObtenerPorCompraAsync(int idCompra)
        {
            var result = await _pagoCompraCreditoService.ObtenerPorCompraAsync(idCompra);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }


        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<PagoCompraCreditoDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _pagoCompraCreditoService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PagoCompraCreditoDto>>>> Delete(int id)
        {
            var response = await _pagoCompraCreditoService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }


    }
}
