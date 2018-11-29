using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Assignment2.Controllers;
using Assignment2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace Assignment2.Tests.Controllers
{
    [TestClass]
    public class DepartmentsControllerTest
    {
        //global variables
        DepartmentsController controller;
        Mock<IDepartmentsMock> mock;
        List<Department> Departments;

        [TestInitialize]

        public void TestInitialize()
        {
            //this method runs automatically before each individual test
            mock = new Mock<IDepartmentsMock>();

            //populate Mock List
            Departments = new List<Department>
            {
new Department{Department_ID = 111, Bakery_dept = "Bread", Grocery_dept = "Biscuits", Total_No_Of_Products = 6, Price_of_Product = 9 },
new Department{Department_ID = 222, Bakery_dept = "Hello", Grocery_dept = "Crackers", Total_No_Of_Products = 7, Price_of_Product = 10 },
new Department{Department_ID = 333, Bakery_dept = "Jam", Grocery_dept = "Vegetables", Total_No_Of_Products = 8, Price_of_Product = 11},
            };

            mock.Setup(m => m.Departments).Returns(Departments.AsQueryable());
            controller = new DepartmentsController(mock.Object);
        }

        [TestMethod]
        public void IndexLoadsView()
        {
            //arrange
            // DepartmentsController controller = new DepartmentsController();
            //act
            ViewResult result = controller.Index() as ViewResult;
            //assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnDepartments()
        {
         //act
         var result = (List<Department>)((ViewResult)controller.Index()).Model;
         //assert
         CollectionAssert.AreEqual(Departments, result);
        }

        //GET: Departments/Details/5
        #region
        [TestMethod]
        public void DetailsNoIdLoadsError()
        {
            //check with no id
            //act
            ViewResult result = (ViewResult)controller.Details(null);
            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidIdLoadsError()
        {
            //check with invalid id
            //act
            ViewResult result = (ViewResult)controller.Details(5555);
            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoadsView()
        {
            //check with valid id
            //act
            ViewResult result = (ViewResult)controller.Details(111);
            //assert
            Assert.AreEqual("Details", result.ViewName);

        }

        [TestMethod]
        public void DetailsValidIdLoadsDepartment()
        {
            //check with valid id department
            //act
            Department result = (Department)((ViewResult)controller.Details(222)).Model;
            //assert
            Assert.AreEqual(Departments[1], result);
        }
        #endregion

        // GET: Departments/Create
        #region
        [TestMethod]
        public void CreateViewLoads()
        {
            //act
            ViewResult result = (ViewResult)controller.Create();
            //assert
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        // POST: Departments/Create
        #region
        [TestMethod]
        public void CreateValidDepartment()
        {
            //arrange
            Department newDepartment = new Department
            {
                Department_ID = 666,
            Grocery_dept = "apple",
            Bakery_dept = "banana",
               Total_No_Of_Products = 1,
                Price_of_Product= 99
};
            //act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(newDepartment);
            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateInvalidDepartment()
        {
            //arrange
            Department invalid = new Department();
            //act
            controller.ModelState.AddModelError("cannot create", "exception");
            ViewResult result = (ViewResult)controller.Create(invalid);
            //assert
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        // GET: Departments/Edit/5
        // same thing as details
        #region
            [TestMethod]
        public void EditNoIdLoadsError()
        {
            //act
            ViewResult result = (ViewResult)controller.Edit(3);
            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]

        public void EditInvalidIdLoadsError()
        {
            //check with invalid id
            //act
            ViewResult result = (ViewResult)controller.Edit(888);
            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsView()
        {
            //check with valid id
            //act
            ViewResult result = (ViewResult)controller.Edit(111);
            //assert
            Assert.AreEqual("Edit", result.ViewName);

        }

        [TestMethod]

        public void EditValidIdLoadsDepartment()
        {
            //check with valid id department
            //act
            Department result = (Department)((ViewResult)controller.Edit(111)).Model;
            //assert
            Assert.AreEqual(Departments[0], result);
        }


        #endregion

        // POST: Departments/Edit/5
        #region

        [TestMethod]
        //post cannot be same as other methods due to different functions
        public void EditPostLoadsIndex()
        {
            //act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(Departments[1]);
            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        [TestMethod]
        public void EditPostInvalidLoadView()
        {
            //arrange
            Department invalid = new Department { Department_ID = 33 };

            controller.ModelState.AddModelError("Error", "donot save");

            //act
            ViewResult result = (ViewResult)controller.Edit(invalid);

            //assert
            Assert.AreEqual("Edit", result.ViewName);

        }

        [TestMethod]

        public void EditPostInvalidLoadDepartment()
        {
            //arrange
            Department invalid = new Department { Department_ID = 333 };

            controller.ModelState.AddModelError("Error", "donot save");

            //act
            Department result = (Department)((ViewResult)controller.Edit(invalid)).Model;

            //assert
            Assert.AreEqual(invalid, result);
        }



        #endregion

        // GET: Departments/Delete/5
        //same test methods as details and edit

        #region

        [TestMethod]

        public void DeleteNoIdLoadsError()
        {
            //act
            ViewResult result = (ViewResult)controller.Delete(null);
            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]

        public void DeleteInvalidIdLoadsError()
        {
            //check with invalid id
            //act
            ViewResult result = (ViewResult)controller.Delete(999);
            //assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
       
        public void DeleteValidIdLoadsView()
        {
            // act
            ViewResult result = (ViewResult)controller.Delete(333);
              // assert
            Assert.AreEqual("delete", result.ViewName);
        }

        [TestMethod]

        public void DeleteValidIdLoadsDepartment()
        {
            //check with valid id department
            //act
            Department result = (Department)((ViewResult)controller.Delete(222)).Model;
            //assert
            Assert.AreEqual(Departments[1], result);
        }

        #endregion

        // POST: Departments/Delete/5
        //delete confirmed for invalid id 

        #region 
        [TestMethod]

        public void DeleteConfirmedInvalidId()
        {

            //act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(444);

            //assert
            Assert.AreEqual("Error", result.ViewName);
         }

        [TestMethod]
        public void DeleteConfirmedValidId()
        {

            //act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(222);

            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        #endregion

    }
}
