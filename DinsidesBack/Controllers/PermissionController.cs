using Application.Permissions.Dto;
using Application.Permissions.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionServices _permissionService;
        public PermissionController(IPermissionServices PermissionService) => _permissionService = PermissionService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<PermissionDto>>>> Get()
        {

            var response = await _permissionService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<PermissionDto>>, Ok<OperationResult<PermissionDto>>>> Get(int id)
        {
            var response = await _permissionService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<PermissionDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna permission con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<PermissionDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PermissionDto>>>> Post([FromBody] PermissionSaveDto request)
        {

            var response = await _permissionService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PermissionDto>>>> Put(int id, [FromBody] PermissionSaveDto request)
        {

            var response = await _permissionService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<PermissionSelectDto>>>> SelSelectActivoect()
        {

            var response = await _permissionService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("permission/{menuId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<PermissionDto>>>,
            Ok<OperationResult<List<PermissionDto>>>>> GetByMenuId(int menuId)
        {
            var result = await _permissionService.FindByMenuIdAsync(menuId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }


        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<PermissionDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _permissionService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PermissionDto>>>> Delete(int id)
        {
            var response = await _permissionService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }    

    }
}
