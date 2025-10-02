using Application.Exceptions;
using Application.Mantenedores.Dtos.MovimientosEspeciales;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class MovimientoEspecialService : IMovimientoEspecialService
    {
        private readonly IMovimientoEspecialRepositorio _movimientoEspecialRepositorio;
        private readonly IMapper _mapper;

        public MovimientoEspecialService(IMovimientoEspecialRepositorio MovimientoEspecialRepositorio, IMapper mapper)
        {
            _movimientoEspecialRepositorio = MovimientoEspecialRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<MovimientoEspecialDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _movimientoEspecialRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<MovimientoEspecialDto>>(response.Data);

            return new PaginadoResponse<MovimientoEspecialDto>(data, response.Meta);
        }
        public async Task<OperationResult<MovimientoEspecialDto>> CreateAsync(MovimientoEspecialSaveDto saveDto)
        {
            var movimientoEspecial = _mapper.Map<MovimientoEspecial>(saveDto);
            movimientoEspecial.FechaCreacion = DateTime.Now;
            movimientoEspecial.Estado = 1;

            await _movimientoEspecialRepositorio.SaveAsync(movimientoEspecial);

            return new OperationResult<MovimientoEspecialDto>()
            {
                Data = _mapper.Map<MovimientoEspecialDto>(movimientoEspecial),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<MovimientoEspecialDto>> DisabledAsync(int id)
        {
            var movimientoEspecial = await _movimientoEspecialRepositorio.FindByIdAsync(id);

            if (movimientoEspecial == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            movimientoEspecial.Estado = movimientoEspecial.Estado == 1 ? 0 : 1;

            await _movimientoEspecialRepositorio.SaveAsync(movimientoEspecial);

            return new OperationResult<MovimientoEspecialDto>()
            {
                Data = _mapper.Map<MovimientoEspecialDto>(movimientoEspecial),
                Message = movimientoEspecial.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<MovimientoEspecialDto>> EditAsync(int id, MovimientoEspecialSaveDto saveDto)
        {
            var movimientoEspecial = await _movimientoEspecialRepositorio.FindByIdAsync(id);

            if (movimientoEspecial == null) throw new NotFoundCoreException("Registro no encontrado con ese id");


            _mapper.Map(saveDto, movimientoEspecial);

            await _movimientoEspecialRepositorio.SaveAsync(movimientoEspecial);

            return new OperationResult<MovimientoEspecialDto>()
            {
                Data = _mapper.Map<MovimientoEspecialDto>(movimientoEspecial),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<MovimientoEspecialDto>> FindAllAsync()
        {
            var response = await _movimientoEspecialRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<MovimientoEspecialDto>>(response);
        }

        public async Task<MovimientoEspecialDto> FindByIdAsync(int id)
        {
            var movimientoEspecial = await _movimientoEspecialRepositorio.FindByIdAsync(id);

            if (movimientoEspecial == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<MovimientoEspecialDto>(movimientoEspecial);
        }

        public async Task<IReadOnlyList<MovimientoEspecialSelectDto>> SelectActivo()
        {
            var response = await _movimientoEspecialRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<MovimientoEspecialSelectDto>>(response);
        }
    }
}
