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


        public async Task<OperationResult<ProyectoDto>> CreateAsync(ProyectoSaveDto saveDto)
        {
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
                Proyecto proyecto;

                // 🔹 Si tiene IdProyecto → Editar
                if (dto.Proyecto.IdProyecto > 0)
                {
                    proyecto = await _projectRepositorio.FindByIdAsync(dto.Proyecto.IdProyecto);

                    if (proyecto == null)
                        return new OperationResult<ProyectoDto>
                        {
                            Success = false,
                            Message = "Proyecto no encontrado."
                        };

                    // ✅ Actualizar solo los campos principales del proyecto
                    proyecto.Nombre = dto.Proyecto.Nombre;
                    proyecto.Descripcion = dto.Proyecto.Descripcion;
                    proyecto.FechaInicio = DateTime.Parse(dto.Proyecto.FechaInicio);
                    proyecto.FechaFin = DateTime.Parse(dto.Proyecto.FechaFin);
                    proyecto.FrecuenciaPago = dto.Proyecto.FrecuenciaPago;
                    proyecto.UsuarioModificacion = dto.Proyecto.UsuarioCreacion;
                    proyecto.FechaModificacion = DateTime.Now;

                    await _projectRepositorio.SaveAsync(proyecto);
                }
                else
                {
                    // 🔹 Crear nuevo proyecto
                    proyecto = _mapper.Map<Proyecto>(dto.Proyecto);
                    proyecto.FechaCreacion = DateTime.Now;
                    proyecto.Estado = 1;

                    await _projectRepositorio.SaveAsync(proyecto);
                }

                // 🧩 Trabajadores: agregar nuevos o actualizar estado
                if (dto.Trabajador is { Count: > 0 })
                {
                    foreach (var t in dto.Trabajador)
                    {
                        var existente = await _trabajadorProyectoRepositorio
                            .FindByProyectoYTrabajadorAsync(proyecto.IdProyecto, t.IdTrabajador);

                        if (existente == null)
                        {
                            // ➕ Nuevo trabajador
                            var trabajadorProyecto = new TrabajadorProyecto
                            {
                                IdProyecto = proyecto.IdProyecto,
                                IdTrabajador = t.IdTrabajador,
                                FechaInicio = DateTime.Parse(t.FechaInicio),
                                FechaFin = DateTime.Parse(t.FechaFin),
                                Estado = t.Estado,
                                UsuarioCreacion = t.UsuarioCreacion,
                                FechaCreacion = DateTime.Now
                            };
                            await _trabajadorProyectoRepositorio.SaveAsync(trabajadorProyecto);
                        }
                        else
                        {
                            // 🔁 Solo cambio de estado
                            existente.Estado = t.Estado;
                            await _trabajadorProyectoRepositorio.SaveAsync(existente);
                        }
                    }
                }

                // 🧩 Sindicato: agregar o actualizar estado
                if (dto.Sindicato is { Count: > 0 })
                {
                    foreach (var s in dto.Sindicato)
                    {
                        var existente = await _aportesSindicatoRepositorio
                            .FindByProyectoMesAnioAsync(proyecto.IdProyecto, s.Mes, s.Anio);

                        if (existente == null)
                        {
                            var aporte = new AportesSindicato
                            {
                                IdProyecto = proyecto.IdProyecto,
                                Mes = s.Mes,
                                Anio = s.Anio,
                                Monto = s.Monto,
                                FechaPago = DateTime.Parse(s.FechaPago),
                                Estado = s.Estado,
                                UsuarioCreacion = s.UsuarioCreacion,
                                FechaCreacion = DateTime.Now
                            };
                            await _aportesSindicatoRepositorio.SaveAsync(aporte);
                        }
                        else
                        {
                            // Solo cambio de estado o monto
                            existente.Estado = s.Estado;
                            existente.Monto = s.Monto;
                            existente.FechaPago = DateTime.Parse(s.FechaPago);
                            await _aportesSindicatoRepositorio.SaveAsync(existente);
                        }
                    }
                }

                // 🧩 Encargado: actualizar si existe, o crear si no
                if (dto.ProyectoEncargado != null)
                {
                    var encargadoExistente = await _proyectoEncargadoRepositorio
                        .FindByProyectoAsync(proyecto.IdProyecto);

                    if (encargadoExistente == null)
                    {
                        var encargado = new ProyectoEncargado
                        {
                            IdProyecto = proyecto.IdProyecto,
                            IdTrabajador = dto.ProyectoEncargado.IdTrabajador,
                            Rol = dto.ProyectoEncargado.Rol,
                            FechaInicio = DateTime.Parse(dto.ProyectoEncargado.FechaInicio),
                            FechaFin = DateTime.Parse(dto.ProyectoEncargado.FechaFin),
                            Estado = dto.ProyectoEncargado.Estado
                        };
                        await _proyectoEncargadoRepositorio.SaveAsync(encargado);
                    }
                    else
                    {
                        encargadoExistente.Estado = dto.ProyectoEncargado.Estado;
                        encargadoExistente.Rol = dto.ProyectoEncargado.Rol;
                        await _proyectoEncargadoRepositorio.SaveAsync(encargadoExistente);
                    }
                }


                // 🔹 Recuperar todo el proyecto con relaciones
                var projectWithIncludes = await _projectRepositorio.FindByIdAsync(proyecto.IdProyecto);
                var mappedDto = _mapper.Map<ProyectoDto>(projectWithIncludes);

                return new OperationResult<ProyectoDto>
                {
                    Success = true,
                    Message = dto.Proyecto.IdProyecto > 0
                        ? "Proyecto actualizado correctamente"
                        : "Proyecto completo creado con éxito",
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
