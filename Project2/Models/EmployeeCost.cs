using System;
using System.Collections.Generic;

namespace Project2.Models
{
    public partial class EmployeeCost
    {
        public int CostId { get; set; }
        public int? DailyRate { get; set; }
        public int? EmployeeNumber { get; set; }
        public int? HourlyRate { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public int? MonthlyRate { get; set; }
        public string OverTime { get; set; }
        public double? PercentSalaryHike { get; set; }
        public int? StandardHours { get; set; }

        public virtual EmployeeDetails EmployeeNumberNavigation { get; set; }
    }
}
