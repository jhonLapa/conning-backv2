using Application.Empresas.Dto;
using Application.Empresas.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Empresas.Services
{
    public class EmpresaService : IEmpresaServices
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IMapper _mapper;

        public EmpresaService(IEmpresaRepositorio empresaRepositorio, IMapper mapper)
        {
            _empresaRepositorio = empresaRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<EmpresaDto>> CreateAsync(EmpresaSaveDto saveDto)
        {
            var empresa = _mapper.Map<Empresa>(saveDto);


            await _empresaRepositorio.SaveAsync(empresa);

            return new OperationResult<EmpresaDto>()
            {
                Data = _mapper.Map<EmpresaDto>(empresa),
                Message = "Se ha Creado",
                Success = true,
            };

        }

        public async Task<OperationResult<EmpresaDto>> DisabledAsync(int id)
        {
            var empresas = await _empresaRepositorio.FindByIdAsync(id);
            if (empresas == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<EmpresaDto>()
            {
                Data = _mapper.Map<EmpresaDto>(empresas),
                Message = "Se ha Desactivado",
                Success = true,
            };
        }

        public async Task<OperationResult<EmpresaDto>> EditAsync(int id, EmpresaSaveDto saveDto)
        {
            var empresas = await _empresaRepositorio.FindByIdAsync(id);

            if (empresas == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, empresas);

            await _empresaRepositorio.SaveAsync(empresas);

            return new OperationResult<EmpresaDto>()
            {
                Data = _mapper.Map<EmpresaDto>(empresas),
                Message = "Se ha actualizado",
                Success = true,
            };

        }

        public async Task<IReadOnlyList<EmpresaDto>> FindAllAsync()
        {
            var response = await _empresaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<EmpresaDto>>(response);
        }

        public async Task<EmpresaDto> FindByIdAsync(int id)
        {
            var response = await _empresaRepositorio.FindByIdAsync(id);

            return _mapper.Map<EmpresaDto>(response);
        }
    }
}
