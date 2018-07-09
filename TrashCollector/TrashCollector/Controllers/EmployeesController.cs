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
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Zipcode);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            var currentUserId = User.Identity.GetUserId();
            if (currentUserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Where(e => e.ApplicationUserID == currentUserId).Include(e => e.Zipcode).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,ZipcodeID")] Employee employee)
        {
            var currentUserId = User.Identity.GetUserId();
            employee.ApplicationUserID = currentUserId;
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip", employee.ZipcodeID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip", employee.ZipcodeID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,ZipcodeID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip", employee.ZipcodeID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

        [HttpGet]
        public ActionResult CreateSchedule()
        {
            ViewBag.TrashDayID = new SelectList(db.TrashDays, "ID", "Day");
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSchedule(int? ZipcodeID, int? TrashDayID)
        {
            if (ZipcodeID == null || TrashDayID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var foundCustomers = db.Customers.Where(c => c.ZipcodeID == ZipcodeID && c.TrashDayID == TrashDayID).Include(c => c.ExtraDay).Include(c => c.TrashDay).Include(c => c.Zipcode);

            if (foundCustomers == null)
            {
                return HttpNotFound();
            }
            return View("WorkSchedule", foundCustomers.ToList());
        }

        public ActionResult EditStatus(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStatus([Bind(Include = "ID,Name,Email,Address,ZipcodeID,TrashDayID,PickUpStatus,ExtraID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var newStatus = db.Customers.Single(s => s.ID == customer.ID);
                newStatus.PickUpStatus = customer.PickUpStatus;
                db.SaveChanges();
                return RedirectToAction("WorkSchedule");
            }
            ViewBag.ExtraID = new SelectList(db.ExtraDays, "ID", "extra", customer.ExtraID);
            ViewBag.TrashDayID = new SelectList(db.TrashDays, "ID", "Day", customer.TrashDayID);
            ViewBag.ZipcodeID = new SelectList(db.Zipcodes, "ID", "Zip", customer.ZipcodeID);
            return View(customer);
        }
        
        public ActionResult WorkSchedule()
        {
            var customers = db.Customers.Include(c => c.ExtraDay).Include(c => c.TrashDay).Include(c => c.Zipcode);
            return View(customers.ToList());
        }
    }
}
