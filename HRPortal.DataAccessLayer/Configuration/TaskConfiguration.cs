using AutoMapper;
using HRPortal.Entities.Dto.InComing.CreationDto;
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
    public class TaskConfiguration : Profile {
        public TaskConfiguration() {
            CreateMap<CreationDtoForTasks, Tasks>()
                .ForMember(b => b.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(b => b.CreatedTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(b => b.Status, opt => opt.MapFrom(src => 1))
                .ForMember(b => b.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(b => b.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(b => b.TaskDescription, opt => opt.MapFrom(src => src.TaskDescription))
                .ForMember(b => b.TaskPriority, opt => opt.MapFrom(src => src.TaskPriority))
                .ForMember(b => b.TaskStartDate, opt => opt.MapFrom(src => src.TaskStartDate))
                .ForMember(b => b.TaskEndDate, opt => opt.MapFrom(src => src.TaskEndDate));


            CreateMap<Tasks, TasksDto>()
                .ForMember(b => b.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(b => b.TaskName, opt => opt.MapFrom(b => b.TaskName))
                .ForMember(b => b.TaskDescription, opt => opt.MapFrom(b => b.TaskDescription))
                .ForMember(b => b.TaskPriority, opt => opt.MapFrom(b => b.TaskPriority))
                .ForMember(b => b.TaskStartDate, opt => opt.MapFrom(b => b.TaskStartDate))
                .ForMember(b => b.TaskEndDate, opt => opt.MapFrom(b => b.TaskEndDate));

            CreateMap<UpdateDtoForTasks, Tasks>()
                .ForMember(b => b.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(b => b.TaskDescription, opt => opt.MapFrom(src => src.TaskDescription))
                .ForMember(b => b.TaskPriority, opt => opt.MapFrom(src => src.TaskPriority))
                .ForMember(b => b.TaskStartDate, opt => opt.MapFrom(src => src.TaskStartDate))
                .ForMember(b => b.TaskEndDate, opt => opt.MapFrom(src => src.TaskEndDate));
        }

        public void Configure(EntityTypeBuilder<Tasks> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Project).WithMany(b => b.Tasks).HasForeignKey(b => b.ProjectId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Owner).WithMany(b => b.Tasks).HasForeignKey(b => b.TaskOwnerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
