using Application.DetalleCompras.Dto;
using Application.DetalleCompras.Dto;
using Application.DetalleCompras.Dto;
using Application.DetalleCompras.Services.Interfaces;
using Application.DetalleCompras.Dto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleCompraController : ControllerBase
    {
        private readonly IDetalleCompraServices _detalleCompraService;
        public DetalleCompraController(IDetalleCompraServices DetalleCompraService) => _detalleCompraService = DetalleCompraService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<DetalleCompraDto>>>> Get()
        {

            var response = await _detalleCompraService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<DetalleCompraDto>>, Ok<OperationResult<DetalleCompraDto>>>> Get(int id)
        {
            var response = await _detalleCompraService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<DetalleCompraDto>
                {
                    Data = null,
                    Message = $"No se encontró ningun dato con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<DetalleCompraDto>
            {
                Data = response,
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DetalleCompraDto>>>> Post([FromBody] DetalleCompraSaveDto request)
        {

            var response = await _detalleCompraService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DetalleCompraDto>>>> Put(int id, [FromBody] DetalleCompraSaveDto request)
        {

            var response = await _detalleCompraService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("compra/{idCompra}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<DetalleCompraDto>>>,
            Ok<OperationResult<List<DetalleCompraDto>>>>> ObtenerPorCompraAsync(int idCompra)
        {
            var result = await _detalleCompraService.ObtenerPorCompraAsync(idCompra);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }


        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<DetalleCompraDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _detalleCompraService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DetalleCompraDto>>>> Delete(int id)
        {
            var response = await _detalleCompraService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

    }
}
