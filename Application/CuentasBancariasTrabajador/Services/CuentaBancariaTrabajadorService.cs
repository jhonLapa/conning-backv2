using Application.CuentasBancariasTrabajador.Dtos;
using Application.CuentasBancariasTrabajador.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.CuentasBancariasTrabajador.Services
{
    public class CuentaBancariaTrabajadorService : ICuentaBancariaTrabajadorServices
    {
        private readonly ICuentaBancariaTrabajadorRepositorio _employeeBankAccountRepositorio;
        private readonly IMapper _mapper;

        public CuentaBancariaTrabajadorService(ICuentaBancariaTrabajadorRepositorio employeeBankAccountRepositorio, IMapper mapper)
        {
            _employeeBankAccountRepositorio = employeeBankAccountRepositorio;
            _mapper = mapper;   
        }

        public async Task<OperationResult<CuentaBancariaTrabajadorDto>> CreateAsync(CuentaBancariaTrabajadorSaveDto saveDto)
        {
            var employeeBankAccount = _mapper.Map<CuentaBancariaTrabajador>(saveDto);


            await _employeeBankAccountRepositorio.SaveAsync(employeeBankAccount);

            return new OperationResult<CuentaBancariaTrabajadorDto>()
            {
                Data = _mapper.Map<CuentaBancariaTrabajadorDto>(employeeBankAccount),
                Message = "Cuenta del Empleado se ha Creado",
                Success = true,
            };

        }

        public async Task<OperationResult<CuentaBancariaTrabajadorDto>> DisabledAsync(int id)
        {
            var employeeBankAccounts = await _employeeBankAccountRepositorio.FindByIdAsync(id);
            if (employeeBankAccounts == null) throw new NotFoundCoreException("Registro no encontrado con el id");
            
            return new OperationResult<CuentaBancariaTrabajadorDto>()
            {
                Data = _mapper.Map<CuentaBancariaTrabajadorDto>(employeeBankAccounts),
                Message = "Cuenta de el empleado se ha Desactivado",
                Success = true,
            };
        }

        public async Task<OperationResult<CuentaBancariaTrabajadorDto>> EditAsync(int id, CuentaBancariaTrabajadorSaveDto saveDto)
        {
            var employeeBankAccounts = await _employeeBankAccountRepositorio.FindByIdAsync(id);
            
            if (employeeBankAccounts == null) throw new NotFoundCoreException("Registro no encontrado con el id");
            

            _mapper.Map(saveDto, employeeBankAccounts);

            await _employeeBankAccountRepositorio.SaveAsync(employeeBankAccounts);

            return new OperationResult<CuentaBancariaTrabajadorDto>()
            {
                Data = _mapper.Map<CuentaBancariaTrabajadorDto>(employeeBankAccounts),
                Message = "Cuenta Bancaria de el empleado se ha actualizado",
                Success = true,
            };

        }

        public async Task<IReadOnlyList<CuentaBancariaTrabajadorDto>> FindAllAsync()
        {
            var response = await _employeeBankAccountRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<CuentaBancariaTrabajadorDto>>(response);
        }

        public async Task<CuentaBancariaTrabajadorDto> FindByIdAsync(int id)
        {
            var response = await _employeeBankAccountRepositorio.FindByIdAsync(id);

            return _mapper.Map<CuentaBancariaTrabajadorDto>(response);
        }
    }
}
