using Application.Mantenedores.Dtos.Trabajadores;
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
    public class TrabajadorController : ControllerBase
    {
        private readonly ITrabajadorService _trabajadorService;
        public TrabajadorController(ITrabajadorService trabajadorService) => _trabajadorService = trabajadorService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TrabajadorDto>>>> Get()
        {

            var response = await _trabajadorService.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<TrabajadorDto>>> Get(int id)
        {
            var response = await _trabajadorService.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorDto>>>> Post([FromBody] TrabajadorSaveDto request)
        {

            var response = await _trabajadorService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorDto>>>> Put(int id, [FromBody] TrabajadorSaveDto request)
        {

            var response = await _trabajadorService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("BusquedaPaginado")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<TrabajadorDto>>>> BusquedaPaginado([FromQuery] PaginationRequest dto)
        {
            var response = await _trabajadorService.BusquedaPaginado(dto);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorDto>>>> Delete(int id)
        {
            var response = await _trabajadorService.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpGet("SelectActivos")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<TrabajadorSelectDto>>>> SelectActivo()
        {

            var response = await _trabajadorService.SelectActivo();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpPost("with-accounts")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<TrabajadorDto>>>> PostWithAccounts([FromBody] TrabajadorWithAccountsSaveDto request)
        {
            var response = await _trabajadorService.CreateOrUpdateWithAccountsAsync(request);
            if (response != null) return TypedResults.Ok(response);
            return TypedResults.BadRequest();
        }

        [HttpGet("{id}/detalle-planilla")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDetallePlanilla(int id)
        {
            var result = await _trabajadorService.GetDetallePlanillaAsync(id);

            if (result.Success == true)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("SelectByProyecto/{idProyecto}")]
        [AllowAnonymous]
        public async Task<IActionResult> SelectByProyecto(int idProyecto)
        {
            var response = await _trabajadorService.SelectByProyecto(idProyecto);

            if (response != null)
                return Ok(response);

            return BadRequest();
        }

        [HttpGet("BusquedaPaginadoConPlanilla")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PaginadoResponse<TrabajadorDto>>>> BusquedaPaginadoConPlanilla(
                [FromQuery] PaginationRequest dto,
                DateTime? fechaInicio = null,
                DateTime? fechaFin = null)
        {
            var response = await _trabajadorService.BusquedaPaginadoConPlanilla(dto, fechaInicio, fechaFin);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("plantilla")]
        [AllowAnonymous]
        public async Task<IResult> DescargarPlantilla()
        {
            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Plantilla");

            string[] headers = new[]
            {
                    "TipoDocumento",
                    "NumeroDocumento",
                    "ApellidosNombres",
                    "Categoria",
                    "Regimen",
                    "FechaNacimiento",
                    "Telefono",
                    "Email",
                    "Sexo",
                    "EstadoCivil",
                    "Direccion",
                    "Hijos",
                    "Banco",
                    "NumeroCuenta",
                    "Cci",
                    "TipoCuenta",
                    "Moneda"
              };

            for (int i = 0; i < headers.Length; i++)
                sheet.Cells[1, i + 1].Value = headers[i];

            sheet.Cells[1, 1, 1, headers.Length].Style.Font.Bold = true;

            // Ejemplo
            sheet.Cells[2, 1].Value = "DNI";
            sheet.Cells[2, 2].Value = "12345678";
            sheet.Cells[2, 3].Value = "Juan Pérez";
            sheet.Cells[2, 4].Value = "Operario";
            sheet.Cells[2, 5].Value = "General";
            sheet.Cells[2, 6].Value = "1990-05-20";
            sheet.Cells[2, 7].Value = "987654321";
            sheet.Cells[2, 8].Value = "correo@correo.com";
            sheet.Cells[2, 9].Value = "M";
            sheet.Cells[2, 10].Value = "Soltero";
            sheet.Cells[2, 11].Value = "Av. Siempre Viva 123";
            sheet.Cells[2, 12].Value = 0;
            sheet.Cells[2, 13].Value = "BBVA";
            sheet.Cells[2, 14].Value = "0011-2222-3333";
            sheet.Cells[2, 15].Value = "002233445566";
            sheet.Cells[2, 16].Value = "AHORROS";
            sheet.Cells[2, 17].Value = "PEN";

            sheet.Cells.AutoFitColumns();

            var bytes = await package.GetAsByteArrayAsync();

            return Results.File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Plantilla_Trabajadores.xlsx"
            );
        }


        [HttpPost("upload/preview")]
        [AllowAnonymous]
        public async Task<IResult> PreviewExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Results.BadRequest("Archivo inválido");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var package = new ExcelPackage(stream);
            var sheet = package.Workbook.Worksheets[0];

            var preview = new List<object>();
            var errores = new List<object>();

            int rows = sheet.Dimension.Rows;

            for (int row = 2; row <= rows; row++)
            {
                var rowErrors = new List<string>();

                string tipoDocumento = sheet.Cells[row, 1].Text.Trim();
                string numeroDoc = sheet.Cells[row, 2].Text.Trim();
                string nombre = sheet.Cells[row, 3].Text.Trim();
                string categoria = sheet.Cells[row, 4].Text.Trim();
                string regimen = sheet.Cells[row, 5].Text.Trim();
                string fechaNac = sheet.Cells[row, 6].Text.Trim();
                string banco = sheet.Cells[row, 13].Text.Trim();
                string nroCuenta = sheet.Cells[row, 14].Text.Trim();

                // VALIDACIONES
                if (string.IsNullOrWhiteSpace(tipoDocumento))
                    rowErrors.Add("TipoDocumento requerido");

                if (string.IsNullOrWhiteSpace(numeroDoc))
                    rowErrors.Add("NumeroDocumento requerido");

                if (string.IsNullOrWhiteSpace(nombre))
                    rowErrors.Add("Nombre requerido");

                if (string.IsNullOrWhiteSpace(categoria))
                    rowErrors.Add("Categoria requerida");

                if (string.IsNullOrWhiteSpace(banco))
                    rowErrors.Add("Banco requerido");

                // fecha
                DateTime? fecha = null;
                if (!string.IsNullOrEmpty(fechaNac))
                {
                    if (DateTime.TryParse(fechaNac, out var f))
                        fecha = f;
                    else
                        rowErrors.Add("FechaNacimiento inválida");
                }

                // Montar DTO
                var dto = new
                {
                    tipoDocumento,
                    numeroDoc,
                    nombre,
                    categoria,
                    regimen,
                    fechaNacimiento = fecha,
                    telefono = sheet.Cells[row, 7].Text,
                    email = sheet.Cells[row, 8].Text,
                    sexo = sheet.Cells[row, 9].Text,
                    estadoCivil = sheet.Cells[row, 10].Text,
                    direccion = sheet.Cells[row, 11].Text,
                    hijos = sheet.Cells[row, 12].GetValue<int>(),
                    banco,
                    nroCuenta,
                    cci = sheet.Cells[row, 15].Text,
                    tipoCuenta = sheet.Cells[row, 16].Text,
                    moneda = sheet.Cells[row, 17].Text
                };

                // Si hay errores → agregar
                if (rowErrors.Any())
                    errores.Add(new { fila = row, errores = rowErrors });

                // Agregar al preview
                preview.Add(new
                {
                    fila = row,
                    datos = dto,
                    errores = rowErrors
                });
            }

            return Results.Ok(new
            {
                preview,
                errores,
                todoOK = errores.Count == 0
            });
        }
        [HttpPost("upload/confirm")]
        [AllowAnonymous]
        public async Task<IResult> Confirm([FromBody] List<TrabajadorMasivoDto> registros)
        {
            var result = await _trabajadorService.ProcesarCargaMasivaAsync(registros);
            return Results.Ok(result);
        }




    }
}
