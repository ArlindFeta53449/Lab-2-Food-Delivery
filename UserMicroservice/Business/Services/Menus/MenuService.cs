using AutoMapper;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Repository.Repositories.MenusItems;

namespace Business.Services.Menus
{
    public class MenuService : IMenuService
    {
        private readonly IMenusRepository _menusRepository;
        private readonly IMapper _mapper;

        public MenuService(IMenusRepository menusRepository, IMapper mapper)
        {
            _menusRepository = menusRepository;
            _mapper = mapper;
        }

        public IList<MenuDto> GetAll()
        {
            var menus = _menusRepository.GetAll();
            return _mapper.Map<IList<MenuDto>>(menus); 
        }

        
        public MenuDto GetMenu(int id)
        {
            var menu = _menusRepository.Get(id);
            return _mapper.Map<MenuDto>(menu);
        }

        public void DeleteMenu(int id)
        {
            var menu = _menusRepository.Get(id);
            _menusRepository.Remove(menu);
        }

        public MenuDto CreateMenu(MenuCreateDto menu)
        {
            var mappedMenu = _mapper.Map<Menu>(menu);
            _menusRepository.Add(mappedMenu);
            return _mapper.Map<MenuDto>(mappedMenu);
        }

        public MenuDto EditMenu(MenuDto menu)
        {
            var mappedMenu = _mapper.Map<Menu>(menu);
            _menusRepository.Update(mappedMenu);
            return _mapper.Map<MenuDto>(mappedMenu);
        }

    }
}
