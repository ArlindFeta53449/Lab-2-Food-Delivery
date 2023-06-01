using AutoMapper;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Repositories.Repositories.Menus;
using Serilog;
using System.Net;
using Business.Services.FileHandling;
using Microsoft.AspNetCore.Http;
using Data.DTOs.Menu;
using Repositories.Repositories.MenusItems;

namespace Business.Services.Menus
{
    public class MenuService : IMenuService
    {
        private readonly IMenusRepository _menusRepository;
        private readonly IMapper _mapper;
        private readonly IFileHandlingService _fileHandlingService;
        private readonly IMenusItemRepository _menuItemRepository;

        public MenuService(
            IMenusRepository menusRepository, 
            IMapper mapper,
            IFileHandlingService fileHandlingService,
            IMenusItemRepository menuItemRepository)
        {
            _menusRepository = menusRepository;
            _mapper = mapper;
            _fileHandlingService = fileHandlingService;
            _menuItemRepository = menuItemRepository;
        }

        public ApiResponse<IList<MenuDto>> GetAll()
        {
            try
            {
                var menus = _menusRepository.GetAll();
                foreach (var menu in menus)
                {
                    menu.ImagePath = _fileHandlingService.ConvertFilePathForImage(menu.ImagePath);
                }
                return new ApiResponse<IList<MenuDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<MenuDto>>(menus)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<MenuDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }
        public ApiResponse<IList<MenuForDisplayDto>> GetMenusForDisplay()
        {
            try
            {
                var menus = _menusRepository.GetMenusIncludeRestaurants();
                foreach (var menu in menus)
                {
                    menu.ImagePath = _fileHandlingService.ConvertFilePathForImage(menu.ImagePath);
                }
                return new ApiResponse<IList<MenuForDisplayDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<MenuForDisplayDto>>(menus)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<MenuForDisplayDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }


        public ApiResponse<MenuDto> GetMenu(int id)
        {
            try
            {
                var menu = _menusRepository.Get(id);
                if (menu == null)
                {
                    return new ApiResponse<MenuDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The menu was not found" }
                    };
                }
                menu.ImagePath = _fileHandlingService.ConvertFilePathForImage(menu.ImagePath);
                return new ApiResponse<MenuDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<MenuDto>(menu)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<MenuDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<IList<MenuForDisplayDto>> GetMenusByRestaurantId(int restaurantId)
        {
            try
            {
                var menus = _menusRepository.GetMenusByRestaurantId(restaurantId);
                if (menus == null)
                {
                    return new ApiResponse<IList<MenuForDisplayDto>>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The restaurant does not have any menus" }
                    };
                }
                foreach (var menu in menus)
                {
                    menu.ImagePath = _fileHandlingService.ConvertFilePathForImage(menu.ImagePath);
                }
                return new ApiResponse<IList<MenuForDisplayDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<MenuForDisplayDto>>(menus)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<MenuForDisplayDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<string> DeleteMenu(int id)
        {
            try
            {
                var menu = _menusRepository.Get(id);
                if (menu == null)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The menu does not exist" }
                    };
                }
                _menuItemRepository.RemoveMenuItemsByMenuId(id);
                if (_menusRepository.Remove(menu))
                {
                    var fileDeleted = _fileHandlingService.DeleteFile(menu.ImagePath);
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "The menu was deleted successfully"
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "The menu was not deleted. Try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<MenuDto> CreateMenu(MenuCreateDto menu,string path,IFormFile file)
        {
            try
            {
                var mappedMenu = _mapper.Map<Menu>(menu);

                if (file != null && file.Length > 0)
                {

                    var fileObject = _fileHandlingService.SaveFile(file, "Menus", path, new string[] { ".jpeg", ".png", ".jpg" });
                    if (fileObject == null)
                    {
                        return new ApiResponse<MenuDto>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() { "The file type is not correct" }
                        };
                    }
                    mappedMenu.Image = fileObject.fileName;
                    mappedMenu.ImagePath = fileObject.filePath;
                }
                if (_menusRepository.Add(mappedMenu))
                {

                    mappedMenu.ImagePath = _fileHandlingService.ConvertFilePathForImage(mappedMenu.ImagePath);
                    return new ApiResponse<MenuDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<MenuDto>(mappedMenu)
                    };
                }
                return new ApiResponse<MenuDto>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "There was a problem while saving the menu.Please try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<MenuDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<MenuDto> EditMenu(MenuDto menu,string path, IFormFile file)
        {
            try
            {
                var menuInDb = _menusRepository.Get(menu.Id);
                if (menuInDb == null)
                {
                    return new ApiResponse<MenuDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The menu does not exist" }
                    };
                }
                var image = menuInDb.Image;
                var imagePath = menuInDb.ImagePath;
                _mapper.Map(menu, menuInDb);
                if (file != null && file.Length > 0)
                {

                    var fileObject = _fileHandlingService.SaveFile(file, "Menus", path, new string[] { ".jpeg", ".png", ".jpg" });
                    if (fileObject == null)
                    {
                        return new ApiResponse<MenuDto>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() { "The file type is not correct" }
                        };
                    }
                    _fileHandlingService.DeleteFile(imagePath);
                    menuInDb.Image = fileObject.fileName;
                    menuInDb.ImagePath = fileObject.filePath;
                }
                if (menuInDb.ImagePath == null && menuInDb.Image == null)
                {
                    menuInDb.ImagePath = imagePath;
                    menuInDb.Image = image;
                }
                if (_menusRepository.Update(menuInDb))
                {

                    menuInDb.ImagePath = _fileHandlingService.ConvertFilePathForImage(menuInDb.ImagePath);
                    return new ApiResponse<MenuDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<MenuDto>(menuInDb)
                    };
                }
                return new ApiResponse<MenuDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Errors = new List<string>() { "The menu was not updated. Please try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<MenuDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

    }
}
