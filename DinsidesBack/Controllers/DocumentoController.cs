using Application.Mantenedores.Dtos.Documentos;
using Application.Mantenedores.Services.Interfaces;
using Application.Usuarios.Dto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoServices _documentoServices;

        public DocumentoController(IDocumentoServices documentoServices) => _documentoServices = documentoServices;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<DocumentoDto>>>> Get()
        {

            var response = await _documentoServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<DocumentoDto>>> Get(int id)
        {
            var response = await _documentoServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DocumentoDto>>>> Post([FromBody] DocumentoSaveDto request)
        {

            var response = await _documentoServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DocumentoDto>>>> Put(int id, [FromBody] DocumentoSaveDto request)
        {

            var response = await _documentoServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

    }
}
