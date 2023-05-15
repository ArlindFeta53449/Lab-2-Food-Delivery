using AutoMapper;
using Business.Services.FileHandling;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.Restaurants;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Business.Services.Restaurants
{
    public class RestaurantService : IRestaurantService
    {

        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly IFileHandlingService _fileHandlingService;

        public RestaurantService(IRestaurantRepository restaurantRepository,
                                 IMapper mapper,
                                 IFileHandlingService fileHandlingService
                                 )
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _fileHandlingService = fileHandlingService; 
        }

        public ApiResponse<IList<RestaurantDto>> GetAll()
        {
            try
            {
                var restaurants = _restaurantRepository.GetAll();
                foreach (var restaurant in restaurants)
                {
                    restaurant.ImagePath = _fileHandlingService.ConvertFilePathForImage(restaurant.ImagePath);
                }
                return new ApiResponse<IList<RestaurantDto>>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IList<RestaurantDto>>(restaurants)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<IList<RestaurantDto>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }

        public ApiResponse<RestaurantDto> GetRestaurant(int id)
        {
            try
            {
                var restaurant = _restaurantRepository.Get(id);
                if (restaurant == null)
                {
                    return new ApiResponse<RestaurantDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The restaurant was not found" }
                    };
                }
                restaurant.ImagePath = _fileHandlingService.ConvertFilePathForImage(restaurant.ImagePath);
                return new ApiResponse<RestaurantDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<RestaurantDto>(restaurant)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<RestaurantDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }

        }

        public ApiResponse<string> DeleteRestaurant(int id)
        {
            try
            {
                var restaurant = _restaurantRepository.Get(id);
                if (restaurant == null)
                {
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The restaurant does not exist" }
                    }; 
                }
                if (_restaurantRepository.Remove(restaurant))
                {
                    var fileDeleted = _fileHandlingService.DeleteFile(restaurant.ImagePath);
                    return new ApiResponse<string>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "The restaurant was deleted successfully"
                    };
                }
                return new ApiResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "The restaurant was not deleted. Try again." }
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

        public ApiResponse<RestaurantDto> CreateRestaurant(RestaurantCreateDto restaurant,string path,IFormFile file)
        {
            try
            {
                var mappedRestaurant = _mapper.Map<Restaurant>(restaurant);

                if (file != null && file.Length > 0)
                {

                    var fileObject = _fileHandlingService.SaveFile(file, "Restaurants", path, new string[] { ".jpeg", ".png",".jpg" });
                    if(fileObject == null)
                    {
                        return new ApiResponse<RestaurantDto>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() {"The file type is not correct"}
                        };
                    }
                    mappedRestaurant.Image = fileObject.fileName;
                    mappedRestaurant.ImagePath = fileObject.filePath;
                }
                if (_restaurantRepository.Add(mappedRestaurant)) {

                    mappedRestaurant.ImagePath = _fileHandlingService.ConvertFilePathForImage(mappedRestaurant.ImagePath);
                    return new ApiResponse<RestaurantDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<RestaurantDto>(mappedRestaurant)
                    };
                }
                return new ApiResponse<RestaurantDto>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Errors = new List<string>() { "There was a problem while saving the restaurant.Please try again."}
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<RestaurantDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

        public ApiResponse<RestaurantDto> EditRestaurant(RestaurantDto restaurant, string path, IFormFile file)
        {
            try
            {
                var restaurantInDb = _restaurantRepository.Get(restaurant.Id);
                if (restaurantInDb == null)
                {
                    return new ApiResponse<RestaurantDto>()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = new List<string>() { "The restaurant does not exist" }
                    };
                }
                var image = restaurantInDb.Image;
                var imagePath = restaurantInDb.ImagePath;
                _mapper.Map(restaurant, restaurantInDb);
                if (file != null && file.Length > 0)
                {

                    var fileObject = _fileHandlingService.SaveFile(file, "Restaurants", path, new string[] { ".jpeg", ".png", ".jpg" });
                    if (fileObject == null)
                    {
                        return new ApiResponse<RestaurantDto>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Errors = new List<string>() { "The file type is not correct" }
                        };
                    }
                    _fileHandlingService.DeleteFile(restaurantInDb.ImagePath);
                    restaurantInDb.Image = fileObject.fileName;
                    restaurantInDb.ImagePath = fileObject.filePath;
                }
                if (_restaurantRepository.Update(restaurantInDb))
                {
                    if (restaurantInDb.ImagePath==null && restaurantInDb.Image == null)
                    {
                        restaurantInDb.ImagePath = imagePath;
                        restaurantInDb.Image = image;
                    }
                    restaurantInDb.ImagePath = _fileHandlingService.ConvertFilePathForImage(restaurantInDb.ImagePath);
                    return new ApiResponse<RestaurantDto>()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _mapper.Map<RestaurantDto>(restaurantInDb)
                    };
                }
                return new ApiResponse<RestaurantDto>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Errors = new List<string>() { "The restaurant was not updated. Please try again." }
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "An error occurred: {ErrorMessage}", ex.Message);
                return new ApiResponse<RestaurantDto>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Errors = new List<string> { "An error occurred while processing your request. Please try again later." }
                };
            }
        }

    }
}
