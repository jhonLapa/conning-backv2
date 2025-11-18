using Application.Exceptions;
using Application.Mantenedores.Dtos.Proyectos;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepositorio _projectRepositorio;
        private readonly IMapper _mapper;
        private readonly ITrabajadorProyectoRepositorio _trabajadorProyectoRepositorio;
        private readonly IProyectoEncargadoRepositorio _proyectoEncargadoRepositorio;
        private readonly IAportesSindicatoRepositorio _aportesSindicatoRepositorio;

        public ProyectoService(IProyectoRepositorio ProjectRepositorio ,
            IProyectoEncargadoRepositorio proyectoEncargadoRepositorio,
            IAportesSindicatoRepositorio aportesSindicatoRepositorio,
            ITrabajadorProyectoRepositorio trabajadorProyectoRepositorio,
                   IMapper mapper)
        {
            _projectRepositorio = ProjectRepositorio;
            _proyectoEncargadoRepositorio = proyectoEncargadoRepositorio;
            _trabajadorProyectoRepositorio = trabajadorProyectoRepositorio;
            _aportesSindicatoRepositorio = aportesSindicatoRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<ProyectoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _projectRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<ProyectoDto>>(response.Data);

            return new PaginadoResponse<ProyectoDto>(data, response.Meta);
        }
        public async Task<PaginadoResponse<ProyectoConTotalDto>> BusquedaPaginadoTrabajador(
            PaginationRequest dto,
            int idTrabajador,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null)
        {
            var response = await _projectRepositorio.BusquedaPaginadoTrabajador(dto, idTrabajador, fechaInicio, fechaFin);
            var data = _mapper.Map<ICollection<ProyectoConTotalDto>>(response.Data);

            return new PaginadoResponse<ProyectoConTotalDto>(data, response.Meta);
        }


        public async Task<OperationResult<ProyectoDto>> CreateAsync(ProyectoSaveDto saveDto)
        {
            var existe = await _projectRepositorio.ExistsAsync(d =>
                d.Nombre.ToLower() == saveDto.Nombre.ToLower());

            if (existe)
                throw new NotFoundCoreException("Ya existe otro dato con el mismo nombre.");

            var project = _mapper.Map<Proyecto>(saveDto);
            project.FechaCreacion = DateTime.Now;
            project.Estado = 1;

            await _projectRepositorio.SaveAsync(project);

            return new OperationResult<ProyectoDto>()
            {
                Data = _mapper.Map<ProyectoDto>(project),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<ProyectoDto>> DisabledAsync(int id)
        {
            var project = await _projectRepositorio.FindByIdAsync(id);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            project.Estado = project.Estado == 1 ? 0 : 1;
            project.FechaModificacion = DateTime.Now;
            await _projectRepositorio.SaveAsync(project);

            return new OperationResult<ProyectoDto>()
            {
                Data = _mapper.Map<ProyectoDto>(project),
                Message = project.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<ProyectoDto>> EditAsync(int id, ProyectoSaveDto saveDto)
        {
            var project = await _projectRepositorio.FindByIdAsync(id);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            var existeDuplicado = await _projectRepositorio.ExistsAsync(d =>
                d.Nombre.ToLower() == saveDto.Nombre.ToLower(), id);

            if (existeDuplicado)
                throw new NotFoundCoreException("Ya existe otro dato con el mismo nombre.");

            project.FechaModificacion = DateTime.Now;

            _mapper.Map(saveDto, project);

            await _projectRepositorio.SaveAsync(project);

            return new OperationResult<ProyectoDto>()
            {
                Data = _mapper.Map<ProyectoDto>(project),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<ProyectoDto>> FindAllAsync()
        {
            var response = await _projectRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ProyectoDto>>(response);
        }

        public async Task<ProyectoDto> FindByIdAsync(int id)
        {
            var project = await _projectRepositorio.FindByIdAsync(id);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<ProyectoDto>(project);
        }

        public async Task<IReadOnlyList<ProyectoSelectDto>> SelectActivo()
        {
            var response = await _projectRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<ProyectoSelectDto>>(response);
        }


        public async Task<OperationResult<ProyectoDto>> CreateProyectoCompletoAsync(ProyectoFormDataDto dto)
        {
            try
            {
                dto.Proyecto.IdProyecto ??= 0;
                Proyecto proyecto;

                // ============================================================
                // 🟢 Crear o actualizar PROYECTO
                // ============================================================
                if (dto.Proyecto.IdProyecto == 0)
                {
                    proyecto = _mapper.Map<Proyecto>(dto.Proyecto);
                    proyecto.FechaCreacion = DateTime.Now;
                    proyecto.Estado = 1;
                    await _projectRepositorio.SaveAsync(proyecto);
                }
                else
                {
                    proyecto = await _projectRepositorio.FindByIdAsync(dto.Proyecto.IdProyecto.Value);
                    if (proyecto == null)
                        return new OperationResult<ProyectoDto> { Success = false, Message = "Proyecto no encontrado." };

                    _mapper.Map(dto.Proyecto, proyecto);
                    proyecto.FechaModificacion = DateTime.Now;
                    proyecto.UsuarioModificacion = dto.Proyecto.UsuarioCreacion;
                    await _projectRepositorio.SaveAsync(proyecto);

                    // 🔄 Eliminar aportes sindicato viejos (siempre se regeneran)
                    await _aportesSindicatoRepositorio.DeleteByProyectoIdAsync(proyecto.IdProyecto);
                }

                // ============================================================
                // 👷 SINCRONIZAR TRABAJADORES DEL PROYECTO
                // ============================================================
                if (dto.Trabajador != null)
                {
                    var trabajadoresExistentes = await _trabajadorProyectoRepositorio.GetByProyectoIdAsync(proyecto.IdProyecto);

                    foreach (var trabajadorExistente in trabajadoresExistentes)
                    {
                        bool usadoEnPlanilla = await _trabajadorProyectoRepositorio
                            .ExisteEnPlanillaAsync(trabajadorExistente.IdTrabajadorProyecto);

                        var match = dto.Trabajador.FirstOrDefault(t => t.IdTrabajador == trabajadorExistente.IdTrabajador);

                        if (usadoEnPlanilla)
                        {
                            // 🔄 Si está en planilla → actualizar datos, no eliminar
                            if (match != null)
                            {
                                trabajadorExistente.FechaInicio = match.FechaInicio;
                                trabajadorExistente.Estado = match.Estado;
                                trabajadorExistente.UsuarioCreacion = match.UsuarioCreacion;
                                await _trabajadorProyectoRepositorio.SaveAsync(trabajadorExistente);
                            }
                            // ⚠️ Si está en planilla pero no vino en dto, se conserva
                        }
                        else
                        {
                            // ❌ Si no está en planilla
                            if (match == null)
                            {
                                // eliminar si no se envió en el DTO
                                await _trabajadorProyectoRepositorio.DeleteAsync(trabajadorExistente.IdTrabajadorProyecto);
                            }
                            else
                            {
                                // ✅ Si vino en DTO, actualizarlo (no eliminar)
                                trabajadorExistente.FechaInicio = match.FechaInicio;
                                trabajadorExistente.Estado = match.Estado;
                                trabajadorExistente.UsuarioCreacion = match.UsuarioCreacion;
                                await _trabajadorProyectoRepositorio.SaveAsync(trabajadorExistente);
                            }
                        }
                    }

                    // 🟢 Recargar lista luego de eliminar/actualizar
                    trabajadoresExistentes = await _trabajadorProyectoRepositorio.GetByProyectoIdAsync(proyecto.IdProyecto);

                    // 🆕 Agregar nuevos trabajadores que no existían
                    foreach (var t in dto.Trabajador)
                    {
                        var existe = trabajadoresExistentes.Any(x => x.IdTrabajador == t.IdTrabajador);
                        if (!existe)
                        {
                            var nuevo = new TrabajadorProyecto
                            {
                                IdProyecto = proyecto.IdProyecto,
                                IdTrabajador = t.IdTrabajador,
                                FechaInicio = t.FechaInicio,
                                Estado = t.Estado,
                                UsuarioCreacion = t.UsuarioCreacion,
                                FechaCreacion = DateTime.Now
                            };
                            await _trabajadorProyectoRepositorio.SaveAsync(nuevo);
                        }
                    }
                }

                // ============================================================
                // 💰 APORTES SINDICATO
                // ============================================================
                if (dto.Sindicato != null && dto.Sindicato.Any())
                {
                    foreach (var s in dto.Sindicato)
                    {
                        var aporte = new AportesSindicato
                        {
                            IdProyecto = proyecto.IdProyecto,
                            Mes = s.Mes,
                            Monto = s.Monto,
                            FechaPago = s.FechaPago,
                            UsuarioCreacion = s.UsuarioCreacion,
                            FechaCreacion = DateTime.Now
                        };
                        await _aportesSindicatoRepositorio.SaveAsync(aporte);
                    }
                }

                // ============================================================
                // 👨‍💼 ENCARGADO DEL PROYECTO
                // ============================================================
                var encargadosExistentes = await _proyectoEncargadoRepositorio.GetByProyectoIdAsync(proyecto.IdProyecto);

                if (dto.ProyectoEncargado == null)
                {
                    // 🔻 Si el usuario DESELECCIONÓ el encargado
                    foreach (var enc in encargadosExistentes)
                    {
                        enc.Estado = 0;
                        enc.FechaFin = DateTime.Now;
                        await _proyectoEncargadoRepositorio.SaveAsync(enc);
                    }
                }
                else
                {
                    // 🔹 Desactivar anteriores
                    foreach (var enc in encargadosExistentes)
                    {
                        enc.Estado = 0;
                        enc.FechaFin = DateTime.Now;
                        await _proyectoEncargadoRepositorio.SaveAsync(enc);
                    }

                    // 🔹 Crear nuevo encargado activo
                    var nuevoEncargado = new ProyectoEncargado
                    {
                        IdProyecto = proyecto.IdProyecto,
                        IdTrabajador = dto.ProyectoEncargado.IdTrabajador,
                        Rol = dto.ProyectoEncargado.Rol,
                        FechaInicio = dto.ProyectoEncargado.FechaInicio,
                        FechaFin = dto.ProyectoEncargado.FechaFin,
                        Estado = 1
                    };
                    await _proyectoEncargadoRepositorio.SaveAsync(nuevoEncargado);
                }

                // ============================================================
                // 🟩 RETORNAR RESULTADO
                // ============================================================
                var projectWithIncludes = await _projectRepositorio.FindByIdAsync(proyecto.IdProyecto);
                var mappedDto = _mapper.Map<ProyectoDto>(projectWithIncludes);

                return new OperationResult<ProyectoDto>
                {
                    Success = true,
                    Message = dto.Proyecto.IdProyecto > 0
                        ? "Proyecto actualizado correctamente"
                        : "Proyecto completo creado con éxito.",
                    Data = mappedDto
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<ProyectoDto>
                {
                    Success = false,
                    Message = $"Error al guardar el proyecto: {ex.Message}"
                };
            }
        }

    }
}
