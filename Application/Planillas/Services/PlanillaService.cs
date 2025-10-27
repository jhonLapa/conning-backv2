using Application.Exceptions;
using Application.Mantenedores.Dtos.Bancos;
using Application.Mantenedores.Dtos.Planillas;
using Application.Planillas.Dto;
using Application.Planillas.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Planillas.Services
{
    public class PlanillaService : IPlanillaServices
    {
        private readonly IPlanillaRepositorio _planillaRepositorio;
        private readonly IMapper _mapper;
        private readonly IDetallePlanillaRepositorio _detallePlanillaRepositorio;
        private readonly IAportesPlanillaRepositorio _aportesPlanillaRepositorio;

        public PlanillaService(IPlanillaRepositorio PlanillaRepositorio, IAportesPlanillaRepositorio aportesPlanillaRepositorio,  IDetallePlanillaRepositorio DetallePlanillaRepositorio, IMapper mapper)
        {
            _planillaRepositorio = PlanillaRepositorio;
            _detallePlanillaRepositorio = DetallePlanillaRepositorio;
            _aportesPlanillaRepositorio = aportesPlanillaRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<PlanillaDto>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null)
        {
            var response = await _planillaRepositorio.BusquedaPaginado(dto, descargarTodo, fechaIni, fechaFin);

            var data = _mapper.Map<ICollection<PlanillaDto>>(response.Data);

            return new PaginadoResponse<PlanillaDto>(data, response.Meta);
        }


        public async Task<OperationResult<PlanillaDto>> CreateAsync(PlanillaSaveDto saveDto)
        {
            var planilla = _mapper.Map<Planilla>(saveDto);


            await _planillaRepositorio.SaveAsync(planilla);

            return new OperationResult<PlanillaDto>()
            {
                Data = _mapper.Map<PlanillaDto>(planilla),
                Message = "Se ha Creado",

            };

        }

        public async Task<OperationResult<PlanillaDto>> DisabledAsync(int id)
        {
            var bank = await _planillaRepositorio.FindByIdAsync(id);

            if (bank == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            bank.Estado = bank.Estado == 1 ? 0 : 1;
            await _planillaRepositorio.SaveAsync(bank);

            return new OperationResult<PlanillaDto>()
            {
                Data = _mapper.Map<PlanillaDto>(bank),
                Message = bank.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<PlanillaDto>> EditAsync(int id, PlanillaSaveDto saveDto)
        {
            var planilla = await _planillaRepositorio.FindByIdAsync(id);

            if (planilla == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, planilla);

            await _planillaRepositorio.SaveAsync(planilla);

            return new OperationResult<PlanillaDto>()
            {
                Data = _mapper.Map<PlanillaDto>(planilla),
                Message = "Se ha actualizado",

            };

        }

        public async Task<IReadOnlyList<PlanillaDto>> FindAllAsync()
        {
            var response = await _planillaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<PlanillaDto>>(response);
        }

        public async Task<PlanillaDto> FindByIdAsync(int id)
        {
            var response = await _planillaRepositorio.FindByIdAsync(id);

            return _mapper.Map<PlanillaDto>(response);
        }
        public async Task<OperationResult<PlanillaDto>> CreatePlanillaCompletaAsync(PlanillaFormDataDto dto)
        {
            try
            {
                // 1️⃣ Crear o actualizar PLANILLA
                var planilla = _mapper.Map<Planilla>(dto.Planilla);

                if (planilla.IdPlanilla > 0)
                {
                    // Si existe, actualizar
                    var existente = await _planillaRepositorio.FindByIdAsync(planilla.IdPlanilla);
                    if (existente == null)
                        return new OperationResult<PlanillaDto> { Success = false, Message = "Planilla no encontrada." };

                    _mapper.Map(dto.Planilla, existente);
                    await _planillaRepositorio.SaveAsync(existente);

                    // 🧹 Eliminar detalles y aportes anteriores
                    await _detallePlanillaRepositorio.DeleteRangeAsync(planilla.IdPlanilla);
                    await _aportesPlanillaRepositorio.DeleteRangeAsync(planilla.IdPlanilla);

                    planilla = existente;
                }
                else
                {
                    // Nueva planilla
                    planilla.Estado = 1;
                    planilla.FechaCreacion = DateTime.Now;
                    planilla.UsuarioCreacion ??= "system";
                    await _planillaRepositorio.SaveAsync(planilla);
                }

                // 2️⃣ Insertar nuevos DETALLES
                foreach (var detalleDto in dto.Detalle)
                {
                    var detalle = _mapper.Map<DetallePlanilla>(detalleDto);
                    detalle.IdPlanilla = planilla.IdPlanilla;
                    detalle.FechaCreacion = DateTime.Now;
                    detalle.UsuarioCreacion ??= planilla.UsuarioCreacion;

                    detalle.TotalMonto = detalle.TotalMonto == 0 ? detalleDto.TotalMonto : detalle.TotalMonto;
                    detalle.TotalHoras = detalle.TotalHoras == 0 ? detalleDto.TotalHoras : detalle.TotalHoras;
                    detalle.TotalDescuentos = detalle.TotalDescuentos == 0 ? detalleDto.TotalDescuentos : detalle.TotalDescuentos;

                    await _detallePlanillaRepositorio.SaveAsync(detalle);
                }
                // 3️⃣ Insertar nuevos APORTES
                foreach (var aporteDto in dto.Aportes)
                {
                    var aporte = _mapper.Map<AportesPlanilla>(aporteDto);
                    aporte.IdPlanilla = planilla.IdPlanilla;
                    await _aportesPlanillaRepositorio.SaveAsync(aporte);
                }

                var resultDto = _mapper.Map<PlanillaDto>(planilla);
                return new OperationResult<PlanillaDto>
                {
                    Success = true,
                    Message = planilla.IdPlanilla > 0 ? "Planilla actualizada correctamente." : "Planilla registrada correctamente.",
                    Data = resultDto
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<PlanillaDto>
                {
                    Success = false,
                    Message = $"Error al registrar planilla: {ex.Message}"
                };
            }
        }

        public async Task<PaginadoResponse<PlanillaDto>> BusquedaPaginadoProyectoTrabajador(PaginationRequest dto, int idTrabajador, int idProyecto)
        {
            var response = await _planillaRepositorio.BusquedaPaginadoProyectoTrabajador(dto, idTrabajador, idProyecto);

            var data = _mapper.Map<ICollection<PlanillaDto>>(response.Data);

            return new PaginadoResponse<PlanillaDto>(data, response.Meta);
        }

        public async Task<BoletaDto?> ObtenerBoletaAsync(int idPlanilla, int idTrabajador)
        {
            var planilla = await _planillaRepositorio.FindByPlanillaAndTrabajadorAsync(idPlanilla, idTrabajador);

            if (planilla == null) return null;

            var detalle = planilla.Detalles.FirstOrDefault();
            if (detalle == null) return null;

            var trabajador = detalle.TrabajadorProyecto.Trabajador;
            var categoria = trabajador.Categoria;
            var regimen = trabajador.Regimen;

            var dto = new BoletaDto
            {
                IdPlanilla = planilla.IdPlanilla,
                Proyecto = planilla.Proyecto?.Nombre ?? "",
                Periodo =  planilla.Mes ?? "",
                ApellidosNombres = trabajador.ApellidosNombres,
                Dni = trabajador.NumeroDocumento,
                Categoria = categoria?.Nombre ?? "",
                Regimen = regimen?.Nombre ?? "",
                DiasTrabajados = detalle.DiasTrabajados,
                Horas60 = detalle.Horas60,
                Horas100 = detalle.Horas100,
                Indemnizacion = detalle.Indemnizacion
            };

            decimal totalIngresos = 0;
            decimal totalDescuentos = 0;
            decimal totalAportes = 0;

            // ============================================================
            // 🧩 Conceptos base de la categoría
            // ============================================================
            if (categoria?.ConceptosCategoria != null)
            {
                foreach (var concepto in categoria.ConceptosCategoria)
                {
                    var valor = concepto.Valor;

                    // Multiplicadores según tipo
                    if (concepto.NombreConcepto.ToLower().Contains("salario") ||
                        concepto.NombreConcepto.ToLower().Contains("buc") ||
                        concepto.NombreConcepto.ToLower().Contains("dso"))
                        valor = Math.Round(concepto.Valor * detalle.DiasTrabajados, 2);

                    if (concepto.NombreConcepto.ToLower().Contains("horaextra60"))
                        valor = Math.Round(valor * (detalle.Horas60 ?? 0), 2);

                    if (concepto.NombreConcepto.ToLower().Contains("horaextra100"))
                        valor = Math.Round(valor * (detalle.Horas100 ?? 0), 2);


                    if (concepto.TipoConcepto == "INGRESO") totalIngresos += valor;
                    else if (concepto.TipoConcepto == "DESCUENTO") totalDescuentos += valor;
                    else if (concepto.TipoConcepto == "APORTE") totalAportes += valor;

                    dto.Conceptos.Add(new BoletaConceptoDto
                    {
                        Codigo = $"C{concepto.IdConcepto:D4}",
                        Nombre = concepto.NombreConcepto,
                        NombreMostrar = concepto.NombreConcepto switch
                        {
                            "salarioBasico" => "BASICO",
                            "dso" => "DOMINICAL",
                            "feriado" => "FERIADO",
                            "horaExtra60" => "H.E. 60%",
                            "horaExtra100" => "H.E. 100%",
                            "buc" => "BUC",
                            "movilidad" => "MOVILIDAD",
                            "cts" => "CTS",
                            "vacaciones" => "VACACIONES TRUNCAS",
                            "gratificacion" => "GRATIFICACIONES TRUNCAS",
                            "asigEscolar" => "ASIG. ESCOLAR",
                            "bon29351" => "BON. 29351",
                            "bextraEsSalud" => "BON. EXTRA ESSALUD",
                            "aporteVoluntario1" => "APORTE VOLUNTARIO 1%",
                            _ => concepto.NombreConcepto.ToUpper()
                        },
                        Tipo = concepto.TipoConcepto,
                        Valor = valor
                    });
                }
            }

            // ============================================================
            // 🧮 Aportes previsionales (ONP o AFP)
            // ============================================================
            if (regimen != null)
            {
                var baseImponible = totalIngresos;

                if (regimen.Tipo == "ONP")
                {
                    var onp = Math.Round(baseImponible * (regimen.Aporte / 100), 2);
                    var conafovicer = Math.Round(baseImponible * 0.02m, 2);

                    totalDescuentos += onp;
                    totalAportes += conafovicer;

                    dto.Conceptos.Add(new BoletaConceptoDto { Codigo = "R0001", Nombre = "onp", NombreMostrar = "ONP", Tipo = "DESCUENTO", Valor = onp });
                    dto.Conceptos.Add(new BoletaConceptoDto { Codigo = "R0002", Nombre = "conafovicer", NombreMostrar = "CONAFOVICER", Tipo = "APORTE", Valor = conafovicer });
                }
                else if (regimen.Tipo == "AFP")
                {
                    var aporteObligatorio = Math.Round(baseImponible * (regimen.Aporte / 100), 2);
                    var comision = Math.Round(baseImponible * ((regimen.Comision ?? 0m) / 100m), 2);
                    var prima = Math.Round(baseImponible * ((regimen.Prima ?? 0m) / 100m), 2);
                    totalDescuentos += aporteObligatorio + comision + prima;

                    dto.Conceptos.Add(new BoletaConceptoDto { Codigo = "R0003", Nombre = "aporteObligatorio", NombreMostrar = "AFP APORTE OBLIGATORIO", Tipo = "DESCUENTO", Valor = aporteObligatorio });
                    dto.Conceptos.Add(new BoletaConceptoDto { Codigo = "R0004", Nombre = "comisionAfp", NombreMostrar = "AFP COMIS. VARIABLE", Tipo = "DESCUENTO", Valor = comision });
                    dto.Conceptos.Add(new BoletaConceptoDto { Codigo = "R0005", Nombre = "primaAfp", NombreMostrar = "AFP PRIMA SEGURO", Tipo = "DESCUENTO", Valor = prima });

                    // Para AFP también puedes incluir un aporte voluntario o seguro de vida ley si deseas
                    var seguroVidaLey = Math.Round(baseImponible * 0.01m, 2);
                    dto.Conceptos.Add(new BoletaConceptoDto { Codigo = "R0006", Nombre = "seguroVidaLey", NombreMostrar = "SEGURO DE VIDA LEY", Tipo = "APORTE", Valor = seguroVidaLey });
                    totalAportes += seguroVidaLey;
                }
            }

            // ============================================================
            // 🧾 Aportes de planilla (ESSALUD, SCTR)
            // ============================================================
            foreach (var aporte in planilla.AportesPlanilla)
            {
                dto.Conceptos.Add(new BoletaConceptoDto
                {
                    Codigo = $"A{aporte.IdAportePlanilla:D4}",
                    Nombre = aporte.TipoAporte.ToLower(),
                    NombreMostrar = aporte.TipoAporte.ToUpper(),
                    Tipo = "APORTE",
                    Valor = aporte.Monto
                });
                totalAportes += aporte.Monto;
            }

            // ============================================================
            // 💰 Totales finales
            // ============================================================
            dto.TotalIngresos = Math.Round(totalIngresos, 2);
            dto.TotalDescuentos = Math.Round(totalDescuentos, 2);
            dto.TotalAportes = Math.Round(totalAportes, 2);
            dto.NetoPagar = Math.Round(totalIngresos - totalDescuentos, 2);

            // 🗂 Orden visual igual a tu Excel (INGRESOS → DESCUENTOS → APORTES)
            dto.Conceptos = dto.Conceptos
                .OrderBy(c => c.Tipo == "INGRESO" ? 1 : c.Tipo == "DESCUENTO" ? 2 : 3)
                .ThenBy(c => c.Codigo)
                .ToList();

            return dto;
        }


    }
}

