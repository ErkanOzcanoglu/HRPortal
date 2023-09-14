using AutoMapper;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.CustomUpdatesDto;
using HRPortal.Entities.Dto.InComing.UpdateDto;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration
{
    public class CompanyConfiguration : Profile {
        public CompanyConfiguration() {
            CreateMap<CreationDtoForCompany, Company>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsPremium, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.CompanyMail, opt => opt.MapFrom(src => src.CompanyMail))
                .ForMember(dest => dest.CompanyPhone, opt => opt.MapFrom(src => src.CompanyPhone))
                .ForMember(dest => dest.CompanyAddress, opt => opt.MapFrom(src => src.CompanyAddress));

            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.CompanyAddress, opt => opt.MapFrom(src => src.CompanyAddress))
                .ForMember(dest => dest.CompanyMail, opt => opt.MapFrom(src => src.CompanyMail))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo))
                .ForMember(dest => dest.CompanyPhone, opt => opt.MapFrom(src => src.CompanyPhone))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website));

            CreateMap<UpdateDtoForCompany, Company>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.CompanyMail, opt => opt.MapFrom(src => src.CompanyMail))
                .ForMember(dest => dest.CompanyPhone, opt => opt.MapFrom(src => src.CompanyPhone));

            CreateMap<Company, UpdateCompanyForPurchase>()
                .ForMember(dest => dest.IsPremium, opt => opt.MapFrom(src => true)).ReverseMap();
        }

        public void Configure(EntityTypeBuilder<Company> builder) {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Logo).IsRequired(false);
            builder.Property(b => b.CompanyAddress).IsRequired(false);
            builder.Property(b => b.Website).IsRequired(false);

            builder.Property(b => b.CompanyPhone).IsRequired(false).HasDefaultValue(false); // Make sure Phone is nullable
            builder.Property(b => b.CompanyName).IsRequired(false).HasDefaultValue(false); // Make sure Name is nullable
        }
    }
}
