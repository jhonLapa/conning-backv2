using Application.Exceptions;
using Application.UserRoles.Dto;
using Application.UserRoles.Services.Interfaces;
using Application.Usuarios.Dto;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.UserRolees.Services
{
    public class UserRoleService : IUserRoleServices
    {
        private readonly IUserRoleRepositorio _userRoleRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IRolRepositorio _rolRepositorio;
        private readonly IMapper _mapper;

        public UserRoleService(
            IUserRoleRepositorio userRoleRepositorio,
            IUsuarioRepositorio UsuarioRepositorio,
            IRolRepositorio RolRepositorio,
            IMapper mapper)
        {
            _userRoleRepositorio = userRoleRepositorio;
            _usuarioRepositorio = UsuarioRepositorio;
            _rolRepositorio = RolRepositorio;
            _mapper = mapper;
        }




        public async Task<PaginadoResponse<UserRoleDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _userRoleRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<UserRoleDto>>(response.Data);

            return new PaginadoResponse<UserRoleDto>(data, response.Meta);
        }

        public async Task<OperationResult<UserRoleDto>> CreateAsync(UserRoleSaveDto saveDto)
        {
            var userRole = _mapper.Map<Domain.UserRole>(saveDto);

          
            if (userRole.AuditCreateUser == null)
            {
                userRole.AuditCreateUser = 1;
            }
       
            if (userRole.AuditCreateDate == null)
            {
                userRole.AuditCreateDate = DateTime.UtcNow; 
            }

            await _userRoleRepositorio.SaveAsync(userRole);

            return new OperationResult<UserRoleDto>()
            {
                Data = _mapper.Map<UserRoleDto>(userRole),
                Message = "Creado con éxito"
            };
        }

        public async Task<OperationResult<UserRoleDto>> DisabledAsync(int id)
        {
            var userRole = await _userRoleRepositorio.FindByIdAsync(id);
            if (userRole == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<UserRoleDto>()
            {
                Data = _mapper.Map<UserRoleDto>(userRole),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<UserRoleDto>> EditAsync(int id, UserRoleSaveDto saveDto)
        {
            var userRole = await _userRoleRepositorio.FindByIdAsync(id);

            if (userRole == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            _mapper.Map(saveDto, userRole);

            await _userRoleRepositorio.SaveAsync(userRole);

            return new OperationResult<UserRoleDto>()
            {
                Data = _mapper.Map<UserRoleDto>(userRole),
                Message = "Actualizado con exito",
            };

        }

        public async Task<IReadOnlyList<UserRoleDto>> FindAllAsync()
        {
            var response = await _userRoleRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<UserRoleDto>>(response);
        }

        public async Task<UserRoleDto> FindByIdAsync(int id)
        {
            var response = await _userRoleRepositorio.FindByIdAsync(id);

            return _mapper.Map<UserRoleDto>(response);
        }

        public async Task<IReadOnlyList<UserRoleSelectDto>> SelectActivo()
        {
            var response = await _userRoleRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<UserRoleSelectDto>>(response);
        }

        public async Task<OperationResult<List<UserRoleDto>>> FindByUserIdAsync(int userId)
        {
            var userRoles = await _userRoleRepositorio.FindByUserIdAsync(userId);

            if (userRoles == null || !userRoles.Any())
            {
                return new OperationResult<List<UserRoleDto>>
                {
                    Data = new List<UserRoleDto>(),
                    Message = $"No existen usuarios registradas para el proveedor con Id {userId}"
                };
            }

            return new OperationResult<List<UserRoleDto>>
            {
                Data = _mapper.Map<List<UserRoleDto>>(userRoles),
                Message = "Usuario encontrado"
            };
        }

        public async Task<OperationResult<List<UserRoleDto>>> FindByRolIdAsync(int roleId)
        {
            var userRoles = await _userRoleRepositorio.FindByRolIdAsync(roleId);

            if (userRoles == null || !userRoles.Any())
            {
                return new OperationResult<List<UserRoleDto>>
                {
                    Data = new List<UserRoleDto>(),
                    Message = $"No existen role registradas para el proveedor con Id {roleId}"
                };
            }

            return new OperationResult<List<UserRoleDto>>
            {
                Data = _mapper.Map<List<UserRoleDto>>(userRoles),
                Message = "Usuario encontrado"
            };
        }

    }
}


