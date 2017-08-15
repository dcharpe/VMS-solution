using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.Models;

namespace VMS.Controllers
{
    

    public class HomeController : Controller
    {
        private VMSystemsEntities db = new VMSystemsEntities();

        public ActionResult Index()
        {
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
            return View();
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
    }
}