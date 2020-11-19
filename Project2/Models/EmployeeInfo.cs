using System;
using System.Collections.Generic;

namespace Project2.Models
{
    public partial class EmployeeInfo
    {
        public int EmployeeId { get; set; }
        public int EmployeeCount { get; set; }
        public int EmployeeNumber { get; set; }
        public int? CostId { get; set; }
        public int? DetailId { get; set; }
        public int? HistoryId { get; set; }
        public int? ReviewId { get; set; }
        public int? JobId { get; set; }

        public virtual EmployeeDetails EmployeeNumberNavigation { get; set; }
    }
}
