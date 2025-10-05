
using Application.Exceptions;
using Application.Mantenedores.Dtos.Categorias;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class CategoriaService : ICategoriaService

    {
        private readonly ICategoriaRepositorio _categoryRepositorio;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepositorio CategoryRepositorio, IMapper mapper)
        {
            _categoryRepositorio = CategoryRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<CategoriaDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _categoryRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<CategoriaDto>>(response.Data);

            return new PaginadoResponse<CategoriaDto>(data, response.Meta);
        }

        public async Task<OperationResult<CategoriaDto>> CreateAsync(CategoriaSaveDto saveDto)
        {
            var Category = _mapper.Map<Categoria>(saveDto);
            Category.FechaCreacion = DateTime.Now;
            Category.Estado = 1;

            var response = await _categoryRepositorio.SaveAsync(Category);

            return new OperationResult<CategoriaDto>()
            {
                Data = _mapper.Map<CategoriaDto>(response),
                Message = "Banco Creado Con Existo",
                Success = true
            };

        }

        public async Task<OperationResult<CategoriaDto>> DisabledAsync(int id)
        {
            var Category = await _categoryRepositorio.FindByIdAsync(id);

            if (Category == null) throw new NotFoundCoreException("No se encontro Registro con es Id");

            Category.Estado = Category.Estado == 1 ? 0 : 1;

            await _categoryRepositorio.SaveAsync(Category);

            return new OperationResult<CategoriaDto>()
            {
                Data = _mapper.Map<CategoriaDto>(Category),
                Message = Category.Estado == 1
                        ? "Activado con éxito"
                        : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<CategoriaDto>> EditAsync(int id, CategoriaSaveDto saveDto)
        {
            var Category = await _categoryRepositorio.FindByIdAsync(id);
            if (Category == null) throw new NotFoundCoreException("No se encontro Registro con es Id");

            Category.FechaModificacion = DateTime.Now;

            _mapper.Map(saveDto, Category);

            await _categoryRepositorio.SaveAsync(Category);

            return new OperationResult<CategoriaDto>()
            {
                Data = _mapper.Map<CategoriaDto>(Category),
                Message = "Banco Actualizado con éxito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<CategoriaDto>> FindAllAsync()
        {
            var response = await _categoryRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<CategoriaDto>>(response);
        }

        public async Task<CategoriaDto> FindByIdAsync(int id)
        {
            var response = await _categoryRepositorio.FindByIdAsync(id);
            if (response == null) throw new NotFoundCoreException("No Existe Registro Con ese Id");

            return _mapper.Map<CategoriaDto>(response);
        }

        public async Task<IReadOnlyList<CategoriaSelectDto>> SelectActivo()
        {
            var response = await _categoryRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<CategoriaSelectDto>>(response);
        }
    }
}
