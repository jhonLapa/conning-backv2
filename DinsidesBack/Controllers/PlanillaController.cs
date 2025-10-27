using Application.Mantenedores.Dtos.Planillas;
using Application.Planillas.Dto;
using Application.Planillas.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanillaController : ControllerBase
    {
        private readonly IPlanillaServices _planillaService;
        public PlanillaController(IPlanillaServices PlanillaService) => _planillaService = PlanillaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<PlanillaDto>>>> Get()
        {

            var response = await _planillaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<PlanillaDto>>, Ok<OperationResult<PlanillaDto>>>> Get(int id)
        {
            var response = await _planillaService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<PlanillaDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna planilla con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<PlanillaDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PlanillaDto>>>> Post([FromBody] PlanillaSaveDto request)
        {

            var response = await _planillaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PlanillaDto>>>> Put(int id, [FromBody] PlanillaSaveDto request)
        {

            var response = await _planillaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<PlanillaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto, string fechaIni = null,
            string fechaFin = null)
        {
            var response = await _planillaService.BusquedaPaginado(dto, fechaIni: fechaIni, fechaFin: fechaFin);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PlanillaDto>>>> Delete(int id)
        {
            var response = await _planillaService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }


        [HttpPost("RegistroCompleto")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PlanillaDto>>>> PostCompleta([FromBody] PlanillaFormDataDto request)
        {
            if (request == null)
            {
                return TypedResults.BadRequest();
            }

            var response = await _planillaService.CreatePlanillaCompletaAsync(request);
            if (response != null) return TypedResults.Ok(response);

            return TypedResults.Ok(response);
        }

        [HttpGet("Descargar")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, FileContentHttpResult>> Descargar(
         [FromQuery] PaginationRequest dto,
         string fechaIni = null,
         string fechaFin = null)
        {
            bool tieneFiltros = dto.Filters != null && dto.Filters.Length > 0;



            var response = await _planillaService.BusquedaPaginado(
                dto,
                descargarTodo: !tieneFiltros,
                fechaIni: fechaIni,
                fechaFin: fechaFin
            );

            if (response == null || response.Data == null || response.Data.Count == 0)
                return TypedResults.BadRequest();

            //Crear Excel
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var ws = workbook.Worksheets.Add("Plantillas");

            //Encabezados
            ws.Cell(1, 1).Value = "Proyecto";
            ws.Cell(1, 2).Value = "Mes";
            ws.Cell(1, 3).Value = "Periodo Inicio";
            ws.Cell(1, 4).Value = "Periodo Fin";
            ws.Cell(1, 5).Value = "Estado";

            //Datos
            int row = 2;
            foreach (var v in response.Data)
            {
                ws.Cell(row, 1).Value = v.Proyecto?.Nombre ?? "";
                ws.Cell(row, 2).Value = v.Mes ?? "";
                ws.Cell(row, 3).Value = v.PeriodoInicio;
                ws.Cell(row, 4).Value = v.PeriodoFin;
                ws.Cell(row, 5).Value = v.Estado == 1 ? "Pagado" : "Pendiente";
                row++;
            }

            ws.Columns().AdjustToContents();
            ws.Row(1).Style.Font.Bold = true;
            ws.SheetView.FreezeRows(1);

            //Exportar como archivo
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var bytes = stream.ToArray();

            return TypedResults.File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Plantillas.xlsx"
            );
        }

        [HttpGet("BusquedaPaginadoProyectoTrabajador")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<PlanillaDto>>>> BusquedaPaginadoProyectoTrabajador([FromQuery] PaginationRequest dto, int idTrabajador, int idProyecto)
        {
            var response = await _planillaService.BusquedaPaginadoProyectoTrabajador(dto, idTrabajador, idProyecto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }



        [HttpGet("ObtenerBoletaAsync/{idPlanilla}/{idTrabajador}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<BoletaDto>>, Ok<OperationResult<BoletaDto>>>> ObtenerBoletaAsync(int idPlanilla, int idTrabajador)
        {
            var response = await _planillaService.ObtenerBoletaAsync(idPlanilla, idTrabajador);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<BoletaDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna planilla con el Id {idPlanilla}"
                });
            }

            return TypedResults.Ok(new OperationResult<BoletaDto>
            {
                Data = response,
            });
        }


    }
}
