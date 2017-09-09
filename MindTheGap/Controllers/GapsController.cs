using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MindTheGap.Models;

namespace MindTheGap.Controllers
{
    public class GapsController : Controller
    {
        private MindTheGapEntities db = new MindTheGapEntities();

        // GET: Gaps
        public ActionResult Index()
        {
            return View(db.Gaps.ToList());
        }

        // GET: Gaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gap gap = db.Gaps.Find(id);
            if (gap == null)
            {
                return HttpNotFound();
            }
            return View(gap);
        }

        // GET: Gaps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GapID,gapSummary,gapStartTime,gapEndTime")] Gap gap)
        {
            if (ModelState.IsValid)
            {
                db.Gaps.Add(gap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gap);
        }

        // GET: Gaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gap gap = db.Gaps.Find(id);
            if (gap == null)
            {
                return HttpNotFound();
            }
            return View(gap);
        }

        // POST: Gaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GapID,gapSummary,gapStartTime,gapEndTime")] Gap gap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gap);
        }

        // GET: Gaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gap gap = db.Gaps.Find(id);
            if (gap == null)
            {
                return HttpNotFound();
            }
            return View(gap);
        }

        // POST: Gaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gap gap = db.Gaps.Find(id);
            db.Gaps.Remove(gap);
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
