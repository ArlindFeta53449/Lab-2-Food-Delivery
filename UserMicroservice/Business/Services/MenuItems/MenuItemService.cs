using AutoMapper;
using Business.Services.FileHandling;
using Data.DTOs;
using Data.DTOs.Menu;
using Data.DTOs.MenuItem;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Repositories.Repositories.MenusItems;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.MenuItems
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenusItemRepository _menusItemRepository;
        private readonly IMapper _mapper;
        private readonly IFileHandlingService _fileHandlingService;

        public MenuItemService(IMenusItemRepository menusItemRepository, IMapper mapper,IFileHandlingService fileHandlingService)
        {
            _menusItemRepository = menusItemRepository;
            _mapper = mapper;
            _fileHandlingService = fileHandlingService;
        }

        public ApiResponse<IList<MenuItemDto>> GetAll()
        {
            try
            {
                var menuItems = _menusItemRepository.GetAll();
                foreach (var menuItem in menuItems)
                {
                    menuItem.ImagePath = _fileHandlingService.ConvertFilePathForImage(menuItem.ImagePath);
                }
                return new ApiResponse<IList<MenuItemDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<MenuItemDto>>(menuItems)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<MenuItemDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<IList<MenuItemForDisplayDto>> GetMenuItemsForDisplay()
        {
            try
            {
                var menuItems = _menusItemRepository.GetMenusIncludeMenus();
                foreach (var menuItem in menuItems)
                {
                    menuItem.ImagePath = _fileHandlingService.ConvertFilePathForImage(menuItem.ImagePath);
                }
                return new ApiResponse<IList<MenuItemForDisplayDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<MenuItemForDisplayDto>>(menuItems)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<MenuItemForDisplayDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<MenuItemDto> GetMenuItem(int id)
        {
            try
            {
                var menuItem = _menusItemRepository.Get(id);
                if (menuItem == null)
                {
                    return new ApiResponse<MenuItemDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The menu item was not found" }
                    };
                }
                menuItem.ImagePath = _fileHandlingService.ConvertFilePathForImage(menuItem.ImagePath);
                return new ApiResponse<MenuItemDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<MenuItemDto>(menuItem)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<MenuItemDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<IList<MenuItemForDisplayDto>> GetMenuItemsByMenuId(int menuId)
        {
            try
            {
                var menuItems = _menusItemRepository.GetMenuItemsByMenuId(menuId);
                if (menuItems == null)
                {
                    return new ApiResponse<IList<MenuItemForDisplayDto>>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The Menu does not have any items" }
                    };
                }
                foreach (var menuItem in menuItems)
                {
                    menuItem.ImagePath = _fileHandlingService.ConvertFilePathForImage(menuItem.ImagePath);
                }
                return new ApiResponse<IList<MenuItemForDisplayDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<MenuItemForDisplayDto>>(menuItems)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<MenuItemForDisplayDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }
        public ApiResponse<string> DeleteMenuItem(int id)
        {
            try
            {
                var menuItem = _menusItemRepository.Get(id);
                if (menuItem == null)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The menu item does not exist" }
                    };
                }
                if (_menusItemRepository.Remove(menuItem))
                {
                    var fileDeleted = _fileHandlingService.DeleteFile(menuItem.ImagePath);
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "The menu item was deleted successfully. Also some offer might be affected."
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "The menu item was not deleted. Try again." }
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

        public ApiResponse<MenuItemDto> CreateMenuItem(MenuItemCreateDto menuItem, string path, IFormFile file)
        {
            try
            {
                var mappedMenuItem = _mapper.Map<MenuItem>(menuItem);

                if (file != null && file.Length > 0)
                {

                    var fileObject = _fileHandlingService.SaveFile(file, "MenuItems", path, new string[] { ".jpeg", ".png", ".jpg" });
                    if (fileObject == null)
                    {
                        return new ApiResponse<MenuItemDto>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() { "The file type is not correct" }
                        };
                    }
                    mappedMenuItem.Image = fileObject.fileName;
                    mappedMenuItem.ImagePath = fileObject.filePath;
                }
                if (_menusItemRepository.Add(mappedMenuItem))
                {

                    mappedMenuItem.ImagePath = _fileHandlingService.ConvertFilePathForImage(mappedMenuItem.ImagePath);
                    return new ApiResponse<MenuItemDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<MenuItemDto>(mappedMenuItem)
                    };
                }
                return new ApiResponse<MenuItemDto>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "There was a problem while saving the menu item.Please try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<MenuItemDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<MenuItemDto> EditMenuItem(MenuItemDto menuItem, string path, IFormFile file)
        {
            try
            {
                var menuItemInDb = _menusItemRepository.Get(menuItem.Id);
                if (menuItemInDb == null)
                {
                    return new ApiResponse<MenuItemDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The menu item does not exist" }
                    };
                }
                var image = menuItemInDb.Image;
                var imagePath = menuItemInDb.ImagePath;
                _mapper.Map(menuItem, menuItemInDb);
                if (file != null && file.Length > 0)
                {

                    var fileObject = _fileHandlingService.SaveFile(file, "MenuItems", path, new string[] { ".jpeg", ".png", ".jpg" });
                    if (fileObject == null)
                    {
                        return new ApiResponse<MenuItemDto>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() { "The file type is not correct" }
                        };
                    }
                    _fileHandlingService.DeleteFile(imagePath);
                    menuItemInDb.Image = fileObject.fileName;
                    menuItemInDb.ImagePath = fileObject.filePath;
                }
                if (menuItemInDb.ImagePath == null && menuItemInDb.Image == null)
                {
                    menuItemInDb.ImagePath = imagePath;
                    menuItemInDb.Image = image;
                }
                if (_menusItemRepository.Update(menuItemInDb))
                {

                    menuItemInDb.ImagePath = _fileHandlingService.ConvertFilePathForImage(menuItemInDb.ImagePath);
                    return new ApiResponse<MenuItemDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<MenuItemDto>(menuItemInDb)
                    };
                }
                return new ApiResponse<MenuItemDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Errors = new List<string>() { "The menu item was not updated. Please try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<MenuItemDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        /* Metoda e cila ben match se a eshte menuItem qe po kerkohet nga konsumatori*/
        public IList<MenuItemDto> SearchMenuItems(string menuitem)
        {
            var menuItems = _menusItemRepository.SearchMenuItems(menuitem);
            return _mapper.Map<IList<MenuItemDto>>(menuItems);
        }
    }
}
