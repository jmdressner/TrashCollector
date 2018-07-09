using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var customers = db.Customers.Where(c => c.ApplicationUserID == currentUserId).Include(c => c.ExtraDay).Include(c => c.TrashDay).Include(c => c.Zipcode);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details()
        {
            var currentUserId = User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Where(e => e.ApplicationUserID == currentUserId).Include(c => c.ExtraDay).Include(c => c.TrashDay).Include(c => c.Zipcode).FirstOrDefault();
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.ExtraID = new SelectList(db.ExtraDays, "ID", "extra");
            ViewBag.TrashDayID = new SelectList(db.TrashDays, "ID", "Day");
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Address,ZipcodeID,TrashDayID,PickUpStatus,ExtraID")] Customer customer)
        {
            var currentUserId = User.Identity.GetUserId();
            customer.ApplicationUserID = currentUserId;
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ExtraID = new SelectList(db.ExtraDays, "ID", "extra", customer.ExtraID);
            ViewBag.TrashDayID = new SelectList(db.TrashDays, "ID", "Day", customer.TrashDayID);
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip", customer.ZipcodeID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExtraID = new SelectList(db.ExtraDays, "ID", "extra", customer.ExtraID);
            ViewBag.TrashDayID = new SelectList(db.TrashDays, "ID", "Day", customer.TrashDayID);
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip", customer.ZipcodeID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "ID,Name,Email,Address,ZipcodeID,TrashDayID,PickUpStatus,ExtraID,ApplicationUserID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.ExtraID = new SelectList(db.ExtraDays, "ID", "extra", customer.ExtraID);
            ViewBag.TrashDayID = new SelectList(db.TrashDays, "ID", "Day", customer.TrashDayID);
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip", customer.ZipcodeID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult EditSchedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExtraID = new SelectList(db.ExtraDays, "ID", "extra", customer.ExtraID);
            ViewBag.TrashDayID = new SelectList(db.TrashDays, "ID", "Day", customer.TrashDayID);
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip", customer.ZipcodeID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSchedule([Bind(Include = "ID,Name,Email,Address,ZipcodeID,TrashDayID,PickUpStatus,ExtraID,ApplicationUserID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExtraID = new SelectList(db.ExtraDays, "ID", "extra", customer.ExtraID);
            ViewBag.TrashDayID = new SelectList(db.TrashDays, "ID", "Day", customer.TrashDayID);
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip", customer.ZipcodeID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
