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

        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            try
            {
                var data = await _dashboardService.GetDashboardAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener dashboard", error = ex.Message });
            }
        }
    }
}
