using Application.Empleados.Dtos.EmployeesBanksAccounts;
using Application.Empleados.Services.Interfaces;
using Application.Mantenedores.Dtos.Bancks;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBankAccountsController : ControllerBase
    {
        private readonly IEmployeeBankAccountServices _employeeBankAccountServices;
        public EmployeeBankAccountsController(IEmployeeBankAccountServices employeeBankAccountServices)
        {
            _employeeBankAccountServices = employeeBankAccountServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<EmployeeBankAccountDto>>>> Get()
        {

            var response = await _employeeBankAccountServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<EmployeeBankAccountDto>>> Get(int id)
        {
            var response = await _employeeBankAccountServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EmployeeBankAccountDto>>>> Post([FromBody] EmployeeBankAccountSaveDto request)
        {

            var response = await _employeeBankAccountServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EmployeeBankAccountDto>>>> Put(int id, [FromBody] EmployeeBankAccountSaveDto request)
        {

            var response = await _employeeBankAccountServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
