using Application.Departamentos.Dto;
using Application.Departamentos.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _service;

        public DepartamentoController(IDepartamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Save(SaveDepartamento dto)
            => Ok(await _service.SaveAsync(dto));

        [HttpDelete("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok("Eliminado correctamente");
        }
    }
}