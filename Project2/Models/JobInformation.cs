using System;
using System.Collections.Generic;

namespace Project2.Models
{
    public partial class JobInformation
    {
        public int JobId { get; set; }
        public string BusinessTravel { get; set; }
        public string Department { get; set; }
        public int? EmployeeNumber { get; set; }
        public int? JobInvolvement { get; set; }
        public int? JobLevel { get; set; }
        public string JobRole { get; set; }

        public virtual EmployeeDetails EmployeeNumberNavigation { get; set; }
    }
}
