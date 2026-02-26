using Application.Departamentos.Dto;
using Application.Departamentos.Services.Interface;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Departamentos.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepositorio _repo;
        private readonly IMapper _mapper;

        public DepartamentoService(
            IDepartamentoRepositorio repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<DepartamentoDto>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();
            return _mapper.Map<List<DepartamentoDto>>(data);
        }

        public async Task<DepartamentoDto?> GetByIdAsync(int id)
        {

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            return _mapper.Map<DepartamentoDto>(entity);
        }

        public async Task<DepartamentoDto> SaveAsync(SaveDepartamento dto)
        {
            Departamento entity;

            if  (dto.Id == 0)
            {
                // Crear
                entity = _mapper.Map<Departamento>(dto);
                await _repo.AddAsync(entity);
            }
            else
            {
                // Update
                entity = await _repo.GetByIdAsync(dto.Id ?? 0)
                    ?? throw new Exception("Departamento no encontrado");

                entity.Nombre = dto.Nombre;
                await _repo.UpdateAsync(entity);
            }

            return _mapper.Map<DepartamentoDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            await _repo.DeleteAsync(entity);
            return true;
        }
    }
}