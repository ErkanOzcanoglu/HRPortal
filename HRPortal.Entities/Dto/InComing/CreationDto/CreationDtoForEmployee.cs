﻿using HRPortal.Entities.Dto.OutComing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.CreationDto {
    public class CreationDtoForEmployee : BaseDto {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public bool IsVerificated { get; set; }
        public string Phone { get; set; }
        public string TC { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public bool IsAdmin { get; set; }
    }
}
