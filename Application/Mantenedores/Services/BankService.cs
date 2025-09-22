using Application.Exceptions;
using Application.Mantenedores.Dtos.Banks;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepositorio _bankRepositorio;
        private readonly IMapper _mapper;

        public BankService(IBankRepositorio BankRepositorio, IMapper mapper)
        {
            _bankRepositorio = BankRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<BankDto>> CreateAsync(BankSaveDto saveDto)
        {
            var bank = _mapper.Map<Bank>(saveDto);
            bank.FechaCreacion = DateTime.Now;
            bank.IdUsuarioCreacion = 1;

            await _bankRepositorio.SaveAsync(bank);

            return new OperationResult<BankDto>()
            {
                Data = _mapper.Map<BankDto>(bank),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<BankDto>> DisabledAsync(int id)
        {
            var bank = await _bankRepositorio.FindByIdAsync(id);

            if (bank == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            bank.Estado = bank.Estado == 1 ? 0 : 1;
            bank.FechaModificacion = DateTime.Now;

            return new OperationResult<BankDto>()
            {
                Data = _mapper.Map<BankDto>(bank),
                Message = bank.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<BankDto>> EditAsync(int id, BankSaveDto saveDto)
        {
            var bank = await _bankRepositorio.FindByIdAsync(id);

            if (bank == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            bank.FechaModificacion = DateTime.Now;
            bank.IdUsuarioModificacion = 1;

            _mapper.Map(saveDto, bank);

            await _bankRepositorio.SaveAsync(bank);

            return new OperationResult<BankDto>()
            {
                Data = _mapper.Map<BankDto>(bank),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<BankDto>> FindAllAsync()
        {
            var response = await _bankRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<BankDto>>(response);
        }

        public async Task<BankDto> FindByIdAsync(int id)
        {
            var bank = await _bankRepositorio.FindByIdAsync(id);

            if (bank == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<BankDto>(bank);
        }
    }
}
