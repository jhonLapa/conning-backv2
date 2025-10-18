using Application.Exceptions;
using Application.Mantenedores.Dtos.Trabajadores;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class TrabajadorService : ITrabajadorService
    {
        private readonly ITrabajadorRepositorio _trabajadorRepositorio;
        private readonly IMapper _mapper;
        private readonly ICuentaBancariaTrabajadorRepositorio _cuentaBancariaTrabajadorRepositorio;

        public TrabajadorService(ITrabajadorRepositorio trabajadorRepositorio, ICuentaBancariaTrabajadorRepositorio cuentaBancariaTrabajadorRepositorio, IMapper mapper)
        {
            _trabajadorRepositorio = trabajadorRepositorio;
            _cuentaBancariaTrabajadorRepositorio = cuentaBancariaTrabajadorRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<TrabajadorDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _trabajadorRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<TrabajadorDto>>(response.Data);

            return new PaginadoResponse<TrabajadorDto>(data, response.Meta);
        }
        public async Task<OperationResult<TrabajadorDto>> CreateAsync(TrabajadorSaveDto saveDto)
        {
            var trabajador = _mapper.Map<Trabajador>(saveDto);

            trabajador.FechaCreacion = DateTime.Now;
            trabajador.Estado = 1;

            trabajador.UsuarioCreacion = "system";
            trabajador.FechaModificacion = null;
            trabajador.UsuarioModificacion = null;

            trabajador.Email = saveDto.Email ?? "sincorreo@example.com";
            trabajador.Telefono = saveDto.Telefono ?? "000000000";
            trabajador.Direccion = saveDto.Direccion ?? "Sin dirección";


            if (trabajador.TipoDocumentoId == 0 || trabajador.IdCategoria == 0 || trabajador.IdRegimen == 0)
            {
                throw new NotFoundCoreException("Los campos Tipo Documento, Categoría y Régimen son obligatorios.");
            }


            await _trabajadorRepositorio.SaveAsync(trabajador);

            return new OperationResult<TrabajadorDto>()
            {
                Data = _mapper.Map<TrabajadorDto>(trabajador),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<TrabajadorDto>> DisabledAsync(int id)
        {
            var trabajador = await _trabajadorRepositorio.FindByIdAsync(id);

            if (trabajador == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            trabajador.Estado = trabajador.Estado == 1 ? 0 : 1;
            trabajador.FechaCreacion = DateTime.Now;

            await _trabajadorRepositorio.SaveAsync(trabajador);

            return new OperationResult<TrabajadorDto>()
            {
                Data = _mapper.Map<TrabajadorDto>(trabajador),
                Message = trabajador.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<TrabajadorDto>> EditAsync(int id, TrabajadorSaveDto saveDto)
        {
            var trabajador = await _trabajadorRepositorio.FindByIdAsync(id);

            if (trabajador == null) throw new NotFoundCoreException("Registro no encontrado con ese id");


            _mapper.Map(saveDto, trabajador);

            await _trabajadorRepositorio.SaveAsync(trabajador);

            return new OperationResult<TrabajadorDto>()
            {
                Data = _mapper.Map<TrabajadorDto>(trabajador),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<TrabajadorDto>> FindAllAsync()
        {
            var response = await _trabajadorRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TrabajadorDto>>(response);
        }

        public async Task<TrabajadorDto> FindByIdAsync(int id)
        {
            var trabajador = await _trabajadorRepositorio.FindByIdAsync(id);

            if (trabajador == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<TrabajadorDto>(trabajador);
        }

        public async Task<IReadOnlyList<TrabajadorSelectDto>> SelectActivo()
        {
            var response = await _trabajadorRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<TrabajadorSelectDto>>(response);
        }

        public async Task<OperationResult<TrabajadorDto>> CreateOrUpdateWithAccountsAsync(TrabajadorWithAccountsSaveDto dto)
        {
            var idTrabajador = dto.IdTrabajador;

            // 🧩 Validar existencia antes de crear o actualizar
            bool existe = await _trabajadorRepositorio.ExistsAsync(
                       x => x.NumeroDocumento == dto.NumeroDocumento
                         && x.TipoDocumentoId == dto.IdTipoDocumento
                         && x.ApellidosNombres == dto.ApellidosNombres,
                       idTrabajador == 0 ? (int?)null : idTrabajador // 👈 conversión explícita
                   );

            if (existe)
            {
                return new OperationResult<TrabajadorDto>
                {
                    Success = false,
                    Message = "Ya existe un trabajador con el mismo documento y nombre.",
                    Data = null
                };
            }

            Trabajador trabajador;

            if (idTrabajador == 0)
            {
                // 🟢 Crear nuevo trabajador
                trabajador = _mapper.Map<Trabajador>(dto);
                trabajador.FechaCreacion = DateTime.Now;
                trabajador.Estado = 1;
                trabajador.TipoDocumentoId = dto.IdTipoDocumento;

                await _trabajadorRepositorio.SaveAsync(trabajador);
            }
            else
            {
                // 🟡 Actualizar trabajador existente
                trabajador = await _trabajadorRepositorio.FindByIdAsync(idTrabajador);
                if (trabajador == null)
                {
                    return new OperationResult<TrabajadorDto>
                    {
                        Success = false,
                        Message = "El trabajador no existe.",
                        Data = null
                    };
                }

                // Mapear los nuevos datos al existente
                _mapper.Map(dto, trabajador);
                trabajador.FechaModificacion = DateTime.Now;
                trabajador.TipoDocumentoId = dto.IdTipoDocumento;

                await _trabajadorRepositorio.SaveAsync(trabajador);

                // 🔴 Eliminar cuentas existentes antes de recrearlas
                var cuentasExistentes = await _cuentaBancariaTrabajadorRepositorio
                    .GetByTrabajadorIdAsync(idTrabajador);

                foreach (var cuenta in cuentasExistentes)
                    await _cuentaBancariaTrabajadorRepositorio.DeleteByCuentaTrabajadorIdAsync(cuenta.IdTrabajador);
            }

            // 🔵 Crear cuentas nuevas
            if (dto.Cuentas != null && dto.Cuentas.Any())
            {
                foreach (var cuentaDto in dto.Cuentas)
                {
                    var cuenta = _mapper.Map<CuentaBancariaTrabajador>(cuentaDto);
                    cuenta.IdTrabajador = trabajador.IdTrabajador;
                    cuenta.FechaCreacion = DateTime.Now;
                    cuenta.Estado = 1;

                    await _cuentaBancariaTrabajadorRepositorio.SaveAsync(cuenta);
                }
            }

            return new OperationResult<TrabajadorDto>
            {
                Data = _mapper.Map<TrabajadorDto>(trabajador),
                Message = idTrabajador == 0
                    ? "Trabajador y cuentas creados con éxito"
                    : "Trabajador y cuentas actualizados con éxito",
                Success = true
            };
        }


    }
}
