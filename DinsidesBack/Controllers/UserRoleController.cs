using Application.UserRoles.Dto;
using Application.UserRoles.Services.Interfaces;
using Application.Usuarios.Dto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleServices _userRoleService;
        public UserRoleController(IUserRoleServices UserRoleService) => _userRoleService = UserRoleService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<UserRoleDto>>>> Get()
        {

            var response = await _userRoleService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<UserRoleDto>>, Ok<OperationResult<UserRoleDto>>>> Get(int id)
        {
            var response = await _userRoleService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<UserRoleDto>
                {
                    Data = null,
                    Message = $"No se encontró ningun usuario con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<UserRoleDto>
            {
                Data = response,
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<UserRoleDto>>>> Post([FromBody] UserRoleSaveDto request)
        {

            var response = await _userRoleService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<UserRoleDto>>>> Put(int id, [FromBody] UserRoleSaveDto request)
        {

            var response = await _userRoleService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<UserRoleSelectDto>>>> SelSelectActivoect()
        {

            var response = await _userRoleService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("usuario/{usuarioId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<UserRoleDto>>>,
            Ok<OperationResult<List<UserRoleDto>>>>> GetByUsuarioId(int usuarioId)
        {
            var result = await _userRoleService.FindByUserIdAsync(usuarioId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }

        [HttpGet("rol/{rolId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<UserRoleDto>>>,
            Ok<OperationResult<List<UserRoleDto>>>>> GetByRolId(int rolId)
        {
            var result = await _userRoleService.FindByRolIdAsync(rolId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<UserRoleDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _userRoleService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<UserRoleDto>>>> Delete(int id)
        {
            var response = await _userRoleService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }
      
    }
}
