using AutoMapper;
using HRPortal.Entities.Dto.InComing.DtoForUserCompanyInfo;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Mapper {
    public class UserCompanyMapper : Profile {
        public UserCompanyMapper() {
            CreateMap<UserCompanyCreationDto, UserCompanyInfo>()
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TotalDaysOff, opt => opt.MapFrom(src => src.TotalDaysOff))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<UserCompanyInfo, UserCompanyDto>()
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TotalDaysOff, opt => opt.MapFrom(src => src.TotalDaysOff))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate));
        }

        public void Configure(EntityTypeBuilder<UserCompanyInfo> builder) {
            builder.HasOne(x => x.UserPersonalInfo)
                .WithOne(x => x.UserCompanyInfo)
                .HasForeignKey<UserCompanyInfo>(x => x.UserPersonalId);
        }
    }
}
