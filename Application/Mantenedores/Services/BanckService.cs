using Application.Exceptions;
using Application.Mantenedores.Dtos.Bancks;
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
    public class BanckService : IBanckService

    {
        private readonly IBanckRepositorio _banckRepositorio;
        private readonly IMapper _mapper;

        public BanckService(IBanckRepositorio banckRepositorio, IMapper mapper)
        {
            _banckRepositorio = banckRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<BanckDto>> CreateAsync(BanckSaveDto saveDto)
        {
            var banck = _mapper.Map<Banck>(saveDto);
            banck.AuditCreateDate = DateTime.Now;
            banck.AuditCreateUser = 1;

            var response = await _banckRepositorio.SaveAsync(banck);

            return new OperationResult<BanckDto>()
            {
                Data = _mapper.Map<BanckDto>(response),
                Message = "Banco Creado Con Existo",
                State = true
            };
            
        }

        public async Task<OperationResult<BanckDto>> DisabledAsync(int id)
        {
            var banck = await _banckRepositorio.FindByIdAsync(id);

            if (banck == null) throw new NotFoundCoreException("No se encontro Registro con es Id");

            banck.State = !banck.State;

            await _banckRepositorio.SaveAsync(banck);

            return new OperationResult<BanckDto>()
            {
                Data = _mapper.Map<BanckDto>(banck),
                Message = banck.State
                            ? "Banco Activado con éxito"
                            : "Banco Desactivado con éxito",
                State=true
            };

        }

        public async Task<OperationResult<BanckDto>> EditAsync(int id, BanckSaveDto saveDto)
        {
            var banck = await _banckRepositorio.FindByIdAsync(id);
            if (banck == null) throw new NotFoundCoreException("No se encontro Registro con es Id");

            banck.AuditUpdateDate = DateTime.Now;

            _mapper.Map(saveDto,  banck);

            await _banckRepositorio.SaveAsync(banck);

            return new OperationResult<BanckDto>()
            {
                Data = _mapper.Map<BanckDto>(banck),
                Message = "Banco Actualizado con éxito",
                State = true
            };

        }

        public async Task<IReadOnlyList<BanckDto>> FindAllAsync()
        {
            var response = await _banckRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<BanckDto>>(response);
        }

        public async Task<BanckDto> FindByIdAsync(int id)
        {
            var response = await _banckRepositorio.FindByIdAsync(id);
            if (response == null) throw new NotFoundCoreException("No Existe Registro Con ese Id");

            return _mapper.Map<BanckDto>(response);
        }
    }
}
