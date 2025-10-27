using Application.Mantenedores.Dtos.MovimientosEspeciales;
using Application.Mantenedores.Services;
using Application.Mantenedores.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
            ws.Cell(1, 5).Value = "Observación";
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
    }
}
