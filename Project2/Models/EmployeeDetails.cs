using System;
using System.Collections.Generic;

namespace Project2.Models
{
    public partial class EmployeeDetails
    {
        public EmployeeDetails()
        {
            EmployeeCost = new HashSet<EmployeeCost>();
            EmployeeHistory = new HashSet<EmployeeHistory>();
            EmployeeInfo = new HashSet<EmployeeInfo>();
            EmployeeReview = new HashSet<EmployeeReview>();
            JobInformation = new HashSet<JobInformation>();
        }

        public int DetailId { get; set; }
        public int? Age { get; set; }
        public string Attrition { get; set; }
        public string Department { get; set; }
        public int? DistanceFromHome { get; set; }
        public int? Education { get; set; }
        public string EducationField { get; set; }
        public int EmployeeNumber { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Over18 { get; set; }

        public virtual ICollection<EmployeeCost> EmployeeCost { get; set; }
        public virtual ICollection<EmployeeHistory> EmployeeHistory { get; set; }
        public virtual ICollection<EmployeeInfo> EmployeeInfo { get; set; }
        public virtual ICollection<EmployeeReview> EmployeeReview { get; set; }
        public virtual ICollection<JobInformation> JobInformation { get; set; }
    }
}
