
using Application.ConfigAfectacions.Dto;
using Application.ConfigAfectacions.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigAfectacionController : ControllerBase
    {
        private readonly IConfigAfectacionServices _configAfectacionService;
        public ConfigAfectacionController(IConfigAfectacionServices configAfectacionService) => _configAfectacionService = configAfectacionService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<ConfigAfectacionDto>>>> Get()
        {

            var response = await _configAfectacionService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<ConfigAfectacionDto>>> Get(int id)
        {
            var response = await _configAfectacionService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ConfigAfectacionDto>>>> Post([FromBody] ConfigAfectacionSaveDto request)
        {

            var response = await _configAfectacionService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<ConfigAfectacionDto>>>> Put(int id, [FromBody] ConfigAfectacionSaveDto request)
        {

            var response = await _configAfectacionService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPost("save-array")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveArray([FromBody] List<ConfigAfectacionSaveDto> dtos)
        {
            var result = await _configAfectacionService.SaveArrayAsync(dtos);
            return Ok(result);
        }
    }
}
