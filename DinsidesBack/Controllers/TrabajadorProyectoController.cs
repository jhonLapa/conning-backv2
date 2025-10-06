using Application.TrabajadorProyectos.Dto;
using Application.TrabajadorProyectos.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadorProyectoController : ControllerBase
    {
        private readonly ITrabajadorProyectoServices _trabajadorProyectoService;
        public TrabajadorProyectoController(ITrabajadorProyectoServices TrabajadorProyectoService) => _trabajadorProyectoService = TrabajadorProyectoService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TrabajadorProyectoDto>>>> Get()
        {

            var response = await _trabajadorProyectoService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<TrabajadorProyectoDto>>, Ok<OperationResult<TrabajadorProyectoDto>>>> Get(int id)
        {
            var response = await _trabajadorProyectoService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<TrabajadorProyectoDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna trabajadorProyecto con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<TrabajadorProyectoDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorProyectoDto>>>> Post([FromBody] TrabajadorProyectoSaveDto request)
        {

            var response = await _trabajadorProyectoService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorProyectoDto>>>> Put(int id, [FromBody] TrabajadorProyectoSaveDto request)
        {

            var response = await _trabajadorProyectoService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TrabajadorProyectoSelectDto>>>> SelSelectActivoect()
        {

            var response = await _trabajadorProyectoService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<TrabajadorProyectoDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _trabajadorProyectoService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorProyectoDto>>>> Delete(int id)
        {
            var response = await _trabajadorProyectoService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

    }
}