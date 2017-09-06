using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MindTheGap.Models;
using Newtonsoft.Json;
using System;

namespace MindTheGap.Controllers
{
    public class EventsController : Controller
    {
        private MindTheGapEntities db = new MindTheGapEntities();
        public string EventSummary { get; set; }
        public string EventLocation { get; set; }
        public string EventStart { get; set; }
        public string EventEnd { get; set; }
        public string EventDescription { get; set; }
        public string EventRecurrence { get; set; }
        public bool EventReminders { get; set; }
        public string EventColor { get; set; }

        //START OF TEST
        [HttpPost]
        public JsonResult GetUserEventInfo(EventsController events)
        {
            string summary = events.EventSummary;
            string location = events.EventLocation;
            string description = events.EventDescription;
            string startTime = events.EventStart;
            string endTime = events.EventEnd;
            string recurrence = events.EventRecurrence;
            bool reminders = events.EventReminders;
            string color = events.EventColor;
            string AllInfo;

            //Event newEvent = new Event();
            //newEvent.summary = summary;
            //newEvent.location = location;
            //newEvent.description = description;
            //newEvent.starttime = DateTime.TryParseExact(startTime,);
            //newEvent.endtime = endTime;
            //newEvent.recurrence = recurrence;
            //newEvent.reminders = reminders;
            //newEvent.colorId = color;
            //db.Events.Add(newEvent);
            //db.SaveChanges();

            if (startTime == "All Day")
            {
                AllInfo = summary + " is an All Day Event";
            }
            else
            {
                AllInfo = summary + " @ " + location + " From " + startTime + " to " + endTime;
            }

            return new JsonResult()
            {
                Data = JsonConvert.SerializeObject(AllInfo
                ),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            //return Json(summary, JsonRequestBehavior.AllowGet);
        }
        //END OF TEST


        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events.Include(e => e.Interest).Include(e => e.AspNetUser);
            return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.interestId = new SelectList(db.Interests, "interestId", "userId");
            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "eventId,userId,interestId,summary,description,location,colorId,starttime,endtime,recurrence,reminders,completed")] Event @event)
        {
            if (ModelState.IsValid)
            {
                
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.interestId = new SelectList(db.Interests, "interestId", "userId", @event.interestId);
            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email", @event.userId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.interestId = new SelectList(db.Interests, "interestId", "userId", @event.interestId);
            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email", @event.userId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "eventId,userId,interestId,summary,description,location,colorId,starttime,endtime,recurrence,reminders,completed")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.interestId = new SelectList(db.Interests, "interestId", "userId", @event.interestId);
            ViewBag.userId = new SelectList(db.AspNetUsers, "Id", "Email", @event.userId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
