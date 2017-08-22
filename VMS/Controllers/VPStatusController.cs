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
    public class VPStatusController : Controller
    {
        private VPrimaryEntities db = new VPrimaryEntities();

        // GET: VPStatus
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "VPSkills")
            {
                return View(db.VPStatus.Where(x => x.VPSkills == search || search == null).ToList());
            }
            else
            {
                return View(db.VPStatus.Where(x => x.VPStatus.StartsWith(search) || search == null).ToList());
            }
        }

        // GET: VPStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VPStatu vPStatu = db.VPStatus.Find(id);
            if (vPStatu == null)
            {
                return HttpNotFound();
            }
            return View(vPStatu);
        }

        // GET: VPStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VPStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VPID,VPFName,VPLName,VPUsername,VPPassword,VPCenter,VPSkills,VPAvailability,PAddress,VPhone,VPEmail,VPStatus")] VPStatu vPStatu)
        {
            if (ModelState.IsValid)
            {
                db.VPStatus.Add(vPStatu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vPStatu);
        }

        // GET: VPStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VPStatu vPStatu = db.VPStatus.Find(id);
            if (vPStatu == null)
            {
                return HttpNotFound();
            }
            return View(vPStatu);
        }

        // POST: VPStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VPID,VPFName,VPLName,VPUsername,VPPassword,VPCenter,VPSkills,VPAvailability,PAddress,VPhone,VPEmail,VPStatus")] VPStatu vPStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vPStatu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vPStatu);
        }

        // GET: VPStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VPStatu vPStatu = db.VPStatus.Find(id);
            if (vPStatu == null)
            {
                return HttpNotFound();
            }
            return View(vPStatu);
        }

        // POST: VPStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VPStatu vPStatu = db.VPStatus.Find(id);
            db.VPStatus.Remove(vPStatu);
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
