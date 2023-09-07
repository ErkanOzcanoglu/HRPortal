using AutoMapper;
using HRPortal.Entities.Dto.InComing.CreationDto;
using HRPortal.Entities.Dto.InComing.UpdateDto;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Configuration {
    public class CompanyWorkersConfiguration : Profile {
        public CompanyWorkersConfiguration() {
            CreateMap<CreationDtoFormCompanyWorkers, CompanyWorkers>()
                .ForMember(b => b.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(b => b.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));

            CreateMap<CompanyWorkers, CompanyWorkersDto>()
                .ForMember(b => b.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(b => b.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));

            CreateMap<UpdateDtoForCompanyWorkers, CompanyWorkers>()
                .ForMember(b => b.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(b => b.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));
        }

        public void Configure(EntityTypeBuilder<CompanyWorkers> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Company).WithMany(b => b.CompanyWorkers).HasForeignKey(b => b.CompanyId);
            builder.HasOne(b => b.Employee).WithMany(b => b.CompanyWorkers).HasForeignKey(b => b.EmployeeId);
        }
    }
}
