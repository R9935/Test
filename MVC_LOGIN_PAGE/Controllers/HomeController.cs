using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_LOGIN_PAGE.Models;
using System.Web.Security;

namespace MVC_LOGIN_PAGE.Controllers
{ 
    [Authorize]
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            SSEntities2 obj = new SSEntities2();
            List<AddEmp> emp = new List<AddEmp>();
            var v = obj.Employees.ToList();
            foreach(var item in v)
            {
                emp.Add(new AddEmp
                {
                    E_ID=item.E_ID,
                    E_Name=item.E_Name,
                    E_Company=item.E_Company,
                    E_Dept=item.E_Dept,
                    E_Email_Id=item.E_Email_Id,
                    E_Salary=item.E_Salary,
                    password=item.password,
                    UserName=item.UserName
                });
            }

            return View(emp);
        }


        
        [HttpGet]
       [AllowAnonymous]
        public ActionResult Index1()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index1(AddEmp emp)
        
        
        
        {
            SSEntities2 obj = new SSEntities2();
            Employee Emp = new Employee();
            Emp.E_ID = emp.E_ID;
            Emp.E_Name = emp.E_Name;
            Emp.E_Email_Id = emp.E_Email_Id;
            Emp.E_Salary = emp.E_Salary;
            Emp.password = emp.password;
            Emp.UserName = emp.UserName;
            Emp.E_Dept = emp.E_Dept;
            Emp.E_Company = emp.E_Company;

            if (emp.E_ID == 0)
            {
                obj.Employees.Add(Emp);
                obj.SaveChanges();
               
            }
            else
            {
                obj.Entry(Emp).State = System.Data.Entity.EntityState.Modified;
                obj.SaveChanges();
            }



            return RedirectToAction("Index");
        }

        public ActionResult Edit(int E_ID)
        {
            SSEntities2 obj = new SSEntities2();
            AddEmp emp = new AddEmp();
            var v = obj.Employees.Where(x => x.E_ID == E_ID).First();
            emp.E_ID = v.E_ID;
            emp.E_Name = v.E_Name;
            emp.E_Email_Id = v.E_Email_Id;
            emp.E_Salary = v.E_Salary;
            emp.password = v.password;
            emp.UserName = v.UserName;
            emp.E_Company = v.E_Company;
            emp.E_Dept = v.E_Dept;


            ViewBag.password = "PassWord";
            ViewBag.update = "Update";


            return View("Index1", emp);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }




            [HttpPost]
            [AllowAnonymous]
        public ActionResult Login(AddEmp emp)
        {
            SSEntities2 obj = new SSEntities2();
            var v = obj.Employees.Where(m => m.UserName == emp.UserName).FirstOrDefault();
            if (v == null)
            {
                TempData["Email Invalid "] = "UserName not found";
            }
            else
            {
                if (v.UserName == emp.UserName && v.password == emp.password)
                {
                    FormsAuthentication.SetAuthCookie(emp.UserName, false);
                    Session["email"] = v.UserName;
                    Session["Password"] = v.password;
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["WrongPassword"] = "Password Inccorect";

                    return View();
                }

              
            }
            Session["q"] = emp.E_Name;
            return RedirectToAction("DashBoard");


        }


        public ActionResult DashBoard()
        {
            return View();
        }

      
        public ActionResult LogOut()
        {
            Session["email"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }



        public ActionResult Delete(int E_ID)
        {

            SSEntities2 obj = new SSEntities2();
            var v = obj.Employees.Where(x => x.E_ID == E_ID).First();
            obj.Employees.Remove(v);
            obj.SaveChanges();

            return RedirectToAction("Index");



        }
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return RedirectToAction("Index1");
        }

     
    }
}