using Application.Exceptions;
using Application.Mantenedores.Dtos.Clientes;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepositorio ClienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = ClienteRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<ClienteDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _clienteRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<ClienteDto>>(response.Data);

            return new PaginadoResponse<ClienteDto>(data, response.Meta);
        }

        public async Task<OperationResult<ClienteDto>> CreateAsync(ClienteSaveDto saveDto)
        {
            var cliente = _mapper.Map<Cliente>(saveDto);
            cliente.FechaCreacion = DateTime.Now;
            cliente.Estado = 1;

            await _clienteRepositorio.SaveAsync(cliente);

            return new OperationResult<ClienteDto>()
            {
                Data = _mapper.Map<ClienteDto>(cliente),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<ClienteDto>> DisabledAsync(int id)
        {
            var cliente = await _clienteRepositorio.FindByIdAsync(id) ?? throw new NotFoundCoreException("Registro no encontrado con ese Id");

            cliente.Estado = cliente.Estado == 1 ? 0 : 1;
            cliente.FechaModificacion = DateTime.Now;

            await _clienteRepositorio.SaveAsync(cliente);

            return new OperationResult<ClienteDto>()
            {
                Data = _mapper.Map<ClienteDto>(cliente),
                Message = cliente.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<ClienteDto>> EditAsync(int id, ClienteSaveDto saveDto)
        {
            var cliente = await _clienteRepositorio.FindByIdAsync(id);

            if (cliente == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            cliente.FechaModificacion = DateTime.Now;

            _mapper.Map(saveDto, cliente);

            await _clienteRepositorio.SaveAsync(cliente);

            return new OperationResult<ClienteDto>()
            {
                Data = _mapper.Map<ClienteDto>(cliente),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<ClienteDto>> FindAllAsync()
        {
            var response = await _clienteRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ClienteDto>>(response);
        }

        public async Task<ClienteDto> FindByIdAsync(int id)
        {
            var cliente = await _clienteRepositorio.FindByIdAsync(id);

            if (cliente == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<IReadOnlyList<ClienteSelectDto>> SelectActivo()
        {
            var response = await _clienteRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ClienteSelectDto>>(response);
        }
    }
}


