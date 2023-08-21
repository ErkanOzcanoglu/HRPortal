using AutoMapper;
using HRPortal.Entities.Dto.InComing.DtoForCompany;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Mapper {
    public class CompanyMapper : Profile {
        public CompanyMapper() {
            CreateMap<CompanyCreationDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => System.Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToUpper()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.TaxId, opt => opt.MapFrom(src => 123456))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .AfterMap((src, dest) => {
                    if (dest.PhoneNumber.ToString().Length != 10) {
                        throw new ArgumentException("PhoneNumber must be a 10-digit number.");
                    }
                    if (!dest.EmailAddress.EndsWith("@gmail.com") && !dest.EmailAddress.EndsWith("@hotmail.com")) {
                        throw new ArgumentException("Email must end with @gmail.com or @hotmail.com.");
                    }
                });

            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.TaxId, opt => opt.MapFrom(src => src.TaxId))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website));

            CreateMap<CreditCardCreationDto, Company>()
                .ForMember(dest => dest.CardHolderName, opt => opt.MapFrom(src => src.CardHolderName))
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber))
                .ForMember(dest => dest.CardSecurityCode, opt => opt.MapFrom(src => src.CardSecurityCode))
                .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate));
        }

        public void Configure(EntityTypeBuilder<Company> builder) {
            builder.HasOne(c => c.UserPersonalInfo)
                .WithMany(u => u.Companies)
                .HasForeignKey(c => c.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder.Ignore(c => c.CardHolderName);
            builder.Ignore(c => c.CardNumber);
            builder.Ignore(c => c.CardSecurityCode);
            builder.Ignore(c => c.CardType);
            builder.Ignore(c => c.ExpirationDate);
        }
    }
}
