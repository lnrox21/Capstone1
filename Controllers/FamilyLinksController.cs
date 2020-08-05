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
    public class FamilyLinksController : Controller
    {
        private CapstoneEntities1 db = new CapstoneEntities1();

        // GET: FamilyLinks
        public ActionResult Index()
        {
            var familyLinks = db.FamilyLinks.Include(f => f.UserProfile).Include(f => f.UserProfile1);
            return View(familyLinks.ToList());
        }

        // GET: FamilyLinks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyLink familyLink = db.FamilyLinks.Find(id);
            if (familyLink == null)
            {
                return HttpNotFound();
            }
            return View(familyLink);
        }

        // GET: FamilyLinks/Create
        public ActionResult Create()
        {
            ViewBag.InitiatingId = new SelectList(db.UserProfiles, "Id", "UserId");
            ViewBag.ReceivingProfileId = new SelectList(db.UserProfiles, "Id", "UserId");
            return View();
        }

        // POST: FamilyLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InitiatingId,ReceivingProfileId,IsApproved,RelationshipType")] FamilyLink familyLink)
        {
            if (ModelState.IsValid)
            {
                db.FamilyLinks.Add(familyLink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InitiatingId = new SelectList(db.UserProfiles, "Id", "UserId", familyLink.InitiatingId);
            ViewBag.ReceivingProfileId = new SelectList(db.UserProfiles, "Id", "UserId", familyLink.ReceivingProfileId);
            return View(familyLink);
        }

        // GET: FamilyLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyLink familyLink = db.FamilyLinks.Find(id);
            if (familyLink == null)
            {
                return HttpNotFound();
            }
            ViewBag.InitiatingId = new SelectList(db.UserProfiles, "Id", "UserId", familyLink.InitiatingId);
            ViewBag.ReceivingProfileId = new SelectList(db.UserProfiles, "Id", "UserId", familyLink.ReceivingProfileId);
            return View(familyLink);
        }

        // POST: FamilyLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InitiatingId,ReceivingProfileId,IsApproved,RelationshipType")] FamilyLink familyLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familyLink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InitiatingId = new SelectList(db.UserProfiles, "Id", "UserId", familyLink.InitiatingId);
            ViewBag.ReceivingProfileId = new SelectList(db.UserProfiles, "Id", "UserId", familyLink.ReceivingProfileId);
            return View(familyLink);
        }

        // GET: FamilyLinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamilyLink familyLink = db.FamilyLinks.Find(id);
            if (familyLink == null)
            {
                return HttpNotFound();
            }
            return View(familyLink);
        }

        // POST: FamilyLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FamilyLink familyLink = db.FamilyLinks.Find(id);
            db.FamilyLinks.Remove(familyLink);
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
