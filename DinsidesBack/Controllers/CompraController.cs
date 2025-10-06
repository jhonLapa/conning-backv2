using Application.Compras.Dto;
using Application.Compras.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraServices _compraService;
        public CompraController(ICompraServices CompraService) => _compraService = CompraService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<CompraDto>>>> Get()
        {

            var response = await _compraService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<CompraDto>>, Ok<OperationResult<CompraDto>>>> Get(int id)
        {
            var response = await _compraService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<CompraDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna compra con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<CompraDto>
            {
                Data = response,
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CompraDto>>>> Post([FromBody] CompraSaveDto request)
        {

            var response = await _compraService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CompraDto>>>> Put(int id, [FromBody] CompraSaveDto request)
        {

            var response = await _compraService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<CompraSelectDto>>>> SelSelectActivoect()
        {

            var response = await _compraService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("proveedor/{proveedorId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<CompraDto>>>,
            Ok<OperationResult<List<CompraDto>>>>> GetByProveedorId(int proveedorId)
        {
            var result = await _compraService.FindByProveedorIdAsync(proveedorId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<CompraDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _compraService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CompraDto>>>> Delete(int id)
        {
            var response = await _compraService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost("RegistrarCompleto")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CompraDto>>>> PostCompleto([FromBody] CompraCompletoSaveDto request)
        {
            var response = await _compraService.CreateWithDetailsAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
