﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.CreationDto
{
    public class CreationDtoForUser {
        public bool IsAdmin { get; set; }
        public Guid CompanyId { get; set; }
    }
}
