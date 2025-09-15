using Application.Mantenedores.Dtos.Bancks;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanckController : ControllerBase
    {
        private readonly IBanckService _banckService;
        public BanckController(IBanckService banckService) => _banckService = banckService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<BanckDto>>>> Get()
        {

            var response = await _banckService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<BanckDto>>> Get(int id)
        {
            var response = await _banckService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<BanckDto>>>> Post([FromBody] BanckSaveDto request)
        {

            var response = await _banckService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<BanckDto>>>> Put(int id, [FromBody] BanckSaveDto request)
        {

            var response = await _banckService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
