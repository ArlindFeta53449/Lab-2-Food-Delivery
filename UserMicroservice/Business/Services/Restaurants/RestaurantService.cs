using AutoMapper;
using Data.DTOs;
using Data.Entities;
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

        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public IList<RestaurantDto> GetAll()
        {
            var restaurants = _restaurantRepository.GetAll();
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

        public RestaurantDto CreateRestaurant(RestaurantCreateDto restaurant)
        {
            var mappedRestaurant = _mapper.Map<Restaurant>(restaurant);
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
