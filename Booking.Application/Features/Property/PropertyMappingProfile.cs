using AutoMapper;
using Booking.Application.Features.Property.Queries.GetAll;
using Booking.Application.Features.Property.Queries.GetAllPropertyPhotos;
using Booking.Application.Features.Property.Queries.GetById;
using Booking.Domain.Estate;
using Booking.Domain.PropertyImage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            CreateMap<PropertyEntity, GetPropertyByIdDto>()
                .ForMember(dest => dest.OwnerFirstName, opt => opt.MapFrom(src => src.Owner.FirstName))
                .ForMember(dest => dest.OwnerLastName, opt => opt.MapFrom(src => src.Owner.LastName))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode));

            CreateMap<PropertyEntity, GetAllPropertiesDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            CreateMap<PropertyImageEntity, GetAllPropertyPhotosDto>();
        }
    }
}
