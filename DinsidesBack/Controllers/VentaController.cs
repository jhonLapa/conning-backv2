using Application.Ventas.Dto;
using Application.Ventas.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaServices _ventaService;
        public VentaController(IVentaServices VentaService) => _ventaService = VentaService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<VentaDto>>>> Get()
        {

            var response = await _ventaService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<NotFound<OperationResult<VentaDto>>, Ok<OperationResult<VentaDto>>>> Get(int id)
        {
            var response = await _ventaService.FindByIdAsync(id);

            if (response == null)
            {
                return TypedResults.NotFound(new OperationResult<VentaDto>
                {
                    Data = null,
                    Message = $"No se encontró ninguna venta con el Id {id}"
                });
            }

            return TypedResults.Ok(new OperationResult<VentaDto>
            {
                Data = response,
            });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<VentaDto>>>> Post([FromBody] VentaSaveDto request)
        {

            var response = await _ventaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<VentaDto>>>> Put(int id, [FromBody] VentaSaveDto request)
        {

            var response = await _ventaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("SelectActivo")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<VentaSelectDto>>>> SelSelectActivoect()
        {

            var response = await _ventaService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("cliente/{clienteId}")]
        [AllowAnonymous]
        public async Task<Results<
            NotFound<OperationResult<List<VentaDto>>>,
            Ok<OperationResult<List<VentaDto>>>>> GetByClienteId(int clienteId)
        {
            var result = await _ventaService.FindByClienteIdAsync(clienteId);

            if (result.Data == null || !result.Data.Any())
                return TypedResults.NotFound(result);

            return TypedResults.Ok(result);
        }


        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<VentaDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _ventaService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<VentaDto>>>> Delete(int id)
        {
            var response = await _ventaService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost("RegistrarCompleto")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<VentaDto>>>> PostCompleto([FromBody] VentaCompletoSaveDto request)
        {
            var response = await _ventaService.CreateWithDetailsAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("Descargar")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, FileContentHttpResult>> Descargar([FromQuery] PaginationRequest dto)
        {
            // ⚙️ Detectar si hay filtros activos
            bool tieneFiltros = dto.Filters != null && dto.Filters.Length > 0;

            // ✅ Si hay filtros, filtra; si no, descarga todo
            var response = await _ventaService.BusquedaPaginado(dto, descargarTodo: !tieneFiltros);

            if (response == null || response.Data == null || response.Data.Count == 0)
                return TypedResults.BadRequest();

            // 🔹 Crear Excel
            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var ws = workbook.Worksheets.Add("Ventas");

            // 🔹 Encabezados
            ws.Cell(1, 1).Value = "Cliente";
            ws.Cell(1, 2).Value = "Proyecto";
            ws.Cell(1, 3).Value = "Tipo Comprobante";
            ws.Cell(1, 4).Value = "Serie";
            ws.Cell(1, 5).Value = "Número";
            ws.Cell(1, 6).Value = "Importe Total";
            ws.Cell(1, 7).Value = "Estado";

            // 🔹 Datos
            int row = 2;
            foreach (var v in response.Data)
            {
                ws.Cell(row, 1).Value = v.Cliente?.NombreCompleto ?? "";
                ws.Cell(row, 2).Value = v.Proyecto?.Nombre ?? "";
                ws.Cell(row, 3).Value = v.TipoComprobante?.Nombre ?? "";
                ws.Cell(row, 4).Value = v.Serie ?? "";
                ws.Cell(row, 5).Value = v.Numero ?? "";
                ws.Cell(row, 6).Value = v.ImporteTotal;
                ws.Cell(row, 7).Value = v.Estado == 1 ? "Pagado" : "Pendiente";
                row++;
            }

            ws.Columns().AdjustToContents();
            ws.Row(1).Style.Font.Bold = true;
            ws.SheetView.FreezeRows(1);

            // 🔹 Exportar como archivo
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var bytes = stream.ToArray();

            return TypedResults.File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Ventas.xlsx"
            );
        }

    }
}
