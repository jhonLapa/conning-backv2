using Application.Empleados.Dtos.EmployeesBanksAccounts;
using Application.Empleados.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empleados.Services
{
    public class EmployeeBankAccountService : IEmployeeBankAccountServices
    {
        private readonly IEmployeeBankAccountRepositorio _employeeBankAccountRepositorio;
        private readonly IMapper _mapper;

        public EmployeeBankAccountService(IEmployeeBankAccountRepositorio employeeBankAccountRepositorio, IMapper mapper)
        {
            _employeeBankAccountRepositorio = employeeBankAccountRepositorio;
            _mapper = mapper;   
        }

        public async Task<OperationResult<EmployeeBankAccountDto>> CreateAsync(EmployeeBankAccountSaveDto saveDto)
        {
            var employeeBankAccount = _mapper.Map<EmployeeAccountBanck>(saveDto);

            employeeBankAccount.AuditCreateDate = DateTime.Now;
            employeeBankAccount.AuditCreateUser = 1;

            await _employeeBankAccountRepositorio.SaveAsync(employeeBankAccount);

            return new OperationResult<EmployeeBankAccountDto>()
            {
                Data = _mapper.Map<EmployeeBankAccountDto>(employeeBankAccount),
                Message = "Cuenta del Empleado se ha Creado",
                State = true,
            };

        }

        public async Task<OperationResult<EmployeeBankAccountDto>> DisabledAsync(int id)
        {
            var employeeBankAccounts = await _employeeBankAccountRepositorio.FindByIdAsync(id);
            if (employeeBankAccounts == null) throw new NotFoundCoreException("Registro no encontrado con el id");
            employeeBankAccounts.AuditDeleteDate = DateTime.Now;
            employeeBankAccounts.State = !employeeBankAccounts.State;
            
            return new OperationResult<EmployeeBankAccountDto>()
            {
                Data = _mapper.Map<EmployeeBankAccountDto>(employeeBankAccounts),
                Message = "Cuenta de el empleado se ha Desactivado",
                State = true,
            };
        }

        public async Task<OperationResult<EmployeeBankAccountDto>> EditAsync(int id, EmployeeBankAccountSaveDto saveDto)
        {
            var employeeBankAccounts = await _employeeBankAccountRepositorio.FindByIdAsync(id);
            
            if (employeeBankAccounts == null) throw new NotFoundCoreException("Registro no encontrado con el id");
            
            employeeBankAccounts.AuditUpdateDate = DateTime.Now;
            employeeBankAccounts.AuditUpdateUser = 1;

            _mapper.Map(saveDto, employeeBankAccounts);

            await _employeeBankAccountRepositorio.SaveAsync(employeeBankAccounts);

            return new OperationResult<EmployeeBankAccountDto>()
            {
                Data = _mapper.Map<EmployeeBankAccountDto>(employeeBankAccounts),
                Message = "Cuenta Bancaria de el empleado se ha actualizado",
                State = true,
            };

        }

        public async Task<IReadOnlyList<EmployeeBankAccountDto>> FindAllAsync()
        {
            var response = await _employeeBankAccountRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<EmployeeBankAccountDto>>(response);
        }

        public async Task<EmployeeBankAccountDto> FindByIdAsync(int id)
        {
            var response = await _employeeBankAccountRepositorio.FindByIdAsync(id);

            return _mapper.Map<EmployeeBankAccountDto>(response);
        }
    }
}
