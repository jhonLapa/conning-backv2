using Application.Exceptions;
using Application.Mantenedores.Dtos.Trabajadores;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Contexts;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Mantenedores.Services
{
    public class TrabajadorService : ITrabajadorService
    {
        private readonly ITrabajadorRepositorio _trabajadorRepositorio;
        private readonly IMapper _mapper;
        private readonly ICuentaBancariaTrabajadorRepositorio _cuentaBancariaTrabajadorRepositorio;
        private readonly ApplicationDbContext _context;
        private readonly ITrabajadorRepositorio _trabajadorRepo;
        private readonly ITipoDocumentoRepositorio _tipoDocumentoRepo;
        private readonly ICategoriaRepositorio _categoriaRepo;
        private readonly IRegimenPrevisionalRepositorio _regimenRepo;
        private readonly IBancoRepositorio _bancoRepo;
        private readonly ICuentaBancariaTrabajadorRepositorio _cuentaRepo;

        public TrabajadorService(ITrabajadorRepositorio trabajadorRepositorio, ICuentaBancariaTrabajadorRepositorio cuentaBancariaTrabajadorRepositorio,
               ITrabajadorRepositorio trabajadorRepo,
                ITipoDocumentoRepositorio tipoDocumentoRepo,
                ICategoriaRepositorio categoriaRepo,
                IRegimenPrevisionalRepositorio regimenRepo,
                IBancoRepositorio bancoRepo,
                ICuentaBancariaTrabajadorRepositorio cuentaRepo
            , IMapper mapper, ApplicationDbContext context)
        {
            _trabajadorRepositorio = trabajadorRepositorio;
            _cuentaBancariaTrabajadorRepositorio = cuentaBancariaTrabajadorRepositorio;

            _trabajadorRepo = trabajadorRepo;
            _tipoDocumentoRepo = tipoDocumentoRepo;
            _categoriaRepo = categoriaRepo;
            _regimenRepo = regimenRepo;
            _bancoRepo = bancoRepo;
            _cuentaRepo = cuentaRepo;

            _mapper = mapper;
            _context = context;

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


        public async Task<OperationResult<object>> GetDetallePlanillaAsync(int id)
        {
            try
            {
                // 🔹 Buscar relación TrabajadorProyecto + incluir el Trabajador real
                var tp = await _context.Set<TrabajadorProyecto>()
                    .Include(tp => tp.Trabajador)
                        .ThenInclude(t => t.Categoria)
                            .ThenInclude(c => c.ConceptosCategoria)
                    .Include(tp => tp.Trabajador)
                        .ThenInclude(t => t.Regimen)
                    .FirstOrDefaultAsync(tp => tp.IdTrabajadorProyecto == id);

                if (tp == null)
                    throw new NotFoundCoreException("No se encontró el trabajador del proyecto con ese ID.");

                var trabajador = tp.Trabajador;

                var response = new
                {
                    idTrabajadorProyecto = tp.IdTrabajadorProyecto,
                    idTrabajador = trabajador.IdTrabajador,
                    apellidosNombres = trabajador.ApellidosNombres,
                    categoria = new
                    {
                        idCategoria = trabajador.Categoria?.IdCategoria,
                        nombre = trabajador.Categoria?.Nombre
                    },
                    regimen = trabajador.Regimen == null ? null : new
                    {
                        idRegimen = trabajador.Regimen.IdRegimen,
                        nombre = trabajador.Regimen.Nombre,
                        tipo = trabajador.Regimen.Tipo,
                        comision = trabajador.Regimen.Comision,
                        prima = trabajador.Regimen.Prima,
                        aporte = trabajador.Regimen.Aporte,
                        total = trabajador.Regimen.Total,
                        tope = trabajador.Regimen.Tope
                    },
                    conceptos = trabajador.Categoria?.ConceptosCategoria?
                        .Where(c => c.Estado == 1)
                        .Select(c => new
                        {
                            idConcepto = c.IdConcepto,
                            nombreConcepto = c.NombreConcepto,
                            valor = c.Valor,
                            tipoConcepto = c.TipoConcepto
                        })
                        .ToList()
                };

                return new OperationResult<object>
                {
                    Success = true,
                    Message = "Detalle del trabajador obtenido correctamente.",
                    Data = response
                };
            }
            catch (NotFoundCoreException ex)
            {
                return new OperationResult<object>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<object>
                {
                    Success = false,
                    Message = $"Error al obtener el detalle del trabajador: {ex.Message}",
                };
            }
        }


        public async Task<IReadOnlyList<TrabajadorSelectDto>> SelectByProyecto(int idProyecto)
        {
            var response = await _trabajadorRepositorio.SelectByProyecto(idProyecto);
            return _mapper.Map<IReadOnlyList<TrabajadorSelectDto>>(response);
        }

        public async Task<PaginadoResponse<TrabajadorDto>> BusquedaPaginadoConPlanilla(
            PaginationRequest dto,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null)
        {
            var response = await _trabajadorRepositorio.BusquedaPaginadoConPlanilla(dto, fechaInicio, fechaFin);

            var data = _mapper.Map<ICollection<TrabajadorDto>>(response.Data);

            return new PaginadoResponse<TrabajadorDto>(data, response.Meta);
        }

        public async Task<OperationResult<object>> ProcesarCargaMasivaAsync(List<TrabajadorMasivoDto> registros)
        {
            int creados = 0;
            var errores = new List<object>();

            foreach (var item in registros)
            {
                var idTipoDoc = await _tipoDocumentoRepo.GetIdByNameAsync(item.TipoDocumento, "Nombre");
                var idCategoria = await _categoriaRepo.GetIdByNameAsync(item.Categoria, "Nombre");
                var idRegimen = await _regimenRepo.GetIdByNameAsync(item.Regimen, "Nombre");
                var idBanco = await _bancoRepo.GetIdByNameAsync(item.Banco, "Nombre");



                if (idTipoDoc == null || idCategoria == null || idRegimen == null || idBanco == null)
                {
                    errores.Add(new { item.NumeroDocumento, error = "No se encontraron IDs para los nombres enviados" });
                    continue;
                }

                bool existe = await _trabajadorRepo.ExistsAsync(
                    x => x.NumeroDocumento == item.NumeroDocumento &&
                         x.TipoDocumentoId == idTipoDoc.Value
                );

                if (existe)
                {
                    errores.Add(new { item.NumeroDocumento, error = "Trabajador duplicado" });
                    continue;
                }

                var trabajador = new Trabajador
                {
                    TipoDocumentoId = idTipoDoc.Value,
                    NumeroDocumento = item.NumeroDocumento,
                    ApellidosNombres = item.ApellidosNombres,
                    IdCategoria = idCategoria.Value,
                    IdRegimen = idRegimen.Value,
                    FechaNacimiento = item.FechaNacimiento,
                    Telefono = item.Telefono,
                    Email = item.Email,
                    Sexo = item.Sexo,
                    EstadoCivil = item.EstadoCivil,
                    Direccion = item.Direccion,
                    Hijos = item.Hijos,
                    FechaCreacion = DateTime.Now,
                    Estado = 1
                };

                await _trabajadorRepo.SaveAsync(trabajador);

                var cuenta = new CuentaBancariaTrabajador
                {
                    IdTrabajador = trabajador.IdTrabajador,
                    IdBanco = idBanco.Value,
                    NumeroCuenta = item.NumeroCuenta,
                    TipoCuenta = item.TipoCuenta,
                    Moneda = item.Moneda,
                    Principal = 1,
                    FechaCreacion = DateTime.Now,
                    Estado = 1
                };

                await _cuentaRepo.SaveAsync(cuenta);

                creados++;
            }

            return new OperationResult<object>
            {
                Success = true,
                Message = $"Carga completada: {creados} trabajadores creados.",
                Data = new { creados, errores }
            };
        }


    }
}
