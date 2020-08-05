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
    [Authorize]
    public class UserProfilesController : Controller
    {
        private CapstoneEntities1 db = new CapstoneEntities1();

        // GET: UserProfiles
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var Profile = db.UserProfiles.Single(p => p.UserId == UserId);
            
            return View(Profile);
        }

        // GET: UserProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // GET: UserProfiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FirstName,LastName,PhoneNumber,Email,StreetAddress,City,StateAbrev,Zipcode,Birthdate")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(userProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userProfile);
        }

        // GET: UserProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FirstName,LastName,PhoneNumber,Email,StreetAddress,City,StateAbrev,Zipcode,Birthdate")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userProfile);
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

        public ActionResult LinkDiseases()
        {
            var UserId = User.Identity.GetUserId();
            var Profile = db.UserProfiles.Single(U => U.UserId == UserId);
            var ExistingDiseases = Profile.Diseases.Select(d =>d.Id).ToList();
            ViewBag.AllDiseases = new MultiSelectList(db.Diseases.ToList(), "Id", "DiseaseName", ExistingDiseases);
            return View();
        }
        [HttpPost]
        public ActionResult LinkDiseases(AddDiseaseViewModel model)
        {
            var UserId = User.Identity.GetUserId();
            var Profile = db.UserProfiles.Single(U => U.UserId == UserId);

            if (ModelState.IsValid)
            {
                Profile.Diseases.Clear();
                if (model?.DiseaseIds != null)
                { foreach (var DiseaseId in model.DiseaseIds)
                    {
                        var Disease = db.Diseases.Find(DiseaseId);
                        Profile.Diseases.Add(Disease);

                    } 
                }

                db.Entry(Profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                  
            }
            
            var ExistingDiseases = Profile.Diseases.ToList();
            ViewBag.AllDiseases = new MultiSelectList(db.Diseases.ToList(), "Id", "DiseaseName", ExistingDiseases);
            return View(model);
        }


       
    }
}
