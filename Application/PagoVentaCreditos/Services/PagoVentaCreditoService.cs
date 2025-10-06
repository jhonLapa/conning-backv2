using Application.Exceptions;
using Application.PagoVentaCreditos.Dto;
using Application.PagoVentaCreditos.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.PagoVentaCreditos.Services
{
    public class PagoVentaCreditoService : IPagoVentaCreditoServices
    {
        private readonly IPagoVentaCreditoRepositorio _pagoVentaCreditoRepositorio;
        private readonly IMapper _mapper;

        public PagoVentaCreditoService(IPagoVentaCreditoRepositorio PagoVentaCreditoRepositorio, IMapper mapper)
        {
            _pagoVentaCreditoRepositorio = PagoVentaCreditoRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<PagoVentaCreditoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _pagoVentaCreditoRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<PagoVentaCreditoDto>>(response.Data);

            return new PaginadoResponse<PagoVentaCreditoDto>(data, response.Meta);
        }


        public async Task<OperationResult<PagoVentaCreditoDto>> CreateAsync(PagoVentaCreditoSaveDto saveDto)
        {
            var pagoVentaCredito = _mapper.Map<PagoVentaCredito>(saveDto);


            await _pagoVentaCreditoRepositorio.SaveAsync(pagoVentaCredito);

            return new OperationResult<PagoVentaCreditoDto>()
            {
                Data = _mapper.Map<PagoVentaCreditoDto>(pagoVentaCredito),
                Message = "Se ha Creado",
            };

        }

        public async Task<OperationResult<PagoVentaCreditoDto>> DisabledAsync(int id)
        {
            var pagoVentaCredito = await _pagoVentaCreditoRepositorio.FindByIdAsync(id);
            if (pagoVentaCredito == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<PagoVentaCreditoDto>()
            {
                Data = _mapper.Map<PagoVentaCreditoDto>(pagoVentaCredito),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<PagoVentaCreditoDto>> EditAsync(int id, PagoVentaCreditoSaveDto saveDto)
        {
            var pagoVentaCredito = await _pagoVentaCreditoRepositorio.FindByIdAsync(id);

            if (pagoVentaCredito == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, pagoVentaCredito);

            await _pagoVentaCreditoRepositorio.SaveAsync(pagoVentaCredito);

            return new OperationResult<PagoVentaCreditoDto>()
            {
                Data = _mapper.Map<PagoVentaCreditoDto>(pagoVentaCredito),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<PagoVentaCreditoDto>> FindAllAsync()
        {
            var response = await _pagoVentaCreditoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<PagoVentaCreditoDto>>(response);
        }

        public async Task<PagoVentaCreditoDto> FindByIdAsync(int id)
        {
            var response = await _pagoVentaCreditoRepositorio.FindByIdAsync(id);

            return _mapper.Map<PagoVentaCreditoDto>(response);
        }



        public async Task<OperationResult<List<PagoVentaCreditoDto>>> ObtenerPorVentaAsync(int id)
        {
            var response = await _pagoVentaCreditoRepositorio.ObtenerPorVentaAsync(id);

            if (response == null || !response.Any())
            {
                return new OperationResult<List<PagoVentaCreditoDto>>
                {
                    Data = new List<PagoVentaCreditoDto>(),
                    Message = $"No existen  datos registrados para la venta con Id {id}"
                };
            }

            return new OperationResult<List<PagoVentaCreditoDto>>
            {
                Data = _mapper.Map<List<PagoVentaCreditoDto>>(response),
                Message = "Datos no encontrados"
            };
        }

    }
}