using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FinalProject.Models
{
    public class UsersContext:DbContext
    {
        public UsersContext() :base("DbCon")
        {

        }
        public DbSet<users> user { get; set; }
        public DbSet<ApplicationForm> applicationForms { get; set; }
        public DbSet<loanRequestStatus> loanRequestStatuses { get; set; }
        public DbSet<EmployeementType> employeementTypes { get; set; }
    }
}