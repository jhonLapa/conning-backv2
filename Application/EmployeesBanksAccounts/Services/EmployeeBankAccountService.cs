using Application.Empleados.Dtos;
using Application.Empleados.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

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
            var employeeBankAccount = _mapper.Map<EmployeeBankAccount>(saveDto);


            await _employeeBankAccountRepositorio.SaveAsync(employeeBankAccount);

            return new OperationResult<EmployeeBankAccountDto>()
            {
                Data = _mapper.Map<EmployeeBankAccountDto>(employeeBankAccount),
                Message = "Cuenta del Empleado se ha Creado",
                Success = true,
            };

        }

        public async Task<OperationResult<EmployeeBankAccountDto>> DisabledAsync(int id)
        {
            var employeeBankAccounts = await _employeeBankAccountRepositorio.FindByIdAsync(id);
            if (employeeBankAccounts == null) throw new NotFoundCoreException("Registro no encontrado con el id");
            
            return new OperationResult<EmployeeBankAccountDto>()
            {
                Data = _mapper.Map<EmployeeBankAccountDto>(employeeBankAccounts),
                Message = "Cuenta de el empleado se ha Desactivado",
                Success = true,
            };
        }

        public async Task<OperationResult<EmployeeBankAccountDto>> EditAsync(int id, EmployeeBankAccountSaveDto saveDto)
        {
            var employeeBankAccounts = await _employeeBankAccountRepositorio.FindByIdAsync(id);
            
            if (employeeBankAccounts == null) throw new NotFoundCoreException("Registro no encontrado con el id");
            

            _mapper.Map(saveDto, employeeBankAccounts);

            await _employeeBankAccountRepositorio.SaveAsync(employeeBankAccounts);

            return new OperationResult<EmployeeBankAccountDto>()
            {
                Data = _mapper.Map<EmployeeBankAccountDto>(employeeBankAccounts),
                Message = "Cuenta Bancaria de el empleado se ha actualizado",
                Success = true,
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
