
using Application.Mantenedores.Dtos.Afectacions;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfectacionController : ControllerBase
    {
        private readonly IAfectacionService _afectacionService;
        public AfectacionController(IAfectacionService afectacionService) => _afectacionService = afectacionService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<AfectacionDto>>>> Get()
        {

            var response = await _afectacionService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<AfectacionDto>>> Get(int id)
        {
            var response = await _afectacionService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AfectacionDto>>>> Post([FromBody] AfectacionSaveDto request)
        {

            var response = await _afectacionService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<AfectacionDto>>>> Put(int id, [FromBody] AfectacionSaveDto request)
        {

            var response = await _afectacionService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
