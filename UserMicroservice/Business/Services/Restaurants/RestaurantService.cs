using AutoMapper;
using Business.Services.FileHandling;
using Data.DTOs;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IList<RestaurantDto> GetAll()
        {
            var restaurants = _restaurantRepository.GetAll();
            foreach(var restaurant in restaurants)
            {
                restaurant.ImagePath = _fileHandlingService.ConvertFilePathForImage(restaurant.ImagePath);
            }
            return _mapper.Map<IList<RestaurantDto>>(restaurants);
        }

        public RestaurantDto GetRestaurant(int id)
        {
            var restaurant = _restaurantRepository.Get(id);
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public void DeleteRestaurant(int id)
        {
            var restaurant = _restaurantRepository.Get(id);
            _restaurantRepository.Remove(restaurant);
        }

        public RestaurantDto CreateRestaurant(RestaurantCreateDto restaurant,string path,IFormFile file)
        {
            var mappedRestaurant = _mapper.Map<Restaurant>(restaurant);

            if(file !=null && file.Length > 0)
            {
                
                var fileObject = _fileHandlingService.SaveFile(file, "Restaurants", path,new string[] { ".jpeg", ".png" });
                mappedRestaurant.Image = fileObject.fileName;
                mappedRestaurant.ImagePath = fileObject.filePath;
            }
            _restaurantRepository.Add(mappedRestaurant);
            return _mapper.Map<RestaurantDto>(mappedRestaurant);
        }

        public RestaurantDto EditRestaurant(RestaurantDto restaurant)
        {
            var mappedRestaurant = _mapper.Map<Restaurant>(restaurant);
            _restaurantRepository.Update(mappedRestaurant);
            return _mapper.Map<RestaurantDto>(mappedRestaurant);
        }

    }
}
