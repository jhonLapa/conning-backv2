using Application.Exceptions;
using Application.RolePermissions.Dto;
using Application.RolePermissions.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.RolePermissions.Services
{
    public class RolePermissionService : IRolePermissionServices
    {
        private readonly IRolePermissionRepositorio _rolePermissionRepositorio;
        private readonly IRolRepositorio _rolRepositorio; 
        private readonly IPermissionRepositorio _permissionRepositorio; 
        private readonly IMapper _mapper;

        public RolePermissionService(
            IRolePermissionRepositorio rolePermissionRepositorio,
            IRolRepositorio rolRepositorio,
            IPermissionRepositorio permissionRepositorio,
            IMapper mapper)
        {
            _rolePermissionRepositorio = rolePermissionRepositorio;
            _rolRepositorio = rolRepositorio;
            _permissionRepositorio = permissionRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<RolePermissionDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _rolePermissionRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<RolePermissionDto>>(response.Data);

            return new PaginadoResponse<RolePermissionDto>(data, response.Meta);
        }

        public async Task<OperationResult<RolePermissionDto>> CreateAsync(RolePermissionSaveDto saveDto)
        {
            var rolePermission = _mapper.Map<Domain.RolePermission>(saveDto);

            var existingRelation = await _rolePermissionRepositorio.FindByIdAsync(rolePermission.RoleId, rolePermission.PermissionId);

            if (existingRelation != null)
            {
                return new OperationResult<RolePermissionDto>()
                {
                    Success = false,
                    Message = $"La relación Rol {rolePermission.RoleId} y Permiso {rolePermission.PermissionId} ya existe. No se permite la inserción duplicada."
                };
            }

            if (rolePermission.AuditCreateUser == null)
            {
                rolePermission.AuditCreateUser = 1;
            }
            if (rolePermission.AuditCreateDate == null)
            {
                rolePermission.AuditCreateDate = DateTime.UtcNow;
            }

            await _rolePermissionRepositorio.AttachUnchangedAsync(rolePermission.RoleId, rolePermission.PermissionId);

            // 5. Guardar solo la nueva relación RolePermission.
            // El repositorio insertará la nueva fila RolePermission.
            await _rolePermissionRepositorio.SaveAsync(rolePermission);
            return new OperationResult<RolePermissionDto>()
            {
                Data = _mapper.Map<RolePermissionDto>(rolePermission),
                Message = "Creado con éxito"
            };
        }

        public async Task<IReadOnlyList<RolePermissionDto>> FindAllAsync()
        {
            var response = await _rolePermissionRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<RolePermissionDto>>(response);
        }


        public async Task<IReadOnlyList<RolePermissionSelectDto>> SelectActivo()
        {
            var response = await _rolePermissionRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<RolePermissionSelectDto>>(response);
        }

        public async Task<OperationResult<List<RolePermissionDto>>> FindByPermissionIdAsync(int permissionId)
        {
            var rolePermissions = await _rolePermissionRepositorio.FindByPermissionIdAsync(permissionId);

            if (rolePermissions == null || !rolePermissions.Any())
            {
                return new OperationResult<List<RolePermissionDto>>
                {
                    Data = new List<RolePermissionDto>(),
                    Message = $"No existen usuarios registradas para el proveedor con Id {permissionId}"
                };
            }

            return new OperationResult<List<RolePermissionDto>>
            {
                Data = _mapper.Map<List<RolePermissionDto>>(rolePermissions),
                Message = "Usuario encontrado"
            };
        }

        public async Task<OperationResult<List<RolePermissionDto>>> FindByRolIdAsync(int roleId)
        {
            var rolePermissions = await _rolePermissionRepositorio.FindByRolIdAsync(roleId);

            if (rolePermissions == null || !rolePermissions.Any())
            {
                return new OperationResult<List<RolePermissionDto>>
                {
                    Data = new List<RolePermissionDto>(),
                    Message = $"No existen role registradas para el proveedor con Id {roleId}"
                };
            }

            return new OperationResult<List<RolePermissionDto>>
            {
                Data = _mapper.Map<List<RolePermissionDto>>(rolePermissions),
                Message = "Usuario encontrado"
            };
        }

        public async Task<RolePermissionDto> FindByIdAsync(int roleId, int permissionId)
        {
            // Usamos los dos IDs para el repositorio
            var response = await _rolePermissionRepositorio.FindByIdAsync(roleId, permissionId);
            return _mapper.Map<RolePermissionDto>(response);
        }

        public async Task<OperationResult<RolePermissionDto>> EditAsync(int roleId, int permissionId, RolePermissionSaveDto saveDto)
        {
            // Usamos los dos IDs para el repositorio
            var rolePermission = await _rolePermissionRepositorio.FindByIdAsync(roleId, permissionId);

            if (rolePermission == null) throw new NotFoundCoreException($"Registro no encontrado para Rol {roleId} y Permiso {permissionId}");

            _mapper.Map(saveDto, rolePermission);
            // TODO: (Opcional) Agregar lógica para AuditUpdateDate y AuditUpdateUser

            await _rolePermissionRepositorio.SaveAsync(rolePermission);

            return new OperationResult<RolePermissionDto>()
            {
                Data = _mapper.Map<RolePermissionDto>(rolePermission),
                Message = "Actualizado con exito",
            };
        }

        public async Task<OperationResult<RolePermissionDto>> DisabledAsync(int roleId, int permissionId)
        {
            // Usamos los dos IDs para el repositorio
            var rolePermission = await _rolePermissionRepositorio.FindByIdAsync(roleId, permissionId);

            if (rolePermission == null) throw new NotFoundCoreException($"Registro no encontrado para Rol {roleId} y Permiso {permissionId}");

            // TODO: Implementar lógica de deshabilitación (ej: rolePermission.State = 'I')

            return new OperationResult<RolePermissionDto>()
            {
                Data = _mapper.Map<RolePermissionDto>(rolePermission),
                Message = "Se ha Desactivado",
            };
        }
    }
}