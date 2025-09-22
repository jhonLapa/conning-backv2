using Application.Mantenedores.Dtos.GrupoConceptos;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoConceptoController : ControllerBase
    {
        private readonly IGrupoConceptoService _grupoConceptoService;
        public GrupoConceptoController(IGrupoConceptoService grupoConceptoService) => _grupoConceptoService = grupoConceptoService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<GrupoConceptoDto>>>> Get()
        {

            var response = await _grupoConceptoService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<GrupoConceptoDto>>> Get(int id)
        {
            var response = await _grupoConceptoService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<GrupoConceptoDto>>>> Post([FromBody] GrupoConceptoSaveDto request)
        {

            var response = await _grupoConceptoService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<GrupoConceptoDto>>>> Put(int id, [FromBody] GrupoConceptoSaveDto request)
        {

            var response = await _grupoConceptoService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
