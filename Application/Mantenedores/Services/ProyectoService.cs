using Application.Exceptions;
using Application.Mantenedores.Dtos.Proyectos;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;
using System;
using System.Globalization;

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

                // ---------------------------------------------------------
                // 🟢 Crear o actualizar PROYECTO
                // ---------------------------------------------------------
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

                    // 🔥 Eliminar registros anteriores excepto encargados
                    await _trabajadorProyectoRepositorio.DeleteByProyectoIdAsync(proyecto.IdProyecto);
                    await _aportesSindicatoRepositorio.DeleteByProyectoIdAsync(proyecto.IdProyecto);
                }

                // ---------------------------------------------------------
                // 👷 Guardar TRABAJADORES
                // ---------------------------------------------------------
                if (dto.Trabajador != null && dto.Trabajador.Any())
                {
                    foreach (var t in dto.Trabajador)
                    {
                        var trabajadorProyecto = new TrabajadorProyecto
                        {
                            IdProyecto = proyecto.IdProyecto,
                            IdTrabajador = t.IdTrabajador,
                            FechaInicio = t.FechaInicio,
                            Estado = t.Estado,
                            UsuarioCreacion = t.UsuarioCreacion,
                            FechaCreacion = DateTime.Now
                        };
                        await _trabajadorProyectoRepositorio.SaveAsync(trabajadorProyecto);
                    }
                }

                // ---------------------------------------------------------
                // 🤝 Guardar APORTES SINDICATO
                // ---------------------------------------------------------
                if (dto.Sindicato != null && dto.Sindicato.Any())
                {
                    foreach (var s in dto.Sindicato)
                    {
                        var aporte = new AportesSindicato
                        {
                            IdProyecto = proyecto.IdProyecto,
                            Mes = s.Mes,
                            Anio = s.Anio,
                            Monto = s.Monto,
                            FechaPago = s.FechaPago,
                            UsuarioCreacion = s.UsuarioCreacion,
                            FechaCreacion = DateTime.Now
                        };
                        await _aportesSindicatoRepositorio.SaveAsync(aporte);
                    }
                }

                // ---------------------------------------------------------
                // 👨‍💼 ENCARGADO DEL PROYECTO
                // ---------------------------------------------------------
                if (dto.ProyectoEncargado != null)
                {
                    // 🔍 Obtener el último encargado registrado para este proyecto
                    var ultimoEncargado = await _proyectoEncargadoRepositorio
                        .FindLastByProyectoAsync(proyecto.IdProyecto); // ← Debe devolver el último por fecha

                    if (ultimoEncargado == null)
                    {
                        // ➕ No existe ninguno → crear nuevo
                        var nuevoEncargado = new ProyectoEncargado
                        {
                            IdProyecto = proyecto.IdProyecto,
                            IdTrabajador = dto.ProyectoEncargado.IdTrabajador,
                            Rol = dto.ProyectoEncargado.Rol,
                            FechaInicio = dto.ProyectoEncargado.FechaInicio,
                            Estado =1,
                        };
                        await _proyectoEncargadoRepositorio.SaveAsync(nuevoEncargado);
                    }
                    else
                    {
                        // 🔁 Existe encargado → comparar solo con el último
                        if (ultimoEncargado.IdTrabajador == dto.ProyectoEncargado.IdTrabajador)
                        {
                            // ✅ Mismo trabajador → solo actualizar sus datos
                            ultimoEncargado.Rol = dto.ProyectoEncargado.Rol;
                            ultimoEncargado.FechaInicio = dto.ProyectoEncargado.FechaInicio;
                            await _proyectoEncargadoRepositorio.SaveAsync(ultimoEncargado);
                        }
                        else
                        {
                            // ⚡ Distinto trabajador → crear nuevo registro (histórico)
                            var nuevoEncargado = new ProyectoEncargado
                            {
                                IdProyecto = proyecto.IdProyecto,
                                IdTrabajador = dto.ProyectoEncargado.IdTrabajador,
                                Rol = dto.ProyectoEncargado.Rol,
                                FechaInicio = dto.ProyectoEncargado.FechaInicio,
                                Estado = 1,
                            };
                            await _proyectoEncargadoRepositorio.SaveAsync(nuevoEncargado);
                        }
                    }
                }

                // ---------------------------------------------------------
                // 🟩 Retornar resultado
                // ---------------------------------------------------------
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
