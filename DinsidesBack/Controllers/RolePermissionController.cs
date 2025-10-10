using Application.RolePermissions.Dto;
using Application.RolePermissions.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionServices _rolePermissionService;
        public RolePermissionController(IRolePermissionServices RolePermissionService) => _rolePermissionService = RolePermissionService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<RolePermissionDto>>>> Get()
        {

            var response = await _rolePermissionService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{roleId}/{permissionId}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<RolePermissionDto>>, Ok<OperationResult<RolePermissionDto>>>> Get(int roleId, int permissionId)
        {
            // Ahora llama al servicio con ambos IDs
            var response = await _rolePermissionService.FindByIdAsync(roleId, permissionId);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<RolePermissionDto>
                {
                    Data = null,
                    Message = $"No se encontró la relación Rol {roleId} y Permiso {permissionId}." // Mensaje actualizado
                });
            }

            return TypedResults.Ok(new OperationResult<RolePermissionDto>
            {
                Data = response,
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<RolePermissionDto>>>> Post([FromBody] RolePermissionSaveDto request)
        {

            var response = await _rolePermissionService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{roleId}/{permissionId}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<RolePermissionDto>>>> Put(int roleId, int permissionId, [FromBody] RolePermissionSaveDto request)
        {
            // Ahora llama al servicio con ambos IDs
            var response = await _rolePermissionService.EditAsync(roleId, permissionId, request);

            if (response != null) return TypedResults.Ok(response);
            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<RolePermissionSelectDto>>>> SelSelectActivoect()
        {

            var response = await _rolePermissionService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("permission/{permissionId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<RolePermissionDto>>>,
            Ok<OperationResult<List<RolePermissionDto>>>>> GetByPermissionId(int permissionId)
        {
            var result = await _rolePermissionService.FindByPermissionIdAsync(permissionId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }

        [HttpGet("rol/{rolId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<RolePermissionDto>>>,
            Ok<OperationResult<List<RolePermissionDto>>>>> GetByRolId(int rolId)
        {
            var result = await _rolePermissionService.FindByRolIdAsync(rolId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<RolePermissionDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _rolePermissionService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{roleId}/{permissionId}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<RolePermissionDto>>>> Delete(int roleId, int permissionId)
        {
            // Ahora llama al servicio con ambos IDs
            var response = await _rolePermissionService.DisabledAsync(roleId, permissionId);

            if (response != null) return TypedResults.Ok(response);
            return TypedResults.BadRequest();
        }

    }
}
