using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VMS.Models;

namespace VMS.Controllers
{
    public class VMSystemsController : Controller
    {
        private VMSystemsEntities db = new VMSystemsEntities();

        // GET: VMSystems
        public ActionResult Volunteer()
        {
            return View(db.VMSystems.ToList());
        }

        // GET: VMSystems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMSystem vMSystem = db.VMSystems.Find(id);
            if (vMSystem == null)
            {
                return HttpNotFound();
            }
            return View(vMSystem);
        }

        // GET: VMSystems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VMSystems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VMID,VFirstName,VLastName,VEmail,VUsername,VPassword,VMobile")] VMSystem vMSystem)
        {
            if (ModelState.IsValid)
            {
                db.VMSystems.Add(vMSystem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vMSystem);
        }

        // GET: VMSystems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMSystem vMSystem = db.VMSystems.Find(id);
            if (vMSystem == null)
            {
                return HttpNotFound();
            }
            return View(vMSystem);
        }

        // POST: VMSystems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VMID,VFirstName,VLastName,VEmail,VUsername,VPassword,VMobile")] VMSystem vMSystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vMSystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vMSystem);
        }

        // GET: VMSystems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMSystem vMSystem = db.VMSystems.Find(id);
            if (vMSystem == null)
            {
                return HttpNotFound();
            }
            return View(vMSystem);
        }

        // POST: VMSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VMSystem vMSystem = db.VMSystems.Find(id);
            db.VMSystems.Remove(vMSystem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
