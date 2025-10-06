using Application.Exceptions;
using Application.PagoCompraCreditos.Dto;
using Application.PagoCompraCreditos.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.PagoCompraCreditos.Services
{
    public class PagoCompraCreditoService : IPagoCompraCreditoServices
    {
        private readonly IPagoCompraCreditoRepositorio _pagoCompraCreditoRepositorio;
        private readonly IMapper _mapper;

        public PagoCompraCreditoService(IPagoCompraCreditoRepositorio PagoCompraCreditoRepositorio, IMapper mapper)
        {
            _pagoCompraCreditoRepositorio = PagoCompraCreditoRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<PagoCompraCreditoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _pagoCompraCreditoRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<PagoCompraCreditoDto>>(response.Data);

            return new PaginadoResponse<PagoCompraCreditoDto>(data, response.Meta);
        }


        public async Task<OperationResult<PagoCompraCreditoDto>> CreateAsync(PagoCompraCreditoSaveDto saveDto)
        {
            var pagoCompraCredito = _mapper.Map<PagoCompraCredito>(saveDto);


            await _pagoCompraCreditoRepositorio.SaveAsync(pagoCompraCredito);

            return new OperationResult<PagoCompraCreditoDto>()
            {
                Data = _mapper.Map<PagoCompraCreditoDto>(pagoCompraCredito),
                Message = "Se ha Creado",
            };

        }

        public async Task<OperationResult<PagoCompraCreditoDto>> DisabledAsync(int id)
        {
            var pagoCompraCredito = await _pagoCompraCreditoRepositorio.FindByIdAsync(id);
            if (pagoCompraCredito == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<PagoCompraCreditoDto>()
            {
                Data = _mapper.Map<PagoCompraCreditoDto>(pagoCompraCredito),
                Message = "Se ha Desactivado",
            };
        }

        public async Task<OperationResult<PagoCompraCreditoDto>> EditAsync(int id, PagoCompraCreditoSaveDto saveDto)
        {
            var pagoCompraCredito = await _pagoCompraCreditoRepositorio.FindByIdAsync(id);

            if (pagoCompraCredito == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, pagoCompraCredito);

            await _pagoCompraCreditoRepositorio.SaveAsync(pagoCompraCredito);

            return new OperationResult<PagoCompraCreditoDto>()
            {
                Data = _mapper.Map<PagoCompraCreditoDto>(pagoCompraCredito),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<PagoCompraCreditoDto>> FindAllAsync()
        {
            var response = await _pagoCompraCreditoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<PagoCompraCreditoDto>>(response);
        }

        public async Task<PagoCompraCreditoDto> FindByIdAsync(int id)
        {
            var response = await _pagoCompraCreditoRepositorio.FindByIdAsync(id);

            return _mapper.Map<PagoCompraCreditoDto>(response);
        }



        public async Task<OperationResult<List<PagoCompraCreditoDto>>> ObtenerPorCompraAsync(int id)
        {
            var response = await _pagoCompraCreditoRepositorio.ObtenerPorCompraAsync(id);

            if (response == null || !response.Any())
            {
                return new OperationResult<List<PagoCompraCreditoDto>>
                {
                    Data = new List<PagoCompraCreditoDto>(),
                    Message = $"No existen  datos registrados para la venta con Id {id}"
                };
            }

            return new OperationResult<List<PagoCompraCreditoDto>>
            {
                Data = _mapper.Map<List<PagoCompraCreditoDto>>(response),
                Message = "Datos no encontrados"
            };
        }

    }
}