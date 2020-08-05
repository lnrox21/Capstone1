using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone1.Models;
using Microsoft.AspNet.Identity;

namespace Capstone1.Controllers
{
    public class DiseasesController : Controller
    {
        private CapstoneEntities1 db = new CapstoneEntities1();
        //[Authorize(Roles ="Admin")]
        // GET: Diseases
        public ActionResult Index()
        {
            return View(db.Diseases.ToList());
        }

        // GET: Diseases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }
        [Authorize(Roles = "Admin")]
        // GET: Diseases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diseases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DiseaseName,Description")] Disease disease)
        {
            if (ModelState.IsValid)
            {
                db.Diseases.Add(disease);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disease);
        }
        [Authorize(Roles = "Admin")]
        // GET: Diseases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DiseaseName,Description")] Disease disease)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disease).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disease);
        }
        [Authorize(Roles = "Admin")]
        // GET: Diseases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disease disease = db.Diseases.Find(id);
            db.Diseases.Remove(disease);
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

        public ActionResult LinkSymptoms()
        {

            ViewBag.AllSymptoms = new MultiSelectList(db.Symptoms.ToList(), "Id", "SymptomName");
            return View();
        }
        [HttpPost]
        public ActionResult ShowDiseasesBySymptoms(SymptomListViewModel model)
        {
            var UserId = User.Identity.GetUserId();
            var Profile = db.UserProfiles.Single(U => U.UserId == UserId);
            
            var MatchingDiseases = new List<Disease>();
            if (ModelState.IsValid && model != null && model.SymptomIds != null)
            {
                

                foreach (var Disease in db.Diseases)
                {
                    var MatchingSymptoms = Disease.Symptoms.Join(model.SymptomIds, s => s.Id, x => x, (l, r) => l);
                    var SymptomCount = MatchingSymptoms.Count();
                    if (SymptomCount >=3)
                    {
                        MatchingDiseases.Add(Disease);
                    }
                }
                

                

            }

            return View(MatchingDiseases);
            
        }
        [Authorize (Roles = "Admin")]
        public ActionResult EditSymptoms(int? id)
        {
            var Disease = db.Diseases.Find(id);
            var ExistingSymptoms = Disease.Symptoms.Select(d => d.Id).ToList();
            ViewBag.AllSymptoms = new MultiSelectList(db.Symptoms.ToList(), "Id", "SymptomName", ExistingSymptoms);
            return View();
        }
        [HttpPost]
        public ActionResult EditSymptoms(int? id, SymptomListViewModel model)
        {

            var Disease = db.Diseases.Find(id);

            if (ModelState.IsValid)
            {
                

                Disease.Symptoms.Clear();
                if (model?.SymptomIds != null)
                {
                    foreach (var SymptomId in model.SymptomIds)
                    {
                        var Symptom = db.Symptoms.Find(SymptomId);
                        Disease.Symptoms.Add(Symptom);

                    }
                }

                db.Entry(Disease).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id = id});

            }

            
            var ExistingSymptoms = Disease.Symptoms.Select(d => d.Id).ToList();
            ViewBag.AllSymptoms = new MultiSelectList(db.Symptoms.ToList(), "Id", "SymptomName", ExistingSymptoms);
            return View(model);
        }


        //public ActionResult LinkDoctors()
        //{
        //    var UserId = User.Identity.GetUserId();
        //    var Profile = db.UserProfiles.Single(U => U.UserId == UserId);
        //    List<int> ExistingDoctors = Profile.Diseases.Select(d => d.Id).ToList();
        //    ViewBag.AllDoctors = new MultiSelectList(db.DoctorTypes.ToList(), "Id", "DoctorTypes", ExistingDoctors);
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult LinkDoctors(AddDiseaseViewModel model)
        //{
        //    var UserId = User.Identity.GetUserId();
        //    var Profile = db.UserProfiles.Single(U => U.UserId == UserId);

        //    if (ModelState.IsValid)
        //    {
        //        Doctor.DoctorType.Clear();
        //        if (model?.DoctorIds != null)
        //        {
        //            foreach (var DiseaseId in model.DiseaseIds)
        //            {
        //                var Doctor = db.DoctorType.Find(DoctorId);
        //                DoctorType.Doctors.Add(Doctor);

        //            }
        //        }

        //        db.Entry(Profile).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");

        //    }

        //    var ExistingDoctors = Profile.Doctors.ToList();
        //    ViewBag.AllDoctors = new MultiSelectList(db.Doctors.ToList(), "Id", "DoctorType", ExistingDoctors);
        //    return View(model);
        //}
    }
}
