using AutoMapper;
using Data.DTOs;
using Data.DTOs.Roles;
using Data.DTOs.Users;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services._00_MappingProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleCreateDto>();
            CreateMap<RoleCreateDto, Role>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserCreateDto, User>();   
            CreateMap<User, UserCreateDto>();

            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, Menu>();
            CreateMap<Menu, MenuCreateDto>();
            CreateMap<MenuCreateDto, Menu>();

            CreateMap<MenuItem, MenuItemDto>();
            CreateMap<MenuItemDto, MenuItem>();
            CreateMap<MenuItem, MenuItemCreateDto>();
            CreateMap<MenuItemCreateDto, MenuItem>();

            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantDto, Restaurant>();
            CreateMap<Restaurant, RestaurantCreateDto>();
            CreateMap<RestaurantCreateDto, Restaurant>();

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemCreateDto>();
            CreateMap<OrderItemCreateDto, OrderItem>();

            CreateMap<Offer, OfferDto>();
            CreateMap<OfferDto, Offer>();
            CreateMap<Offer, OfferCreateDto>();
            CreateMap<OfferCreateDto, Offer>();
        }
    }
}
