using AutoMapper;
using HRPortal.Entities.Dto.InComing;
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
    public class TaskConfiguration : Profile {

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskConfiguration"/> class.
        /// </summary>
        public TaskConfiguration() {
            CreateMap<CreationDtoForTasks, Tasks>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.TaskDescription, opt => opt.MapFrom(src => src.TaskDescription))
                .ForMember(dest => dest.TaskDuration, opt => opt.MapFrom(src => src.TaskDuration))
                .ForMember(dest => dest.TaskEndDate, opt => opt.MapFrom(src => src.TaskEndDate))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.TaskPriority, opt => opt.MapFrom(src => src.TaskPriority))
                .ForMember(dest => dest.TaskStartDate, opt => opt.MapFrom(src => src.TaskStartDate))
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => src.TaskType));

            CreateMap<Tasks, TasksDto>()
                .ForMember(dest => dest.TaskDescription, opt => opt.MapFrom(src => src.TaskDescription))
                .ForMember(dest => dest.TaskDuration, opt => opt.MapFrom(src => src.TaskDuration))
                .ForMember(dest => dest.TaskEndDate, opt => opt.MapFrom(src => src.TaskEndDate))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.TaskPriority, opt => opt.MapFrom(src => src.TaskPriority))
                .ForMember(dest => dest.TaskStartDate, opt => opt.MapFrom(src => src.TaskStartDate))
                .ForMember(dest => dest.TaskType, opt => opt.MapFrom(src => src.TaskType));
        }

        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Tasks> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Project).WithMany(b => b.Tasks).HasForeignKey(b => b.ProjectId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Owner).WithMany(b => b.Tasks).HasForeignKey(b => b.TaskOwnerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
