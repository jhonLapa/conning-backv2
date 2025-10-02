using Application.Ventas.Dto;
using Application.Ventas.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaServices _ventaService;
        public VentaController(IVentaServices VentaService) => _ventaService = VentaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<VentaDto>>>> Get()
        {

            var response = await _ventaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<VentaDto>>, Ok<OperationResult<VentaDto>>>> Get(int id)
        {
            var response = await _ventaService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<VentaDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna venta con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<VentaDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<VentaDto>>>> Post([FromBody] VentaSaveDto request)
        {

            var response = await _ventaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<VentaDto>>>> Put(int id, [FromBody] VentaSaveDto request)
        {

            var response = await _ventaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<VentaSelectDto>>>> SelSelectActivoect()
        {

            var response = await _ventaService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("cliente/{clienteId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<VentaDto>>>,
            Ok<OperationResult<List<VentaDto>>>>> GetByClienteId(int clienteId)
        {
            var result = await _ventaService.FindByClienteIdAsync(clienteId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }


        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<VentaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _ventaService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<VentaDto>>>> Delete(int id)
        {
            var response = await _ventaService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }


    }
}
