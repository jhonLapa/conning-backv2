using Application.Exceptions;
using Application.Permissions.Dto;
using Application.Permissions.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Permissions.Servicess
{
    public class PermissionService : IPermissionServices
    {
        private readonly IPermissionRepositorio _permissionRepositorio;
        private readonly IMenuRepositorio _menuRepositorio;
        private readonly IMapper _mapper;

        public PermissionService(
            IPermissionRepositorio permissionRepositorio,
            IMenuRepositorio MenuRepositorio,
            IMapper mapper)
        {
            _permissionRepositorio = permissionRepositorio;
            _menuRepositorio = MenuRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<PermissionDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _permissionRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<PermissionDto>>(response.Data);

            return new PaginadoResponse<PermissionDto>(data, response.Meta);
        }


        public async Task<OperationResult<PermissionDto>> CreateAsync(PermissionSaveDto saveDto)
        {
            var permission = _mapper.Map<Permission>(saveDto);


            if (permission.AuditCreateUser == null)
            {
                permission.AuditCreateUser = 1;
            }

            if (permission.AuditCreateDate == null)
            {
                permission.AuditCreateDate = DateTime.UtcNow;
            }

            await _permissionRepositorio.SaveAsync(permission);

            return new OperationResult<PermissionDto>()
            {
                Data = _mapper.Map<PermissionDto>(permission),
                Message = "Creado con éxito"
            };
        }

        public async Task<OperationResult<PermissionDto>> DisabledAsync(int id)
        {
            var permission = await _permissionRepositorio.FindByIdAsync(id);
            if (permission == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<PermissionDto>()
            {
                Data = _mapper.Map<PermissionDto>(permission),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<PermissionDto>> EditAsync(int id, PermissionSaveDto saveDto)
        {
            var permission = await _permissionRepositorio.FindByIdAsync(id);

            if (permission == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, permission);

            await _permissionRepositorio.SaveAsync(permission);

            return new OperationResult<PermissionDto>()
            {
                Data = _mapper.Map<PermissionDto>(permission),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<PermissionDto>> FindAllAsync()
        {
            var response = await _permissionRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<PermissionDto>>(response);
        }

        public async Task<PermissionDto> FindByIdAsync(int id)
        {
            var response = await _permissionRepositorio.FindByIdAsync(id);

            return _mapper.Map<PermissionDto>(response);
        }

        public async Task<IReadOnlyList<PermissionSelectDto>> SelectActivo()
        {
            var response = await _permissionRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<PermissionSelectDto>>(response);
        }

        public async Task<OperationResult<List<PermissionDto>>> FindByMenuIdAsync(int menuId)
        {
            var permissions = await _permissionRepositorio.FindByMenuIdAsync(menuId);

            if (permissions == null || !permissions.Any())
            {
                return new OperationResult<List<PermissionDto>>
                {
                    Data = new List<PermissionDto>(),
                    Message = $"No existen permissions registradas para el menu con Id {menuId}"
                };
            }

            return new OperationResult<List<PermissionDto>>
            {
                Data = _mapper.Map<List<PermissionDto>>(permissions),
                Message = "Permissions encontradas"
            };
        }     


    }
}


