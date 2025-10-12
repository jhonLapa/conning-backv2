using Application.Exceptions;
using Application.Mantenedores.Dtos.Roles;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepositorio _rolRepositorio;
        private readonly IMapper _mapper;

        public RolService(IRolRepositorio RolRepositorio, IMapper mapper)
        {
            _rolRepositorio = RolRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<RolDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _rolRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<RolDto>>(response.Data);

            return new PaginadoResponse<RolDto>(data, response.Meta);
        }

        public async Task<OperationResult<RolDto>> CreateAsync(RolSaveDto saveDto)
        {
            // 🚫 Validar duplicado por nombre
            var existe = await _rolRepositorio.ExistsAsync(d =>
                d.Name.ToLower() == saveDto.Name.ToLower());

            if (existe)
                throw new NotFoundCoreException("Ya existe otro dato con el mismo nombre.");

            var rol = _mapper.Map<Rol>(saveDto);
            rol.AuditCreateDate = DateTime.Now;
            rol.AuditCreateUser = 1;
            rol.State = true;

            await _rolRepositorio.SaveAsync(rol);

            return new OperationResult<RolDto>()
            {
                Data = _mapper.Map<RolDto>(rol),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<RolDto>> DisabledAsync(int id)
        {
            var rol = await _rolRepositorio.FindByIdAsync(id) ?? throw new NotFoundCoreException("Registro no encontrado con ese Id");
            rol.AuditDeleteDate = DateTime.Now;

            rol.State = rol.State == true ? false : true;

            await _rolRepositorio.SaveAsync(rol);

            return new OperationResult<RolDto>()
            {
                Data = _mapper.Map<RolDto>(rol),
                Message = rol.State == true
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<RolDto>> EditAsync(int id, RolSaveDto saveDto)
        {
            var rol = await _rolRepositorio.FindByIdAsync(id);
            if (rol == null) throw new NotFoundCoreException("Registro no encontrado con ese id");


            // 🚫 Validar duplicado de nombre (excluyendo el mismo ID)
            var existeDuplicado = await _rolRepositorio.ExistsAsync(d =>
                d.Name.ToLower() == saveDto.Name.ToLower(), id);

            if (existeDuplicado)
                throw new NotFoundCoreException("Ya existe otro dato con el mismo nombre.");

            rol.AuditUpdateDate = DateTime.Now;
            _mapper.Map(saveDto, rol);

            await _rolRepositorio.SaveAsync(rol);

            return new OperationResult<RolDto>()
            {
                Data = _mapper.Map<RolDto>(rol),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<RolDto>> FindAllAsync()
        {
            var response = await _rolRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<RolDto>>(response);
        }

        public async Task<RolDto> FindByIdAsync(int id)
        {
            var rol = await _rolRepositorio.FindByIdAsync(id);

            if (rol == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<RolDto>(rol);
        }

        public async Task<IReadOnlyList<RolSelectDto>> SelectActivo()
        {
            var response = await _rolRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<RolSelectDto>>(response);
        }
    }
}


