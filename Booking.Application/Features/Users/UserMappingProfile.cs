using AutoMapper;
using Booking.Application.Features.Users.Queries.GetMyProfile;
using Booking.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<UserEntity, GetMyProfileDto>();
        }
    }
}
