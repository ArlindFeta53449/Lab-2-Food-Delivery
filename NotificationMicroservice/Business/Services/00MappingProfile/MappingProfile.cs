using AutoMapper;
using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services._00MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserPublishedDto, User>();
            CreateMap<User, UserPublishedDto>();
            CreateMap<NotificationForDisplayDto, Notification>();
            CreateMap<Notification, NotificationForDisplayDto>();
        }
    }
}
