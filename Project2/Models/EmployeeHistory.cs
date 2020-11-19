using System;
using System.Collections.Generic;

namespace Project2.Models
{
    public partial class EmployeeHistory
    {
        public int HistoryId { get; set; }
        public int? EmployeeNumber { get; set; }
        public int? NumCompaniesWorked { get; set; }
        public int? TotalWorkingYears { get; set; }
        public int? TrainingTimesLastYear { get; set; }
        public int? YearsAtCompany { get; set; }
        public int? YearsInCurrentRole { get; set; }
        public int? YearsSinceLastPromotion { get; set; }
        public int? YearsWithCurrManager { get; set; }

        public virtual EmployeeDetails EmployeeNumberNavigation { get; set; }
    }
}
