using Application.Exceptions;
using Application.Mantenedores.Dtos.Proveedores;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepositorio _proveedorRepositorio;
        private readonly IMapper _mapper;

        public ProveedorService(IProveedorRepositorio ProveedorRepositorio, IMapper mapper)
        {
            _proveedorRepositorio = ProveedorRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<ProveedorDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _proveedorRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<ProveedorDto>>(response.Data);

            return new PaginadoResponse<ProveedorDto>(data, response.Meta);
        }

        public async Task<OperationResult<ProveedorDto>> CreateAsync(ProveedorSaveDto saveDto)
        {
            var proveedor = _mapper.Map<Proveedor>(saveDto);
            proveedor.FechaCreacion = DateTime.Now;
            proveedor.Estado = 1;

            await _proveedorRepositorio.SaveAsync(proveedor);

            return new OperationResult<ProveedorDto>()
            {
                Data = _mapper.Map<ProveedorDto>(proveedor),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<ProveedorDto>> DisabledAsync(int id)
        {
            var proveedor = await _proveedorRepositorio.FindByIdAsync(id) ?? throw new NotFoundCoreException("Registro no encontrado con ese Id");

            proveedor.Estado = proveedor.Estado == 1 ? 0 : 1;
            proveedor.FechaModificacion = DateTime.Now;

            await _proveedorRepositorio.SaveAsync(proveedor);

            return new OperationResult<ProveedorDto>()
            {
                Data = _mapper.Map<ProveedorDto>(proveedor),
                Message = proveedor.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<ProveedorDto>> EditAsync(int id, ProveedorSaveDto saveDto)
        {
            var proveedor = await _proveedorRepositorio.FindByIdAsync(id);

            if (proveedor == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            proveedor.FechaModificacion = DateTime.Now;

            _mapper.Map(saveDto, proveedor);

            await _proveedorRepositorio.SaveAsync(proveedor);

            return new OperationResult<ProveedorDto>()
            {
                Data = _mapper.Map<ProveedorDto>(proveedor),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<ProveedorDto>> FindAllAsync()
        {
            var response = await _proveedorRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ProveedorDto>>(response);
        }

        public async Task<ProveedorDto> FindByIdAsync(int id)
        {
            var proveedor = await _proveedorRepositorio.FindByIdAsync(id);

            if (proveedor == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<ProveedorDto>(proveedor);
        }

        public async Task<IReadOnlyList<ProveedorSelectDto>> SelectActivo()
        {
            var response = await _proveedorRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<ProveedorSelectDto>>(response);
        }
    }
}


