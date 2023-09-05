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
    public class ProjectConfiguration : Profile {
        public ProjectConfiguration() {
            CreateMap<CreationDtoForProject, Project>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProjectEndDate, opt => opt.MapFrom(src => src.ProjectEndDate))
                .ForMember(dest => dest.ProjectManager, opt => opt.MapFrom(src => src.ProjectManager))
                .ForMember(dest => dest.ProjectStartDate, opt => opt.MapFrom(src => src.ProjectStartDate))
                .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                .ForMember(dest => dest.ProjectType, opt => opt.MapFrom(src => src.ProjectType));

            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProjectEndDate, opt => opt.MapFrom(src => src.ProjectEndDate))
                .ForMember(dest => dest.ProjectManager, opt => opt.MapFrom(src => src.ProjectManager))
                .ForMember(dest => dest.ProjectStartDate, opt => opt.MapFrom(src => src.ProjectStartDate))
                .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                .ForMember(dest => dest.ProjectType, opt => opt.MapFrom(src => src.ProjectType));

            CreateMap<UpdateDtoForProject, Project>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProjectEndDate, opt => opt.MapFrom(src => src.ProjectEndDate))
                .ForMember(dest => dest.ProjectManager, opt => opt.MapFrom(src => src.ProjectManager))
                .ForMember(dest => dest.ProjectStartDate, opt => opt.MapFrom(src => src.ProjectStartDate))
                .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                .ForMember(dest => dest.ProjectType, opt => opt.MapFrom(src => src.ProjectType));
        }


        public void Configure(EntityTypeBuilder<Project> builder) {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Owner).WithMany(b => b.Projects).HasForeignKey(b => b.OwnerId);
        }
    }
}
