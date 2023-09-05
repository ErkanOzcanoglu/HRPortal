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

namespace HRPortal.DataAccessLayer.Configuration
{
    public class BudgetConfiguration : Profile {
        public BudgetConfiguration() {
            CreateMap<CreationDtoForBudget, Budget>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.BudgetAmount, opt => opt.MapFrom(src => src.BudgetAmount))
                .ForMember(dest => dest.BudgetDescription, opt => opt.MapFrom(src => src.BudgetDescription));

            CreateMap<Budget, BudgetDto>()
                .ForMember(dest => dest.BudgetAmount, opt => opt.MapFrom(src => src.BudgetAmount))
                .ForMember(dest => dest.BudgetDescription, opt => opt.MapFrom(src => src.BudgetDescription));

            CreateMap<UpdateDtoForBudget, Budget>()
                .ForMember(dest => dest.BudgetAmount, opt => opt.MapFrom(src => src.BudgetAmount))
                .ForMember(dest => dest.BudgetDescription, opt => opt.MapFrom(src => src.BudgetDescription));

        }
        public void Configure(EntityTypeBuilder<Budget> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Event).WithMany(e => e.Budgets).HasForeignKey(b => b.EventId);
        }
    }
}
