using Application.ConceptosCategorias.Dto;
using Application.ConceptosCategorias.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptosCategoriaController : ControllerBase
    {
        private readonly IConceptosCategoriaServices _conceptosCategoriaService;
        public ConceptosCategoriaController(IConceptosCategoriaServices ConceptosCategoriaService) => _conceptosCategoriaService = ConceptosCategoriaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<ConceptosCategoriaDto>>>> Get()
        {

            var response = await _conceptosCategoriaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<ConceptosCategoriaDto>>, Ok<OperationResult<ConceptosCategoriaDto>>>> Get(int id)
        {
            var response = await _conceptosCategoriaService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<ConceptosCategoriaDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna conceptosCategoria con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<ConceptosCategoriaDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ConceptosCategoriaDto>>>> Post([FromBody] ConceptosCategoriaSaveDto request)
        {

            var response = await _conceptosCategoriaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ConceptosCategoriaDto>>>> Put(int id, [FromBody] ConceptosCategoriaSaveDto request)
        {

            var response = await _conceptosCategoriaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<ConceptosCategoriaSelectDto>>>> SelSelectActivoect()
        {

            var response = await _conceptosCategoriaService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<ConceptosCategoriaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _conceptosCategoriaService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ConceptosCategoriaDto>>>> Delete(int id)
        {
            var response = await _conceptosCategoriaService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

    }
}