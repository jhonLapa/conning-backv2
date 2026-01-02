using Application.Mantenedores.Dtos.MovimientosEspeciales;
using Application.Mantenedores.Services;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoEspecialController : ControllerBase
    {
        private readonly IMovimientoEspecialService _movimientoEspecialService;
        public MovimientoEspecialController(IMovimientoEspecialService movimientoEspecialService) => _movimientoEspecialService = movimientoEspecialService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<MovimientoEspecialDto>>>> Get()
        {

            var response = await _movimientoEspecialService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<MovimientoEspecialDto>>> Get(int id)
        {
            var response = await _movimientoEspecialService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MovimientoEspecialDto>>>> Post([FromBody] MovimientoEspecialSaveDto request)
        {

            var response = await _movimientoEspecialService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MovimientoEspecialDto>>>> Put(int id, [FromBody] MovimientoEspecialSaveDto request)
        {

            var response = await _movimientoEspecialService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<MovimientoEspecialDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto,
            string fechaIni = null,
            string fechaFin = null)
        {
            var response = await _movimientoEspecialService.BusquedaPaginado(dto, fechaIni: fechaIni, fechaFin: fechaFin);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MovimientoEspecialDto>>>> Delete(int id)
        {
            var response = await _movimientoEspecialService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpGet("SelectActivos")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<MovimientoEspecialSelectDto>>>> SelectActivo()
        {

            var response = await _movimientoEspecialService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("Descargar")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, FileContentHttpResult>> Descargar(
         [FromQuery] PaginationRequest dto,
         string fechaIni = null,
         string fechaFin = null)
        {

            bool tieneFiltros = dto.Filters != null && dto.Filters.Length > 0;

            var response = await _movimientoEspecialService.BusquedaPaginado(
                dto,
                descargarTodo: !tieneFiltros,
                fechaIni: fechaIni,
                fechaFin: fechaFin
            );

            if (response == null || response.Data == null || response.Data.Count == 0)
                return TypedResults.BadRequest();

            //Crear Excel
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var ws = workbook.Worksheets.Add("MovimientoEspeciales");

            //Encabezados
            ws.Cell(1, 1).Value = "Descripción";
            ws.Cell(1, 2).Value = "Monto";
            ws.Cell(1, 3).Value = "Tipo Movimiento";
            ws.Cell(1, 4).Value = "Cuenta Bancaria";
            ws.Cell(1, 5).Value = "Proyecto";
            ws.Cell(1, 6).Value = "Estado";

            //Datos
            int row = 2;
            foreach (var v in response.Data)
            {
                ws.Cell(row, 1).Value = v.Descripcion ?? "";
                ws.Cell(row, 2).Value = v.Monto.ToString("0.00");
                ws.Cell(row, 3).Value = v.TipoMovimiento ?? "";
                ws.Cell(row, 4).Value = v.CuentaBancaria ?? "";
                ws.Cell(row, 5).Value = v.Observacion ?? "";
                ws.Cell(row, 6).Value = v.Estado == 1 ? "Pagado" : "Pendiente";
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
                "MovimientoEspeciales.xlsx"
            );
        }



        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<string>>>> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return TypedResults.BadRequest();

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);

                using var package = new ExcelPackage(stream);
                var sheet = package.Workbook.Worksheets[0];

                var registros = new List<MovimientoEspecialSaveDto>();

                for (int row = 2; row <= sheet.Dimension.Rows; row++)
                {
                    var dto = new MovimientoEspecialSaveDto
                    {
                        Fecha = sheet.Cells[row, 1].GetValue<DateTime>(),
                        TipoMovimiento = sheet.Cells[row, 2].Text,
                        Monto = sheet.Cells[row, 3].GetValue<decimal>(),
                        CuentaBancaria = sheet.Cells[row, 4].Text,
                        Descripcion = sheet.Cells[row, 5].Text,
                        Observacion = sheet.Cells[row, 6].Text
                    };

                    registros.Add(dto);
                }

                foreach (var item in registros)
                {
                    await _movimientoEspecialService.CreateAsync(item);
                }

                return TypedResults.Ok(new OperationResult<string>
                {
                    Success = true,
                    Message = "Carga masiva completada exitosamente"
                });
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest();
            }
        }



        [HttpPost("upload/preview")]
        [AllowAnonymous]
        public async Task<IResult> PreviewExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Results.BadRequest("Archivo vacío o inválido");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var package = new ExcelPackage(stream);
            var sheet = package.Workbook.Worksheets[0];

            var preview = new List<object>();
            var errores = new List<object>();

            int rowCount = sheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                var rowErrors = new List<string>();

                string fechaStr = sheet.Cells[row, 1].Text;
                string tipo = sheet.Cells[row, 2].Text?.Trim().ToUpper();
                string montoStr = sheet.Cells[row, 3].Text;
                string cuenta = sheet.Cells[row, 4].Text;
                string descripcion = sheet.Cells[row, 5].Text;
                string observacion = sheet.Cells[row, 6].Text;

                // ===== VALIDACIONES =====

                // Fecha
                DateTime fecha;
                if (!DateTime.TryParse(fechaStr, out fecha))
                    rowErrors.Add("Fecha inválida");

                // Tipo movimiento
                if (tipo != "INGRESO" && tipo != "EGRESO")
                    rowErrors.Add("TipoMovimiento debe ser INGRESO o EGRESO");

                // Monto
                decimal monto;
                if (!decimal.TryParse(montoStr, out monto) || monto <= 0)
                    rowErrors.Add("Monto inválido (debe ser número > 0)");

                // Descripción obligatoria
                if (string.IsNullOrWhiteSpace(descripcion))
                    rowErrors.Add("Descripción es obligatoria");

                // Armar objeto fila
                var dto = new MovimientoEspecialSaveDto
                {
                    Fecha = fecha,
                    TipoMovimiento = tipo,
                    Monto = monto,
                    CuentaBancaria = cuenta,
                    Descripcion = descripcion,
                    Observacion = observacion
                };

                // Si tiene errores →
                if (rowErrors.Any())
                {
                    errores.Add(new
                    {
                        fila = row,
                        errores = rowErrors,
                        datos = dto
                    });
                }

                // Agregar a previsualización siempre
                preview.Add(new
                {
                    fila = row,
                    datos = dto,
                    errores = rowErrors
                });
            }

            return Results.Ok(new
            {
                totalFilas = rowCount - 1,
                totalErrores = errores.Count,
                errores,
                preview,
                todoOK = errores.Count == 0
            });
        }

        [HttpPost("upload/confirm")]
        [AllowAnonymous]
        public async Task<IResult> ConfirmarCarga([FromBody] List<MovimientoEspecialSaveDto> registros)
        {
            if (registros == null || registros.Count == 0)
                return Results.BadRequest("No hay registros para guardar");

            foreach (var item in registros)
                await _movimientoEspecialService.CreateAsync(item);

            return Results.Ok(new
            {
                message = "Carga masiva registrada correctamente",
                total = registros.Count
            });
        }


        [HttpGet("plantilla")]
        [AllowAnonymous]
        public IResult DescargarPlantilla()
        {
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var sheet = workbook.Worksheets.Add("Plantilla");

            // Encabezados
            sheet.Cell(1, 1).Value = "Fecha";
            sheet.Cell(1, 2).Value = "Tipo Movimiento";
            sheet.Cell(1, 3).Value = "Monto";
            sheet.Cell(1, 4).Value = "Cuenta Bancaria";
            sheet.Cell(1, 5).Value = "Descripcion";
            sheet.Cell(1, 6).Value = "Proyecto";

            // Estilo encabezados
            var header = sheet.Range("A1:F1");
            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;

            // Fila de ejemplo
            sheet.Cell(2, 1).Value = DateTime.Now.ToString("yyyy-MM-dd");
            sheet.Cell(2, 2).Value = "INGRESO / EGRESO";
            sheet.Cell(2, 3).Value = 1500.50m;
            sheet.Cell(2, 4).Value = "BBVA 123-456";
            sheet.Cell(2, 5).Value = "Pago Cliente X";
            sheet.Cell(2, 6).Value = "Ejemplo";

            sheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var bytes = stream.ToArray();

            return Results.File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Plantilla_MovimientosEspeciales.xlsx"
            );
        }



    }
}
