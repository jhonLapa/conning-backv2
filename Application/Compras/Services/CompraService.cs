using Application.Compras.Dto;
using Application.Compras.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Compras.Service
{
    public class CompraService : ICompraService
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
            compra.FechaCreacion = DateTime.Now;
            compra.Estado = 1;

            await _compraRepositorio.SaveAsync(compra);

            return new OperationResult<CompraDto>()
            {
                Data = _mapper.Map<CompraDto>(compra),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<CompraDto>> DisabledAsync(int id)
        {
            var compra = await _compraRepositorio.FindByIdAsync(id) ?? throw new NotFoundCoreException("Registro no encontrado con ese Id");

            compra.Estado = compra.Estado == 1 ? 0 : 1;

            await _compraRepositorio.SaveAsync(compra);

            return new OperationResult<CompraDto>()
            {
                Data = _mapper.Map<CompraDto>(compra),
                Message = compra.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
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
                Success = true
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
    }
}


