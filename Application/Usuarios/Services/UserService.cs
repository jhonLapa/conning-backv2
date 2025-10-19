using Application.Auth.Dto;
using Application.Auth.Services.Interfaces;
using Application.Exceptions;
using Application.Usuarios.Dto;
using Application.Usuarios.Services.Interface;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Usuarios.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IRolRepositorio _rolRepositorio;
        private readonly IUserRoleRepositorio _userRolRepositorio;
        private readonly IJwtServices _securityService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        public UserService(
            IMapper mapper,
            IRolRepositorio rolRepositorio ,
            IUsuarioRepositorio usuarioRepositorio, 
            IUserRoleRepositorio userRolRepositorio,
            IJwtServices securityService, 
            IConfiguration configuration, 
            ILogger<UserService> logger)
        {
            _mapper = mapper;
            _rolRepositorio = rolRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _userRolRepositorio = userRolRepositorio;
            _securityService = securityService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<OperationResult<UserDto>> CreateAsync(UserRoleSaveDto saveDto)
        {
            var existe = await _usuarioRepositorio.ExistsAsync(d =>
                d.FirstName.ToLower() == saveDto.FirstName.ToLower());

            if (existe)
                throw new NotFoundCoreException("Ya existe otro dato con el mismo nombre.");

            var user = _mapper.Map<User>(saveDto.UserId);

            var email = await _usuarioRepositorio.FindByEmailAsync(user.Email);

            if (email != null)
            {
                throw new NotFoundCoreException("Correo ya registrado");
            }
            _ = await _rolRepositorio.FindByIdAsync(saveDto.RoleId) ?? throw new NotFoundCoreException("Rol es Necesario"); ;

            user.Password = _securityService.HashPassword(user.Email, user.Password);
            user.AuditCreateUser = 1;
            user.AuditCreateDate = DateTime.Now;

            await _usuarioRepositorio.SaveAsync(user);


            var rol_usuario = new UserRole();

            rol_usuario.RoleId= saveDto.RoleId;
            rol_usuario.UserId = user.UserId;
            rol_usuario.State = true;
            rol_usuario.AuditCreateDate= DateTime.Now;
            rol_usuario.AuditCreateUser = 1;

            await _userRolRepositorio.SaveAsync(rol_usuario);


            return new OperationResult<UserDto>()
            {
                Success = true,
                Data = _mapper.Map<UserDto>(user),
                Message = "Usuario creado con exito"
            };
        }

        public async Task<OperationResult<UserDto>> DisabledAsync(int id)
        {
            var usuario = await _usuarioRepositorio.FindByIdAsync(id);
            
            if (usuario == null)
            {
                _logger.LogWarning("Usuario no encontrado para el id " + id);
                throw new NotImplementedException();
            }

            usuario.State= !usuario.State;
            usuario.AuditUpdateUser = 1;
            usuario.AuditUpdateDate= DateTime.Now;

            await _usuarioRepositorio.SaveAsync(usuario);
            string action = usuario.State ? "activado" : "desactivado";
            return new OperationResult<UserDto>()
            {
                Success = true,
                Data = _mapper.Map<UserDto>(usuario),
                Message = "Usuario actualizado con exito"
            };
        }

        public async Task<OperationResult<UserDto>> EditAsync(int id, UserRoleSaveDto saveDto)
        {
            User user = await _usuarioRepositorio.FindByIdAsync(id) ?? throw new NotFoundCoreException("Usuario no Registrado con ese id");
            _mapper.Map(saveDto, user);
            if (!string.IsNullOrWhiteSpace(saveDto.Password))
            {
                // Se asume que saveDto.Password trae la nueva contraseña en texto plano.
                user.Password = _securityService.HashPassword(user.Email, saveDto.Password);
            }

            var existeDuplicado = await _usuarioRepositorio.ExistsAsync(d =>
                d.FirstName.ToLower() == saveDto.FirstName.ToLower(), id);

            if (existeDuplicado)
                throw new NotFoundCoreException("Ya existe otro dato con el mismo nombre.");

            user.AuditUpdateDate = DateTime.Now;
            user.Password = user.Password;

            await _usuarioRepositorio.SaveAsync(user);

            return new OperationResult<UserDto>()
            {
                Success = true,
                Data = _mapper.Map<UserDto>(user),
                Message = "Usuario actualizado con exito"
            };

        }

        public async Task<IReadOnlyList<UserDto>> FindAllAsync()
        {
            var response = await _usuarioRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<UserDto>>(response);
        }

        public async Task<UserDto> FindByIdAsync(int id)
        {
            var response = await _usuarioRepositorio.FindByIdAsync(id);

            return  _mapper.Map<UserDto>(response);
        }

        public async Task<OperationResult<LoginDto>> LoginAsync(LoginRequest userAuthDto)
        {

            User user = await _usuarioRepositorio.FindByEmailAsync(userAuthDto.Email) ?? throw new NotFoundCoreException("Usuario no registrado"); ;
            
            bool isCorrect = _securityService.VerifyHashedPassword(user.Email, user.Password, userAuthDto.Password);

            if (!isCorrect) throw new NotFoundCoreException("La contraseña no es correcta");

            string jwtSecretKey = _configuration.GetSection("security:JwtSecretKey").Get<string>();
            
            var user_securiti = _securityService.JwtSecurity(jwtSecretKey);

            
            LoginDto userSecurity = new()
            {
                AccessToken = user_securiti.Token,
                RefreshToken = user_securiti.Token,
                User = _mapper.Map<UserView>(user),
            };



            return new OperationResult<LoginDto>()
            {
                Data = userSecurity,
                Message = "Sesión iniciada correctamente",
                Success = true,
            };
        }

        public async Task<PaginadoResponse<UserDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _usuarioRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<UserDto>>(response.Data);

            return new PaginadoResponse<UserDto>(data, response.Meta);
        }

        public async Task<IReadOnlyList<UserSelectDto>> SelectActivo()
        {
            var response = await _usuarioRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<UserSelectDto>>(response);
        }


    }
}
