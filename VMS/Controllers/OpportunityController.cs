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
    public class OpportunityController : Controller
    {
        private dbEntities db = new dbEntities();




        public ActionResult Index(CustomerCustom model = null)
        {
            int i;
            if (model != null)
            {
                i = model.CurrentPageIndex;
            }
            model = new CustomerCustom
            {
                cust = db.Odbs.ToList(),
                custDDL = db.Odbs.ToList()
            };
            var res = (from s in model.cust
                       select s);
            res = res.ToList();
            if (model.CurrentPageIndex == 0)
            {
                model.CurrentPageIndex = 0;
            }
            model.PageSize = 2;
            model.PageCount = ((res.Count() + model.PageSize - 1) / model.PageSize);
            if (model.CurrentPageIndex > model.PageCount)
            {
                model.CurrentPageIndex = model.PageCount;
            }
            model.cust = res.Skip(model.CurrentPageIndex * model.PageSize).Take(model.PageSize);



            return View(model);
        }


        [HttpPost]
        // GET: Opportunity
        public ActionResult Index( CustomerCustom model, string btn = null)
        {

            if (model.SortField == null)
            {
                model.SortField = "OCenter";
                model.SortDirection = "ascending";
            }
            #region SortData

            switch (model.SortField)
            {
                case "OCenter":
                    model.cust = (model.SortDirection == "ascending" ?
                        db.Odbs.OrderBy(c => c.OCenter) :
                        db.Odbs.OrderByDescending(c => c.OCenter));
                    break;
                case "CustomerLastName":
                    model.cust = (model.SortDirection == "ascending" ?
                        db.Odbs.OrderBy(c => c.OSkills) :
                        db.Odbs.OrderByDescending(c => c.OSkills));
                    break;
            }

            #endregion

            var ddl = (from d in model.cust
                       select d);
            model.custDDL = ddl;

            #region FilterData

            if (!String.IsNullOrEmpty(model.SelectedFirstName))
            {
                model.cust = model.cust.Where(s => s.OCenter.ToString().Trim().Equals(model.SelectedFirstName.Trim()));
            }
            if (!String.IsNullOrEmpty(model.SelectedLastName))
            {
                model.cust = model.cust.Where(s => s.OSkills.ToString().Trim().Equals(model.SelectedLastName.Trim()));
            }

            #endregion

            var res = (from s in model.cust
                       select s);
            res = res.ToList();
            if (model.CurrentPageIndex == 0)
            {
                model.CurrentPageIndex = 0;
            }
            model.PageSize = 100;
            model.PageCount = ((res.Count() + model.PageSize - 1) / model.PageSize);
            if (model.CurrentPageIndex > model.PageCount)
            {
                model.CurrentPageIndex = model.PageCount;
            }
            model.cust = res.Skip(model.CurrentPageIndex * model.PageSize).Take(model.PageSize);
            return View(model);


        }

        // GET: Opportunity/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Odb odb = db.Odbs.Find(id);
            if (odb == null)
            {
                return HttpNotFound();
            }
            return View(odb);
        }

        // GET: Opportunity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Opportunity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VOppID,OName,ODate,OCenter,OSkills")] Odb odb)
        {
            if (ModelState.IsValid)
            {
                db.Odbs.Add(odb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(odb);

       
        }

        // GET: Opportunity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Odb odb = db.Odbs.Find(id);
            if (odb == null)
            {
                return HttpNotFound();
            }
            return View(odb);
        }

        // POST: Opportunity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VOppID,OName,ODate,OCenter,OSkills")] Odb odb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(odb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(odb);
        }

        // GET: Opportunity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Odb odb = db.Odbs.Find(id);
            if (odb == null)
            {
                return HttpNotFound();
            }
            return View(odb);
        }

        // POST: Opportunity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Odb odb = db.Odbs.Find(id);
            db.Odbs.Remove(odb);
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
