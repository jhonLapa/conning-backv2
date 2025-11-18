using Application.Dashboard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<DashboardDto>> GetDashboard(
        [FromQuery] DateTime? fechaInicio,
        [FromQuery] DateTime? fechaFin)
        {
            try
            {
                // 📅 Si no se envían fechas, tomar rango del mes actual
                var inicio = fechaInicio ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fin = fechaFin ?? DateTime.Now;

                var data = await _dashboardService.GetDashboardAsync(inicio, fin);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener dashboard", error = ex.Message });
            }
        }

    }
}
