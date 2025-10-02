using Application.Mantenedores.Dtos.TiposDocumento;
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
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoService _documentoServices;

        public TipoDocumentoController(ITipoDocumentoService documentoServices) => _documentoServices = documentoServices;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TipoDocumentoDto>>>> Get()
        {

            var response = await _documentoServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<TipoDocumentoDto>>> Get(int id)
        {
            var response = await _documentoServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TipoDocumentoDto>>>> Post([FromBody] TipoDocumentoSaveDto request)
        {

            var response = await _documentoServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TipoDocumentoDto>>>> Put(int id, [FromBody] TipoDocumentoSaveDto request)
        {

            var response = await _documentoServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<TipoDocumentoDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _documentoServices.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("Select")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TipoDocumentoSelectDto>>>> Select()
        {

            var response = await _documentoServices.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
