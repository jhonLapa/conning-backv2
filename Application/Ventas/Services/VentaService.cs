using Application.Exceptions;
using Application.Ventas.Dto;
using Application.Ventas.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Venta.Services
{
    public class VentaService : IVentaServices
    {
        private readonly IVentaRepositorio _ventaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepositorio VentaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = VentaRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<VentaDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _ventaRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<VentaDto>>(response.Data);

            return new PaginadoResponse<VentaDto>(data, response.Meta);
        }


        public async Task<OperationResult<VentaDto>> CreateAsync(VentaSaveDto saveDto)
        {
            var venta = _mapper.Map<Domain.Venta>(saveDto);


            await _ventaRepositorio.SaveAsync(venta);

            return new OperationResult<VentaDto>()
            {
                Data = _mapper.Map<VentaDto>(venta),
                Message = "Se ha Creado",
            };

        }

        public async Task<OperationResult<VentaDto>> DisabledAsync(int id)
        {
            var venta = await _ventaRepositorio.FindByIdAsync(id);
            if (venta == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<VentaDto>()
            {
                Data = _mapper.Map<VentaDto>(venta),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<VentaDto>> EditAsync(int id, VentaSaveDto saveDto)
        {
            var venta = await _ventaRepositorio.FindByIdAsync(id);

            if (venta == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, venta);

            await _ventaRepositorio.SaveAsync(venta);

            return new OperationResult<VentaDto>()
            {
                Data = _mapper.Map<VentaDto>(venta),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<VentaDto>> FindAllAsync()
        {
            var response = await _ventaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<VentaDto>>(response);
        }

        public async Task<VentaDto> FindByIdAsync(int id)
        {
            var response = await _ventaRepositorio.FindByIdAsync(id);

            return _mapper.Map<VentaDto>(response);
        }

        public async Task<IReadOnlyList<VentaSelectDto>> SelectActivo()
        {
            var response = await _ventaRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<VentaSelectDto>>(response);
        }

        public async Task<OperationResult<List<VentaDto>>> FindByClienteIdAsync(int clienteId)
        {
            var ventas = await _ventaRepositorio.FindByClienteIdAsync(clienteId);

            if (ventas == null || !ventas.Any())
            {
                return new OperationResult<List<VentaDto>>
                {
                    Data = new List<VentaDto>(),
                    Message = $"No existen ventas registradas para el cliente con Id {clienteId}"
                };
            }

            return new OperationResult<List<VentaDto>>
            {
                Data = _mapper.Map<List<VentaDto>>(ventas),
                Message = "Ventas encontradas"
            };
        }


    }
}


