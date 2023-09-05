using HRPortal.Entities.Dto.OutComing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Entities.Dto.InComing.CreationDto
{
    public class CreationDtoForBudget : BaseDto
    {
        /// <summary>
        /// Gets or sets the budget description.
        /// </summary>
        /// <value>
        /// The budget description.
        /// </value>
        public string BudgetDescription { get; set; }

        /// <summary>
        /// Gets or sets the budget amount.
        /// </summary>
        /// <value>
        /// The budget amount.
        /// </value>
        public string BudgetAmount { get; set; }
    }
}
