
using Application.Mantenedores.Dtos.Categorias;
using Application.Mantenedores.Dtos.TiposComprobantes;
using Application.Mantenedores.Services;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService banckService) => _categoriaService = banckService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<CategoriaDto>>>> Get()
        {

            var response = await _categoriaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<CategoriaDto>>> Get(int id)
        {
            var response = await _categoriaService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CategoriaDto>>>> Post([FromBody] CategoriaSaveDto request)
        {

            var response = await _categoriaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CategoriaDto>>>> Put(int id, [FromBody] CategoriaSaveDto request)
        {

            var response = await _categoriaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CategoriaDto>>>> Delete(int id)
        {
            var response = await _categoriaService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<CategoriaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _categoriaService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpGet("SelectActivos")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<CategoriaSelectDto>>>> SelectActivo()
        {

            var response = await _categoriaService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }





    }
}
