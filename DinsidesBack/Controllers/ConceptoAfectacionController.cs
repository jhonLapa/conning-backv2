
using Application.ConceptoAfectacions.Dto;
using Application.ConceptoAfectacions.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptoAfectacionController : ControllerBase
    {
        private readonly IConceptoAfectacionServices _ConceptoAfectacionService;
        public ConceptoAfectacionController(IConceptoAfectacionServices ConceptoAfectacionService) => _ConceptoAfectacionService = ConceptoAfectacionService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<ConceptoAfectacionDto>>>> Get()
        {

            var response = await _ConceptoAfectacionService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<ConceptoAfectacionDto>>> Get(int id)
        {
            var response = await _ConceptoAfectacionService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ConceptoAfectacionDto>>>> Post([FromBody] ConceptoAfectacionSaveDto request)
        {

            var response = await _ConceptoAfectacionService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ConceptoAfectacionDto>>>> Put(int id, [FromBody] ConceptoAfectacionSaveDto request)
        {

            var response = await _ConceptoAfectacionService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPost("save-array")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveArray([FromBody] List<ConceptoAfectacionSaveDto> dtos)
        {
            var result = await _ConceptoAfectacionService.SaveArrayAsync(dtos);
            return Ok(result);
        }

        [HttpGet("afectacion/{idConcepto}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<ConceptoAfectacionDto>>>> AfectacionesMasivo(int idConcepto)
        {

            var response = await _ConceptoAfectacionService.FechtByIdConceptos(idConcepto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

    }
}
