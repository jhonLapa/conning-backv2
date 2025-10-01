using Application.CuentasBancariasTrabajador.Dtos;
using Application.CuentasBancariasTrabajador.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaBancariaTrabajadorController : ControllerBase
    {
        private readonly IEmployeeBankAccountServices _employeeBankAccountServices;
        public CuentaBancariaTrabajadorController(IEmployeeBankAccountServices employeeBankAccountServices)
        {
            _employeeBankAccountServices = employeeBankAccountServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<CuentaBancariaTrabajadorDto>>>> Get()
        {

            var response = await _employeeBankAccountServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<CuentaBancariaTrabajadorDto>>> Get(int id)
        {
            var response = await _employeeBankAccountServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CuentaBancariaTrabajadorDto>>>> Post([FromBody] CuentaBancariaTrabajadorSaveDto request)
        {

            var response = await _employeeBankAccountServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CuentaBancariaTrabajadorDto>>>> Put(int id, [FromBody] CuentaBancariaTrabajadorSaveDto request)
        {

            var response = await _employeeBankAccountServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
