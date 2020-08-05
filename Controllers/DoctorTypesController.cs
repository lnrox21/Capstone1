using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone1.Models;

namespace Capstone1.Controllers
{
    public class DoctorTypesController : Controller
    {
        private CapstoneEntities1 db = new CapstoneEntities1();

        // GET: DoctorTypes
        public ActionResult Index()
        {
            return View(db.DoctorTypes.ToList());
        }

        // GET: DoctorTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorType doctorType = db.DoctorTypes.Find(id);
            if (doctorType == null)
            {
                return HttpNotFound();
            }
            return View(doctorType);
        }

        // GET: DoctorTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeName")] DoctorType doctorType)
        {
            if (ModelState.IsValid)
            {
                db.DoctorTypes.Add(doctorType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctorType);
        }

        // GET: DoctorTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorType doctorType = db.DoctorTypes.Find(id);
            if (doctorType == null)
            {
                return HttpNotFound();
            }
            return View(doctorType);
        }

        // POST: DoctorTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeName")] DoctorType doctorType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctorType);
        }

        // GET: DoctorTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorType doctorType = db.DoctorTypes.Find(id);
            if (doctorType == null)
            {
                return HttpNotFound();
            }
            return View(doctorType);
        }

        // POST: DoctorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorType doctorType = db.DoctorTypes.Find(id);
            db.DoctorTypes.Remove(doctorType);
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
