using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Repository.Repositories.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.MenuItems
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenusItemRepository _menusItemRepository;
        private readonly IMapper _mapper;

        public MenuItemService(IMenusItemRepository menusItemRepository, IMapper mapper)
        {
            _menusItemRepository = menusItemRepository;
            _mapper = mapper;
        }

        public IList<MenuItemDto> GetAll()
        {
            var menusItem = _menusItemRepository.GetAll();
            return _mapper.Map<IList<MenuItemDto>>(menusItem);
        }

        public MenuItemDto GetMenuItem(int id)
        {
            var menuItem = _menusItemRepository.Get(id);
            return _mapper.Map<MenuItemDto>(menuItem);
        }

        public void DeleteMenuItem(int id)
        {
            var menuItem = _menusItemRepository.Get(id);
            _menusItemRepository.Remove(menuItem);
        }

        public MenuItemDto CreateMenuItem(MenuItemCreateDto menuItem)
        {
            var mappedMenuItem = _mapper.Map<MenuItem>(menuItem);
            _menusItemRepository.Add(mappedMenuItem);
            return _mapper.Map<MenuItemDto>(mappedMenuItem);
        }

        public MenuItemDto EditMenuItem(MenuItemDto menuItem)
        {
            var mappedMenuItem = _mapper.Map<MenuItem>(menuItem);
            _menusItemRepository.Update(mappedMenuItem);
            return _mapper.Map<MenuItemDto>(mappedMenuItem);
        }
    }
}
