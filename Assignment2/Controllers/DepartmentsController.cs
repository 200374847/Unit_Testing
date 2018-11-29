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
    [Authorize(Roles = "Administrator")]
    public class DepartmentsController : Controller
    {
        //we donot need this automatic database connection for mock data
        //private NikunjGroceryStoreModel db = new NikunjGroceryStoreModel();
        private IDepartmentsMock db;
        private Object Departments;
       
        //Default Constructor, use the live db
        public DepartmentsController()
        {
            this.db = new EFDepartments();
        }
        //Mock Constructor
        public DepartmentsController(IDepartmentsMock mock)
        {
            this.db = mock;
        }

        // GET: Departments
        public ActionResult Index()
        {

            var Departments = db.Departments.Include(a => a.Department_ID).Include(a => a.Grocery_dept).Include(a => a.Bakery_dept).Include(a => a.Total_No_Of_Products).Include(a => a.Price_of_Product);

            //return View(db.Departments.ToList());

            return View("Index", Departments.ToList());
        }

        // GET: Departments/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
       {
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //  Department department = db.Departments.Find(id);
            Department Department = db.Departments.SingleOrDefault(a => a.Department_ID == id);
            if (Department == null)
            {
                // return HttpNotFound();
                return View("Error");
            }
            return View("Details", Department);
        }

        // GET: Departments/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Department_ID,Grocery_dept,Bakery_dept,Total_No_Of_Products,Price_of_Product")] Department department)
        {
            if (ModelState.IsValid)
            {
                // db.Departments.Add(department);
                // db.SaveChanges();
                db.Save(department);
                return RedirectToAction("Index");
            }

            return View( "Create", department);
        }

        // GET: Departments/Edit/5
        //SAME thing as done for DETAILS 
        [Authorize(Roles = "Customer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            // Department department = db.Departments.Find(id);
            Department Department = db.Departments.SingleOrDefault(a => a.Department_ID == id);
            if (Department == null)
            {
                //  return HttpNotFound();
                return View("Error");
            }
            return View("Edit", Department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Department_ID,Grocery_dept,Bakery_dept,Total_No_Of_Products,Price_of_Product")] Department department)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(department).State = EntityState.Modified;
                // db.SaveChanges();
                db.Save(department);
                return RedirectToAction("Index");
            }
            return View("Edit", department);
        }

        // GET: Departments/Delete/5
        [Authorize(Roles = "Customer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");  // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //  Department department = db.Departments.Find(id);
            Department Department = db.Departments.SingleOrDefault(a => a.Department_ID == id);
           if (Department == null)
            {
                return View("Error"); //   return HttpNotFound();
            }
            return View("delete", Department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //  Department department = db.Departments.Find(id);
            Department Department = db.Departments.SingleOrDefault(a => a.Department_ID == id);
          //  db.Departments.Remove(department);
          //  db.SaveChanges();
          if(id == null)
            {
                return View("Error");
            }
          if(Department == null)
                {
            
                return View("Error");

                }
            db.delete(Department);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
          //  if (disposing)
            //{
              //  db.Dispose();
            //}
         //   base.Dispose(disposing);
        //}
    }
}
