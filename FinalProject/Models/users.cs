using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FinalProject.Models
{

    public class EmployeementType
    {
        [Key]
        public int EmployeementTypeId { get; set; }

        [Display(Name ="Employee Type")]
        public string employeeType { get; set; }
    }
    public class users
    {
        [Key]
        public int userId { get; set; }

        [Display(Name ="Email")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }
    }

    public class ApplicationForm
    {
        [Key]
        public int ApplicationId { get; set; }

        [Display(Name ="Applicant Name")]
        [Required]
        public string applicantName { get; set; }

        [Display(Name ="Father's Name")]
        [Required]
        public string FatherName { get; set; }
        
        [Display(Name ="Co-Applicant Name")]
        [Required]
        public string coApplicantName { get; set; }

        [Display(Name="Address")]
        [Required]
        public string Address { get; set; }

        [Display(Name="Aadhar")]
        [Required]
        [RegularExpression("^[2-9]{1}[0-9]{3}\\s[0-9]{4}\\s[0-9]{4}$", ErrorMessage ="Invalid AdharNumber")]
        public string aadhar { get; set; }

        [Display(Name="PAN")]
        [Required]
        [RegularExpression("^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$",ErrorMessage ="Invalid Pan Number")]
        public string panNumber { get; set; }

        [Display(Name ="Mobile Number")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string mobileNo { get; set; }

        public virtual int EmployeementTypeId { get; set; }
        public virtual EmployeementType GetEmployeementType { get; set; }

        [Display(Name="Requested Amount")]
        [Required]
        public decimal requestedAmount { get; set; }

        [Display(Name="Home Loann")]
        public decimal homeLoan { get; set; }

        [Display(Name="Automobile Loan")]
        public decimal  autoMobile { get; set; }

        [Display(Name="Bussiness Loan")]
        public decimal  bussinessLoan { get; set; }

       
        [DataType(DataType.DateTime)]
        public DateTime applyDate { get; set; }

        public string status { get; set; }

        public virtual int userId { get; set; }
        public virtual users GetUsers { get; set; }

        
    }

    public class loanRequestStatus
    {
        [Key]
        public int id { get; set; }

        public virtual int ApplicationId { get; set; }
        public virtual ApplicationForm GetApplicationForm { get; set; }

        [Display(Name ="Eligible Amount")]
        public decimal eligibleAmount  { get; set; }

        public string Status { get; set; }

        public DateTime modificationDate { get; set; }
    }
}