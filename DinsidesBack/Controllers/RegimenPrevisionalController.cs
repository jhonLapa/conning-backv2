
using Application.Mantenedores.Dtos.RegimenesPrevisionales;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegimenPrevisionalController : ControllerBase
    {
        private readonly IRegimenPrevisionalService _regimenPrevisionalService;
        public RegimenPrevisionalController(IRegimenPrevisionalService regimenPrevisionalService) => _regimenPrevisionalService = regimenPrevisionalService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<RegimenPrevisionalDto>>>> Get()
        {

            var response = await _regimenPrevisionalService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<RegimenPrevisionalDto>>> Get(int id)
        {
            var response = await _regimenPrevisionalService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<RegimenPrevisionalDto>>>> Post([FromBody] RegimenPrevisionalSaveDto request)
        {

            var response = await _regimenPrevisionalService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<RegimenPrevisionalDto>>>> Put(int id, [FromBody] RegimenPrevisionalSaveDto request)
        {

            var response = await _regimenPrevisionalService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<RegimenPrevisionalDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _regimenPrevisionalService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
        
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<RegimenPrevisionalDto>>>> Delete(int id)
        {
            var response = await _regimenPrevisionalService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

    }
}
