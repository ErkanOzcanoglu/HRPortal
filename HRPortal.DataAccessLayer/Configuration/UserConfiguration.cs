using AutoMapper;
using HRPortal.Entities.Dto.InComing.UserForAuth;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class UserConfiguration : Profile {
        public UserConfiguration() {
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => System.Guid.NewGuid()))
                .ForMember(dest => dest.IsVerificated, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Email));

            CreateMap<LoginDto, User>()
                .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Email));
        }

        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Company).WithMany(b => b.Users).HasForeignKey(b => b.CompanyId);

            builder.Property(b => b.Address).IsRequired(false);
            builder.Property(b => b.Phone).IsRequired(false);
            builder.Property(b => b.TC).IsRequired(false);
            builder.Property(b => b.DateOfBirth).IsRequired(false);
            builder.Property(b => b.Title).IsRequired(false);
            builder.Property(b => b.Department).IsRequired(false);
            builder.Property(b => b.Authority).IsRequired(false);
        }
    }
}
