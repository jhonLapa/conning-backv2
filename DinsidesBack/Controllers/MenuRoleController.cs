using Application.MenuRoles.Dto;
using Application.MenuRoles.Services.Interfaces;
using Application.Usuarios.Dto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuRoleController : ControllerBase
    {
        private readonly IMenuRoleServices _menuRoleService;
        public MenuRoleController(IMenuRoleServices MenuRoleService) => _menuRoleService = MenuRoleService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<MenuRoleDto>>>> Get()
        {

            var response = await _menuRoleService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<MenuRoleDto>>, Ok<OperationResult<MenuRoleDto>>>> Get(int id)
        {
            var response = await _menuRoleService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<MenuRoleDto>
                {
                    Data = null,
                    Message = $"No se encontró ningun usuario con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<MenuRoleDto>
            {
                Data = response,
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MenuRoleDto>>>> Post([FromBody] MenuRoleSaveDto request)
        {

            var response = await _menuRoleService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MenuRoleDto>>>> Put(int id, [FromBody] MenuRoleSaveDto request)
        {

            var response = await _menuRoleService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<MenuRoleSelectDto>>>> SelSelectActivoect()
        {

            var response = await _menuRoleService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("menu/{menuId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<MenuRoleDto>>>,
            Ok<OperationResult<List<MenuRoleDto>>>>> GetByMenuId(int menuId)
        {
            var result = await _menuRoleService.FindByMenuIdAsync(menuId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }

        [HttpGet("rol/{rolId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<MenuRoleDto>>>,
            Ok<OperationResult<List<MenuRoleDto>>>>> GetByRolId(int rolId)
        {
            var result = await _menuRoleService.FindByRolIdAsync(rolId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<MenuRoleDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _menuRoleService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MenuRoleDto>>>> Delete(int id)
        {
            var response = await _menuRoleService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

    }
}
