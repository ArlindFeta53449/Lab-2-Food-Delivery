using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Menus
{
    public interface IMenuService
    {
        IList<MenuDto> GetAll();

        MenuDto GetMenu(int id);
        void DeleteMenu(int id);
        MenuDto CreateMenu(MenuCreateDto menu);
        MenuDto EditMenu(MenuDto menu);

    }
}
