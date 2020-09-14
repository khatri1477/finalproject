using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if (uid != 0)
            {
                DateTime now = DateTime.Now;
                var obj = db.applicationForms.Where(x => x.aadhar.Equals(applicationForm.aadhar)).FirstOrDefault();
                if (obj == null)
                {
                    string status = "In-Progress";
                    applicationForm.applyDate = DateTime.Now;
                    applicationForm.userId = uid;
                    applicationForm.status = status;
                    db.applicationForms.Add(applicationForm);
                    db.SaveChanges();
                    return RedirectToAction("applicationList", "salesPerson");
                }

                var olddate = obj.applyDate.Date;
                var todaydate = now.Date;

                int monthsApart = 12 * (olddate.Year - todaydate.Year) + olddate.Month - todaydate.Month;
                int diff = Math.Abs(monthsApart);
                if (diff <= 6)
                {
                    if (obj.status == "RejectedByRCU" || obj.status == "RejectedByCreditBuyer" || obj.status == "RejectedByCreditManager")
                    {
                        TempData["Message"] = "Cant not be register beacuse application was rejected in last six month";
                        return RedirectToAction("ApplicationForm", "salesPerson");
                    }
                    if (obj.status == "In-Progress" || obj.status == "ApprovedByRCU" || obj.status=="ApprovedByCreditBuyer")
                    {
                        TempData["Message"] = "Can't be registed these user because one application  status is in progress";
                        return RedirectToAction("ApplicationForm", "salesPerson");
                    }

                }
                if (diff > 6 && obj.status == "ApprovedByCreditManager")
                {

                    string status = "In-Progress";
                    applicationForm.applyDate = DateTime.Now;
                    applicationForm.userId = uid;
                    db.applicationForms.Add(applicationForm);
                    db.SaveChanges();
                    return RedirectToAction("applicationList", "salesPerson");
                }
            }
            return RedirectToAction("login", "Front");
        }

        public ActionResult ApplicationList()
        {

            var userid = Session["userid"];
            int uid = Convert.ToInt32(userid);
            if (uid != 0)
            {
                ViewBag.id = uid;
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
        public ActionResult approveByRcu(int? id)
        {

            var userid = Session["userid"];
            int uid = Convert.ToInt32(userid);
            if (uid != 0)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationForm applicationForm = db.applicationForms.Find(id);
                applicationForm.status = "ApprovedByRCU";
                applicationForm.userId = uid;
                db.SaveChanges();
                return RedirectToAction("applicationList", "salesPerson");
            }
            return RedirectToAction("login", "Front");

        }

        public ActionResult RejectByRcu(int? id)
        {
            var userid = Session["userid"];
            int uid = Convert.ToInt32(userid);
            if (uid != 0)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationForm applicationForm = db.applicationForms.Find(id);

                applicationForm.status = "RejectedByRCU";
                applicationForm.userId = uid;
                db.SaveChanges();
                return RedirectToAction("applicationList", "salesPerson");
            }
            return RedirectToAction("login", "Front");
        }

    }
}