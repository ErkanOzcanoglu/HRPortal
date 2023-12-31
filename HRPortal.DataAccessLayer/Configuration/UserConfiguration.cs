﻿using AutoMapper;
using HRPortal.Entities.Dto.InComing;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.UpdateDto;
using HRPortal.Entities.Dto.InComing.UserForAuth;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class UserConfiguration : Profile {
        public UserConfiguration() {
            CreateMap<CreationDtoForEmployee, User>()
                .ForMember(u => u.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(u => u.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
                .ForMember(u => u.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(u => u.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(u => u.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(u => u.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(u => u.TC, opt => opt.MapFrom(src => src.TC))
                .ForMember(u => u.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(u => u.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(u => u.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(u => u.Mail, opt => opt.MapFrom(src => src.Mail))
                .ForMember(u => u.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<CreationDtoForUser, User>()
                .ForMember(u => u.IsAdmin, opt => opt.MapFrom(src => true))
                .ForMember(u => u.CompanyId, opt => opt.MapFrom(src => src.CompanyId));

            CreateMap<User, UserDto>()
                .ForMember(u => u.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(u => u.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
                .ForMember(u => u.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(u => u.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(u => u.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(u => u.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(u => u.TC, opt => opt.MapFrom(src => src.TC))
                .ForMember(u => u.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(u => u.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(u => u.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(u => u.Mail, opt => opt.MapFrom(src => src.Mail))
                .ForMember(u => u.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<UpdateDtoForUser, User>()
                .ForMember(u => u.IsAdmin, opt => opt.MapFrom(src => true))
                .ForMember(u => u.CompanyId, opt => opt.MapFrom(src => src.CompanyId));

            CreateMap<RegisterDto, User>()
                .ForMember(u => u.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(u => u.Status, opt => opt.MapFrom(src => 1))
                .ForMember(u => u.IsAdmin, opt => opt.MapFrom(src => true))
                .ForMember(u => u.CreatedTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(u => u.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(u => u.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(u => u.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(u => u.TC, opt => opt.MapFrom(src => src.TC))
                .ForMember(u => u.Title, opt => opt.MapFrom(src => src.Title));

            CreateMap<LoginDto, User>()
                .ForMember(u => u.Mail, opt => opt.MapFrom(src => src.Email));
        }

        public void Configure(EntityTypeBuilder<User> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Company).WithMany(b => b.Users).HasForeignKey(b => b.CompanyId);

            builder.Property(b => b.Name).IsRequired(false);
            builder.Property(b => b.Surname).IsRequired(false);
            builder.Property(b => b.Address).IsRequired(false);
            builder.Property(b => b.Phone).IsRequired(false).HasDefaultValue(null); // Make sure Phone is nullable
            builder.Property(b => b.TC).IsRequired(false).HasMaxLength(11); // TC must be 11 digits
            builder.Property(b => b.DateOfBirth).IsRequired(false);
            builder.Property(b => b.Title).IsRequired(false);
            builder.Property(b => b.Department).IsRequired(false);
            builder.Property(b => b.PasswordResetToken).IsRequired(false);

            builder.Property(b => b.Mail)
                   .IsRequired(false) // You can make it required if email is a required field
                   .HasMaxLength(255) // Assuming a reasonable email length
                   .IsUnicode(false) // Assuming it's not Unicode
                   .HasDefaultValue(null); // Make sure Email is nullable
        }

    }
}
