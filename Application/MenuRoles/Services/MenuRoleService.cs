using Application.Exceptions;
using Application.MenuRoles.Dto;
using Application.MenuRoles.Services.Interfaces;
using Application.Usuarios.Dto;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.MenuRolees.Services
{
    public class MenuRoleService : IMenuRoleServices
    {
        private readonly IMenuRoleRepositorio _menuRoleRepositorio;
        private readonly IMenuRepositorio _menuRepositorio;
        private readonly IRolRepositorio _rolRepositorio;
        private readonly IMapper _mapper;

        public MenuRoleService(
            IMenuRoleRepositorio menuRoleRepositorio,
            IMenuRepositorio MenuRepositorio,
            IRolRepositorio RolRepositorio,
            IMapper mapper)
        {
            _menuRoleRepositorio = menuRoleRepositorio;
            _menuRepositorio = MenuRepositorio;
            _rolRepositorio = RolRepositorio;
            _mapper = mapper;
        }




        public async Task<PaginadoResponse<MenuRoleDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _menuRoleRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<MenuRoleDto>>(response.Data);

            return new PaginadoResponse<MenuRoleDto>(data, response.Meta);
        }

        public async Task<OperationResult<MenuRoleDto>> CreateAsync(MenuRoleSaveDto saveDto)
        {
            var menuRole = _mapper.Map<Domain.MenuRole>(saveDto);


            if (menuRole.AuditCreateUser == null)
            {
                menuRole.AuditCreateUser = 1;
            }

            if (menuRole.AuditCreateDate == null)
            {
                menuRole.AuditCreateDate = DateTime.UtcNow;
            }

            await _menuRoleRepositorio.SaveAsync(menuRole);

            return new OperationResult<MenuRoleDto>()
            {
                Data = _mapper.Map<MenuRoleDto>(menuRole),
                Message = "Creado con éxito"
            };
        }

        public async Task<OperationResult<MenuRoleDto>> DisabledAsync(int id)
        {
            var menuRole = await _menuRoleRepositorio.FindByIdAsync(id);
            if (menuRole == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<MenuRoleDto>()
            {
                Data = _mapper.Map<MenuRoleDto>(menuRole),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<MenuRoleDto>> EditAsync(int id, MenuRoleSaveDto saveDto)
        {
            var menuRole = await _menuRoleRepositorio.FindByIdAsync(id);

            if (menuRole == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            _mapper.Map(saveDto, menuRole);

            await _menuRoleRepositorio.SaveAsync(menuRole);

            return new OperationResult<MenuRoleDto>()
            {
                Data = _mapper.Map<MenuRoleDto>(menuRole),
                Message = "Actualizado con exito",
            };

        }

        public async Task<IReadOnlyList<MenuRoleDto>> FindAllAsync()
        {
            var response = await _menuRoleRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<MenuRoleDto>>(response);
        }

        public async Task<MenuRoleDto> FindByIdAsync(int id)
        {
            var response = await _menuRoleRepositorio.FindByIdAsync(id);

            return _mapper.Map<MenuRoleDto>(response);
        }

        public async Task<IReadOnlyList<MenuRoleSelectDto>> SelectActivo()
        {
            var response = await _menuRoleRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<MenuRoleSelectDto>>(response);
        }

        public async Task<OperationResult<List<MenuRoleDto>>> FindByMenuIdAsync(int menuId)
        {
            var menuRoles = await _menuRoleRepositorio.FindByMenuIdAsync(menuId);

            if (menuRoles == null || !menuRoles.Any())
            {
                return new OperationResult<List<MenuRoleDto>>
                {
                    Data = new List<MenuRoleDto>(),
                    Message = $"No existen usuarios registradas para el proveedor con Id {menuId}"
                };
            }

            return new OperationResult<List<MenuRoleDto>>
            {
                Data = _mapper.Map<List<MenuRoleDto>>(menuRoles),
                Message = "Usuario encontrado"
            };
        }

        public async Task<OperationResult<List<MenuRoleDto>>> FindByRolIdAsync(int roleId)
        {
            var menuRoles = await _menuRoleRepositorio.FindByRolIdAsync(roleId);

            if (menuRoles == null || !menuRoles.Any())
            {
                return new OperationResult<List<MenuRoleDto>>
                {
                    Data = new List<MenuRoleDto>(),
                    Message = $"No existen role registradas para el proveedor con Id {roleId}"
                };
            }

            return new OperationResult<List<MenuRoleDto>>
            {
                Data = _mapper.Map<List<MenuRoleDto>>(menuRoles),
                Message = "Usuario encontrado"
            };
        }

    }
}


