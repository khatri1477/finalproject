using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class departmentLoginController : Controller
    {
        UsersContext db = new UsersContext();

        // GET: departmentLogin
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(users users)
        {
            var obj = db.user.Where(x => x.email.Equals(users.email) && x.Password.Equals(users.Password)).FirstOrDefault();
            if(obj!=null)
            {
                Session["userid"] = obj.userId;
                string role = obj.Role;
                if (role == "Sales Person")
                {
                    return RedirectToAction("ApplicationForm", "salesPerson");
                }
            }
            else
            {
                ViewBag.Error = "Invalid Credentials";
               
            }
            return View(users);

        }
    }
}