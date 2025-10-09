using Application.Exceptions;
using Application.Mantenedores.Dtos.Menus;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepositorio _menuRepositorio;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepositorio MenuRepositorio, IMapper mapper)
        {
            _menuRepositorio = MenuRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<MenuDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _menuRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<MenuDto>>(response.Data);

            return new PaginadoResponse<MenuDto>(data, response.Meta);
        }

        public async Task<OperationResult<MenuDto>> CreateAsync(MenuSaveDto saveDto)
        {
            var menu = _mapper.Map<Menu>(saveDto);

            await _menuRepositorio.SaveAsync(menu);

            return new OperationResult<MenuDto>()
            {
                Data = _mapper.Map<MenuDto>(menu),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<MenuDto>> DisabledAsync(int id)
        {
            var menu = await _menuRepositorio.FindByIdAsync(id) ?? throw new NotFoundCoreException("Registro no encontrado con ese Id");

            menu.State = menu.State == 1 ? 0 : 1;

            await _menuRepositorio.SaveAsync(menu);

            return new OperationResult<MenuDto>()
            {
                Data = _mapper.Map<MenuDto>(menu),
                Message = menu.State == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<MenuDto>> EditAsync(int id, MenuSaveDto saveDto)
        {
            var menu = await _menuRepositorio.FindByIdAsync(id);

            if (menu == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            _mapper.Map(saveDto, menu);

            await _menuRepositorio.SaveAsync(menu);

            return new OperationResult<MenuDto>()
            {
                Data = _mapper.Map<MenuDto>(menu),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<MenuDto>> FindAllAsync()
        {
            var response = await _menuRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<MenuDto>>(response);
        }

        public async Task<MenuDto> FindByIdAsync(int id)
        {
            var menu = await _menuRepositorio.FindByIdAsync(id);

            if (menu == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<MenuDto>(menu);
        }

        public async Task<IReadOnlyList<MenuSelectDto>> SelectActivo()
        {
            var response = await _menuRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<MenuSelectDto>>(response);
        }
    }
}


