using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class salesPersonController : Controller
    {
        UsersContext db = new UsersContext();

        // GET: salesPerson
        public ActionResult ApplicationForm()
        {
            var userid = Session["userid"];
            int uid = Convert.ToInt32(userid);
            if(uid !=0)
            {

                ViewBag.EmployeementTypeId = new SelectList(db.employeementTypes, "EmployeementTypeId", "employeeType");
                return View();
            }
            return RedirectToAction("login", "departmentLogin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicationForm(ApplicationForm applicationForm)
        {
            var userid = Session["userid"];
            int uid = Convert.ToInt32(userid);
            if(uid !=0)
            {
               // var ifexist = db.applicationForms.Where(x => x.aadhar.Equals(applicationForm.aadhar)).FirstOrDefault();
               // var now = DateTime.Now;
               // var months = Enumerable.Range(-12, 12)
               //.Select(x => new {
                 //   year = now.AddMonths(x).Year,
                  //  month = now.AddMonths(x).Month
                //});
                applicationForm.applyDate = DateTime.Now;
                applicationForm.status = "In-Progress";
                applicationForm.userId = uid;
                db.applicationForms.Add(applicationForm);
                db.SaveChanges();
                return View();
            }
            return RedirectToAction("login", "departmentLogin");
        }

        public ActionResult ApplicationList()
        {

            var userid = Session["userid"];
            int uid = Convert.ToInt32(userid);
            if (uid != 0)
            {

                return View(db.applicationForms.ToList());
            }
            return RedirectToAction("login", "departmentLogin");
        }

        public ActionResult ApplicantDetails(int ? id)
        {
            var userid = Session["userid"];
            int uid = Convert.ToInt32(userid);
            if (uid != 0)
            {
                var applicantDetails = db.applicationForms.Find(id);
                return View(applicantDetails);
            }
            return RedirectToAction("login", "departmentLogin");
        }
    }
}