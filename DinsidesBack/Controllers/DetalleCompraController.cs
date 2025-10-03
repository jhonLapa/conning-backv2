using Application.DetalleCompras.Dto;
using Application.DetalleCompras.Services.Interfaces;
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
        private readonly IDetalleCompraService _detalleCompraService;
        public DetalleCompraController(IDetalleCompraService detalleCompraService) => _detalleCompraService = detalleCompraService;

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
        public async Task<Results<BadRequest, Ok<DetalleCompraDto>>> Get(int id)
        {
            var response = await _detalleCompraService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

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

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<DetalleCompraDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _detalleCompraService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        } 
    }
}
