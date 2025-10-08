using Application.ProyectoEncargados.Dto;
using Application.ProyectoEncargados.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoEncargadoController : ControllerBase
    {
        private readonly IProyectoEncargadoServices _proyectoEncargadoService;
        public ProyectoEncargadoController(IProyectoEncargadoServices ProyectoEncargadoService) => _proyectoEncargadoService = ProyectoEncargadoService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<ProyectoEncargadoDto>>>> Get()
        {

            var response = await _proyectoEncargadoService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<ProyectoEncargadoDto>>, Ok<OperationResult<ProyectoEncargadoDto>>>> Get(int id)
        {
            var response = await _proyectoEncargadoService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<ProyectoEncargadoDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna proyectoEncargado con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<ProyectoEncargadoDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ProyectoEncargadoDto>>>> Post([FromBody] ProyectoEncargadoSaveDto request)
        {

            var response = await _proyectoEncargadoService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ProyectoEncargadoDto>>>> Put(int id, [FromBody] ProyectoEncargadoSaveDto request)
        {

            var response = await _proyectoEncargadoService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<ProyectoEncargadoSelectDto>>>> SelSelectActivoect()
        {

            var response = await _proyectoEncargadoService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<ProyectoEncargadoDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _proyectoEncargadoService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ProyectoEncargadoDto>>>> Delete(int id)
        {
            var response = await _proyectoEncargadoService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

    }
}