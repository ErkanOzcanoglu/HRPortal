using AutoMapper;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.UpdateDto;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRPortal.DataAccessLayer.Configuration
{
    public class CreditCardConfiguration : Profile {
        public CreditCardConfiguration() {
            CreateMap<CreationDtoForCreditCard, CreditCard>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber))
                .ForMember(dest => dest.CardHolderName, opt => opt.MapFrom(src => src.CardHolderName))
                .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType))
                .ForMember(dest => dest.CardSecurityCode, opt => opt.MapFrom(src => src.CardSecurityCode))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate)).ReverseMap();

            CreateMap<CreditCard, CreditCardDto>()
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber))
                .ForMember(dest => dest.CardHolderName, opt => opt.MapFrom(src => src.CardHolderName))
                .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType))
                .ForMember(dest => dest.CardSecurityCode, opt => opt.MapFrom(src => src.CardSecurityCode))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate)).ReverseMap();

            CreateMap<UpdateDtoForCreditCard, CreditCard>()
               .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId)).ReverseMap();
        }

        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<CreditCard> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Company).WithOne(b => b.CreditCards).HasForeignKey<CreditCard>(b => b.CompanyId);


        }
    }
}
