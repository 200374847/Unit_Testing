using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class EFDepartments : IDepartmentsMock
    {
        //connect to db
        private NikunjGroceryStoreModel db = new NikunjGroceryStoreModel();
        public IQueryable<Department> Departments { get { return db.Departments; } }

        public void delete(Department Department)
        {
            db.Departments.Remove(Department);
            db.SaveChanges();
        }

        public Department Save(Department Department)
        {
            if(Department.Department_ID == 0)
            {
                db.Departments.Add(Department);
            }
            else
            {
                db.Entry(Department).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return Department;
        }
    }
}