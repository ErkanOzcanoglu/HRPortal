using HRPortal.Entities.Dto.OutComing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.CreationDto
{
    public class CreationDtoForTasks : BaseDto
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskPriority { get; set; }
        public string TaskType { get; set; }
        public string TaskStartDate { get; set; }
        public string TaskEndDate { get; set; }
        public string TaskDuration { get; set; }
    }
}
