using AutoMapper;
using HRPortal.Entities.Dto.InComing.DtoForUserPersonalInfo;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Mapper {
    public class UserPersonalMapper : Profile {
        public UserPersonalMapper() {
            CreateMap<UserPersonalCreationDto, UserPersonalInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => System.Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.ToUpper()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.ToUpper()))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<UserPersonalInfo, UserPersonalDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<UserForRegisterDto, UserPersonalInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => System.Guid.NewGuid()))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        }

        public void Configure(EntityTypeBuilder<UserPersonalInfo> builder) {
            builder.Ignore(c => c.Password);
            builder.Ignore(c => c.PhoneNumber);
            builder.Ignore(c => c.SocialSecurityNumber);
            builder.Ignore(c => c.DateOfBirth);
            builder.HasOne(x => x.UserCompanyInfo)
                .WithOne(x => x.UserPersonalInfo)
                .HasForeignKey<UserCompanyInfo>(x => x.UserPersonalId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
