using Application.Exceptions;
using Application.Mantenedores.Dtos.Bancos;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class BancoService : IBancoService
    {
        private readonly IBancoRepositorio _bankRepositorio;
        private readonly IMapper _mapper;

        public BancoService(IBancoRepositorio BankRepositorio, IMapper mapper)
        {
            _bankRepositorio = BankRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<BancoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _bankRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<BancoDto>>(response.Data);

            return new PaginadoResponse<BancoDto>(data, response.Meta);
        }
        public async Task<OperationResult<BancoDto>> CreateAsync(BancoSaveDto saveDto)
        {
            var bank = _mapper.Map<Banco>(saveDto);
            bank.FechaCreacion = DateTime.Now;
            bank.Estado = 1;

            await _bankRepositorio.SaveAsync(bank);

            return new OperationResult<BancoDto>()
            {
                Data = _mapper.Map<BancoDto>(bank),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<BancoDto>> DisabledAsync(int id)
        {
            var bank = await _bankRepositorio.FindByIdAsync(id);

            if (bank == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            bank.Estado = bank.Estado == 1 ? 0 : 1;
            bank.FechaModificacion = DateTime.Now;

            await _bankRepositorio.SaveAsync(bank);

            return new OperationResult<BancoDto>()
            {
                Data = _mapper.Map<BancoDto>(bank),
                Message = bank.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<BancoDto>> EditAsync(int id, BancoSaveDto saveDto)
        {
            var bank = await _bankRepositorio.FindByIdAsync(id);

            if (bank == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            bank.FechaModificacion = DateTime.Now;

            _mapper.Map(saveDto, bank);

            await _bankRepositorio.SaveAsync(bank);

            return new OperationResult<BancoDto>()
            {
                Data = _mapper.Map<BancoDto>(bank),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<BancoDto>> FindAllAsync()
        {
            var response = await _bankRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<BancoDto>>(response);
        }

        public async Task<BancoDto> FindByIdAsync(int id)
        {
            var bank = await _bankRepositorio.FindByIdAsync(id);

            if (bank == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<BancoDto>(bank);
        }

        public async Task<IReadOnlyList<BancoSelectDto>> SelectActivo()
        {
            var response = await _bankRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<BancoSelectDto>>(response);
        }
    }
}
