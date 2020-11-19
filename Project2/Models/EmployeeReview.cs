using System;
using System.Collections.Generic;

namespace Project2.Models
{
    public partial class EmployeeReview
    {
        public int ReviewId { get; set; }
        public int? EmployeeNumber { get; set; }
        public int? EnvironmentSatisfaction { get; set; }
        public int? JobSatisfaction { get; set; }
        public int? PerformanceRating { get; set; }
        public int? RelationshipSatisfaction { get; set; }

        public virtual EmployeeDetails EmployeeNumberNavigation { get; set; }
    }
}
