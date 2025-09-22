using Application.Exceptions;
using Application.Mantenedores.Dtos.Pensiones;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Services
{
    public class PensionService : IPensionService
    {
        public readonly IPensionRepositorio _pensionRepositorio;
        public readonly IMapper _mapper;

        public PensionService(IPensionRepositorio pensionRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _pensionRepositorio = pensionRepositorio;
        }

        public async Task<OperationResult<PensionDto>> CreateAsync(PensionSaveDto saveDto)
        {
            var pension = _mapper.Map<Pension>(saveDto);
            
            await _pensionRepositorio.SaveAsync(pension);

            return new OperationResult<PensionDto>()
            {
                Data = _mapper.Map<PensionDto>(pension),
                Message = "Pension Creada Con Exio",
                Success = true
            };
        }

        public async Task<OperationResult<PensionDto>> DisabledAsync(int id)
        {
            throw new NotFoundCoreException("Metodo Aun no Implementado");
        }

        public async Task<OperationResult<PensionDto>> EditAsync(int id, PensionSaveDto saveDto)
        {
            var pension = await _pensionRepositorio.FindByIdAsync(id);  

            _mapper.Map(saveDto, pension);

            await _pensionRepositorio.SaveAsync(pension);

            return new OperationResult<PensionDto>()
            {
                Data = _mapper.Map<PensionDto>(pension),
                Message = "Pension Actualizada Con Exio",
                Success = true
            };

        }

        public async Task<IReadOnlyList<PensionDto>> FindAllAsync()
        {
            var response = await _pensionRepositorio.FindAllAsync();
            return _mapper.Map<IReadOnlyList<PensionDto>>(response);
        }

        public async Task<PensionDto> FindByIdAsync(int id)
        {
            var response = await _pensionRepositorio.FindByIdAsync(id);
            
            if (response == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            return _mapper.Map<PensionDto>(response);

        }
    }
}
