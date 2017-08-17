using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VMS.Models;

namespace VMS.Controllers
{
    

    public class HomeController : Controller
    {
        private VMSystemsEntities db = new VMSystemsEntities();

        public ActionResult Index()
        {
            //Weeeeee
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(VMSystem reg)
        {
            if (ModelState.IsValid)
            {
                db.VMSystems.Add(reg);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }

        public ActionResult Welcome()
        {
            if (Session["VUsername"] == null)
            {
                return RedirectToAction("Login");


            }
            else
            {
                return View();

            }


        }

        public ActionResult Volunteer()
        {
            if (Session["VUsername"] == null)
            {
                return RedirectToAction("Login");

               
            }
            else
            {
                return View();
                
        }
        }
        
        public ActionResult Opportunity()
        {
            if (Session["VUsername"] == null)
            {
                return RedirectToAction("Login");


            }
            else
            {
                return View();

            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(VMSystem reg)
        {
            if (ModelState.IsValid)
            {
                var details = (from userlist in db.VMSystems
                               where userlist.VUsername == reg.VUsername && userlist.VPassword == reg.VPassword
                               select new
                               {
                                   userlist.VMID,
                                   userlist.VUsername

                               }).ToList();
                if (details.FirstOrDefault() != null)
                {
                    Session["VMID"] = details.FirstOrDefault().VMID;
                    Session["VUsername"] = details.FirstOrDefault().VUsername;
                    return RedirectToAction("Welcome", "Home");
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credentials");

            }
            return View(reg);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Home");
        }

       
    }
}