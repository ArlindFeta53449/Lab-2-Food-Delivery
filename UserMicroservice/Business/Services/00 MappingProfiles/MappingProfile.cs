using AutoMapper;
using Data.DTOs;
using Data.DTOs.Cart;
using Data.DTOs.MenuItem;
using Data.DTOs.Offer;
using Data.DTOs.RabbitMQ;
using Data.DTOs.Roles;
using Data.DTOs.Users;
using Data.Entities;
using Data.DTOs.ParentDtos;
using Data.DTOs.ChildDtos;

using Data.DTOs.DirectorDtos;
using Data.DTOs.MovieDtos;
using Data.DTOs.Payment;
using Data.DTOs.EmployeeDtos;
using Data.DTOs.ContractDtos;
using Data.DTOs.InterviewDtos;
using Data.DTOs.InterviewNotesDtos;

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



            CreateMap<User, UserLoginResponseDto>();
            CreateMap<UserLoginResponseDto, User>();

            CreateMap<User, UserEditDto>();
            CreateMap<UserEditDto, User>();

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

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderCreateDto>();
            CreateMap<OrderCreateDto, Order>();
            

            CreateMap<Offer, OfferDto>();
            CreateMap<OfferDto, Offer>();
            CreateMap<Offer, OfferCreateDto>();
            CreateMap<OfferCreateDto, Offer>();

            CreateMap<MenuItemOffer, MenuItemOfferDto>();
            CreateMap<MenuItemOfferDto, MenuItemOffer>();

            CreateMap<MenuItemOfferCreateDto, MenuItemOffer>();
            CreateMap<MenuItemOffer, MenuItemOfferCreateDto>();
            CreateMap<MenuItemOfferDto, MenuItemOfferCreateDto>();
            CreateMap<MenuItemOfferCreateDto, MenuItemOfferDto>();

            CreateMap<Cart, CartDto>();
            CreateMap<CartDto, Cart>();

            CreateMap<MenuItemForCartDto, MenuItem>();
            CreateMap<MenuItem, MenuItemForCartDto>();
            CreateMap<OfferForCartDto, Offer>();
            CreateMap<Offer, OfferForCartDto>();

            CreateMap<Cart, CartCreateDto>();
            CreateMap<CartCreateDto, Cart>();
            CreateMap<CartDto, CartCreateDto>();
            CreateMap<CartCreateDto,CartDto >();
            CreateMap<OfferForCartCreateDto, Offer>();
            CreateMap<Offer, OfferForCartCreateDto>();
            CreateMap<MenuItemForCartCreateDto, CartMenuItem>();
            CreateMap<CartMenuItem, MenuItemForCartCreateDto>();


            CreateMap<MenuItemForCartCreateDto, MenuItem>();
            CreateMap<MenuItem, MenuItemForCartCreateDto>();

            CreateMap<OfferForCartCreateDto, CartOffer>();
            CreateMap<CartOffer, OfferForCartCreateDto>();

            CreateMap<MenuItemForOrderCreateDto, OrderMenuItem>();
            CreateMap<OrderMenuItem, MenuItemForOrderCreateDto>();

            CreateMap<OrderOffer, OfferForOrderDto>();
            CreateMap<OfferForOrderDto, OrderOffer>();

            CreateMap<UserPublishedDto,User> ();
            CreateMap<User, UserPublishedDto>();

            CreateMap<Order, OrderPublishedDto>();
            CreateMap<OrderPublishedDto, Order>();
            CreateMap<Order, OrderCreatedDto>();
            CreateMap<OrderCreatedDto, Order>();


            CreateMap<Parent, ParentDto>();
            CreateMap<ParentDto, Parent>();
            CreateMap<Parent, ParentCreateDto>();
            CreateMap<ParentCreateDto, Parent>();

            CreateMap<Child, ChildDto>();
            CreateMap<ChildDto, Child>();
            CreateMap<Child, ChildCreateDto>();
            CreateMap<ChildCreateDto, Child>();

            //CreateMap<Director, DirectorDto>();
            //CreateMap<DirectorDto, Director>();
            //CreateMap<Director, DirectorCreateDto>();
            //CreateMap<DirectorCreateDto, Director>();

            //CreateMap<Movie, MovieDto>();
            //CreateMap<MovieDto, Movie>();
            //CreateMap<Movie, MovieCreateDto>();
            //CreateMap<MovieCreateDto, Movie>();

            //CreateMap<Employee, EmployeeDto>();
            //CreateMap<EmployeeDto, Employee>();
            //CreateMap<Employee, EmployeeCreateDto>();
            //CreateMap<EmployeeCreateDto, Employee>();

            //CreateMap<Contract, ContractDto>();
            //CreateMap<ContractDto, Contract>();
            //CreateMap<Contract, ContractCreateDto>();
            //CreateMap<ContractCreateDto, Contract>();

            //CreateMap<Interview, InterviewDto>();
            //CreateMap<InterviewDto, Interview>();
            //CreateMap<Interview, InterviewCreateDto>();
            //CreateMap<InterviewCreateDto, Interview>();

            //CreateMap<InterviewNotes, InterviewNotesDto>();
            //CreateMap<InterviewNotesDto, InterviewNotes>();
            //CreateMap<InterviewNotes, InterviewNotesCreateDto>();
            //CreateMap<InterviewNotesCreateDto, InterviewNotes>();
        }
    }
}
