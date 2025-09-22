
using Application.Exceptions;
using Application.Mantenedores.Dtos.Categorys;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class CategoryService : ICategoryService

    {
        private readonly ICategoryRepositorio _categoryRepositorio;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepositorio CategoryRepositorio, IMapper mapper)
        {
            _categoryRepositorio = CategoryRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<CategoryDto>> CreateAsync(CategorySaveDto saveDto)
        {
            var Category = _mapper.Map<Category>(saveDto);
            Category.FechaCreacion = DateTime.Now;
            Category.IdUsuarioCreacion = 1;

            var response = await _categoryRepositorio.SaveAsync(Category);

            return new OperationResult<CategoryDto>()
            {
                Data = _mapper.Map<CategoryDto>(response),
                Message = "Banco Creado Con Existo",
                Success = true
            };

        }

        public async Task<OperationResult<CategoryDto>> DisabledAsync(int id)
        {
            var Category = await _categoryRepositorio.FindByIdAsync(id);

            if (Category == null) throw new NotFoundCoreException("No se encontro Registro con es Id");

            Category.Estado = Category.Estado == 1 ? 0 : 1;

            await _categoryRepositorio.SaveAsync(Category);

            return new OperationResult<CategoryDto>()
            {
                Data = _mapper.Map<CategoryDto>(Category),
                Message = Category.Estado == 1
                        ? "Activado con éxito"
                        : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<CategoryDto>> EditAsync(int id, CategorySaveDto saveDto)
        {
            var Category = await _categoryRepositorio.FindByIdAsync(id);
            if (Category == null) throw new NotFoundCoreException("No se encontro Registro con es Id");

            Category.FechaModificacion = DateTime.Now;

            _mapper.Map(saveDto, Category);

            await _categoryRepositorio.SaveAsync(Category);

            return new OperationResult<CategoryDto>()
            {
                Data = _mapper.Map<CategoryDto>(Category),
                Message = "Banco Actualizado con éxito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<CategoryDto>> FindAllAsync()
        {
            var response = await _categoryRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<CategoryDto>>(response);
        }

        public async Task<CategoryDto> FindByIdAsync(int id)
        {
            var response = await _categoryRepositorio.FindByIdAsync(id);
            if (response == null) throw new NotFoundCoreException("No Existe Registro Con ese Id");

            return _mapper.Map<CategoryDto>(response);
        }
    }
}
