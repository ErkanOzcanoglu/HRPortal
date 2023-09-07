using AutoMapper;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class EmployeeCompanyInformationConfiguration : Profile {
        public EmployeeCompanyInformationConfiguration() {
            CreateMap<CreationDtoForEmployeeCompanyInformation, EmployeeCompanyInformation>()
                .ForMember(u => u.Status, opt => opt.MapFrom(src => 1))
                .ForMember(u => u.IsAdmin, opt => opt.MapFrom(src => false));

            CreateMap<EmployeeCompanyInformation, EmployeeCompanyInformationDto>()
                .ForMember(u => u.IsAdmin, opt => opt.MapFrom(src => false));

            CreateMap<EmployeeCompanyInformation, EmployeeCompanyInformationDto>()
                 .ForMember(u => u.IsAdmin, opt => opt.MapFrom(src => false));
        }

        public void Configure(EntityTypeBuilder<EmployeeCompanyInformation> builder) {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Employee).WithOne(x => x.EmployeeCompanyInformation).HasForeignKey<EmployeeCompanyInformation>(x => x.EmployeeId);
            //builder.HasOne(b => b.Company).WithMany(b => b.Users).HasForeignKey(b => b.CompanyId);
        }
    }
}