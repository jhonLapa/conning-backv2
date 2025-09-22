using Application.Mantenedores.Dtos.DocumentTypes;
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
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeServices _documentoServices;

        public DocumentTypeController(IDocumentTypeServices documentoServices) => _documentoServices = documentoServices;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<DocumentTypeDto>>>> Get()
        {

            var response = await _documentoServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<DocumentTypeDto>>> Get(int id)
        {
            var response = await _documentoServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DocumentTypeDto>>>> Post([FromBody] DocumentTypeSaveDto request)
        {

            var response = await _documentoServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<DocumentTypeDto>>>> Put(int id, [FromBody] DocumentTypeSaveDto request)
        {

            var response = await _documentoServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

    }
}
