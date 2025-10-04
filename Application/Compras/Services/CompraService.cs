using Application.Compras.Dto;
using Application.Compras.Dto;
using Application.Compras.Dto;
using Application.Compras.Services.Interfaces;
using Application.Exceptions;
using Application.Compras.Dto;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.Compras.Service
{
    public class CompraService : ICompraServices
    {
        private readonly ICompraRepositorio _compraRepositorio;
        private readonly IMapper _mapper;

        public CompraService(ICompraRepositorio CompraRepositorio, IMapper mapper)
        {
            _compraRepositorio = CompraRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<CompraDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _compraRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<CompraDto>>(response.Data);

            return new PaginadoResponse<CompraDto>(data, response.Meta);
        }

        public async Task<OperationResult<CompraDto>> CreateAsync(CompraSaveDto saveDto)
        {
            var compra = _mapper.Map<Compra>(saveDto);

            await _compraRepositorio.SaveAsync(compra);

            return new OperationResult<CompraDto>()
            {
                Data = _mapper.Map<CompraDto>(compra),
                Message = "Creado con Exito",
            };
        }

        public async Task<OperationResult<CompraDto>> DisabledAsync(int id)
        {
            var compra = await _compraRepositorio.FindByIdAsync(id);
            if (compra == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<CompraDto>()
            {
                Data = _mapper.Map<CompraDto>(compra),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<CompraDto>> EditAsync(int id, CompraSaveDto saveDto)
        {
            var compra = await _compraRepositorio.FindByIdAsync(id);

            if (compra == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            _mapper.Map(saveDto, compra);

            await _compraRepositorio.SaveAsync(compra);

            return new OperationResult<CompraDto>()
            {
                Data = _mapper.Map<CompraDto>(compra),
                Message = "actualizado con exito",
            };

        }

        public async Task<IReadOnlyList<CompraDto>> FindAllAsync()
        {
            var response = await _compraRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<CompraDto>>(response);
        }

        public async Task<CompraDto> FindByIdAsync(int id)
        {
            var compra = await _compraRepositorio.FindByIdAsync(id);

            if (compra == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<CompraDto>(compra);
        }

        public async Task<IReadOnlyList<CompraSelectDto>> SelectActivo()
        {
            var response = await _compraRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<CompraSelectDto>>(response);
        }

        public async Task<OperationResult<List<CompraDto>>> FindByProveedorIdAsync(int proveedorId)
        {
            var compras = await _compraRepositorio.FindByProveedorIdAsync(proveedorId);

            if (compras == null || !compras.Any())
            {
                return new OperationResult<List<CompraDto>>
                {
                    Data = new List<CompraDto>(),
                    Message = $"No existen compras registradas para el proveedor con Id {proveedorId}"
                };
            }

            return new OperationResult<List<CompraDto>>
            {
                Data = _mapper.Map<List<CompraDto>>(compras),
                Message = "Compras encontradas"
            };
        }

    }
}


