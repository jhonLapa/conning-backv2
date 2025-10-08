using Application.Asistencias.Dto;
using Application.Asistencias.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaServices _asistenciaService;
        public AsistenciaController(IAsistenciaServices AsistenciaService) => _asistenciaService = AsistenciaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<AsistenciaDto>>>> Get()
        {

            var response = await _asistenciaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<AsistenciaDto>>, Ok<OperationResult<AsistenciaDto>>>> Get(int id)
        {
            var response = await _asistenciaService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<AsistenciaDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna asistencia con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<AsistenciaDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AsistenciaDto>>>> Post([FromBody] AsistenciaSaveDto request)
        {

            var response = await _asistenciaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AsistenciaDto>>>> Put(int id, [FromBody] AsistenciaSaveDto request)
        {

            var response = await _asistenciaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<AsistenciaSelectDto>>>> SelSelectActivoect()
        {

            var response = await _asistenciaService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<AsistenciaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _asistenciaService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        //[HttpDelete("{id}")]
        //[AllowAnonymous]
        //public async Task<Results<BadRequest, Ok<OperationResult<AsistenciaDto>>>> Delete(int id)
        //{
        //    var response = await _asistenciaService.DisabledAsync(id);

        //    if (response != null) return TypedResults.Ok(response);

        //    return TypedResults.BadRequest();

        //}

    }
}