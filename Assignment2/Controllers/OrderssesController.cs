using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    
    public class OrderssesController : Controller
    {
        private NikunjGroceryStoreModel db = new NikunjGroceryStoreModel();

        // GET: Ordersses
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var ordersses = db.Ordersses.Include(o => o.Department);
            return View(ordersses.ToList());
        }

        // GET: Ordersses/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderss orderss = db.Ordersses.Find(id);
            if (orderss == null)
            {
                return HttpNotFound();
            }
            return View(orderss);
        }

        // GET: Ordersses/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            ViewBag.Department_ID = new SelectList(db.Departments, "Department_ID", "Grocery_dept");
            return View();
        }

        // POST: Ordersses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Order_ID,Order_Number,Person_Name,Department_ID")] Orderss orderss)
        {
            if (ModelState.IsValid)
            {
                db.Ordersses.Add(orderss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Department_ID = new SelectList(db.Departments, "Department_ID", "Grocery_dept", orderss.Department_ID);
            return View(orderss);
        }

        // GET: Ordersses/Edit/5
        [Authorize(Roles = "Customer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderss orderss = db.Ordersses.Find(id);
            if (orderss == null)
            {
                return HttpNotFound();
            }
            ViewBag.Department_ID = new SelectList(db.Departments, "Department_ID", "Grocery_dept", orderss.Department_ID);
            return View(orderss);
        }

        // POST: Ordersses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_ID,Order_Number,Person_Name,Department_ID")] Orderss orderss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department_ID = new SelectList(db.Departments, "Department_ID", "Grocery_dept", orderss.Department_ID);
            return View(orderss);
        }

        // GET: Ordersses/Delete/5
        [Authorize(Roles = "Customer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orderss orderss = db.Ordersses.Find(id);
            if (orderss == null)
            {
                return HttpNotFound();
            }
            return View(orderss);
        }

        // POST: Ordersses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orderss orderss = db.Ordersses.Find(id);
            db.Ordersses.Remove(orderss);
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
