
//Michael Moskvitin 321182669
//Christina Tsiviliov 317819027

//
using StoreProject.Dal;
using StoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreProject.Controllers
{
    public class HomeController : Controller
    {
        // Home page 
        public ActionResult Enter()
        {
            return View("Enter");
        }
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Register()
        {
            return View(new Customers());
        }

        public ActionResult SubmitL()
        {

            string Email = Request.Form["Email"];
            string Password = Request.Form["Password"];
            if (Request.Form["EmpRadio"] != null)
            {
                EmployeeDal empDal = new EmployeeDal();
                List<Employee> emp = (from x in empDal.employee where x.EmployeeEmail.Contains(Email) select x).ToList<Employee>();
                //List<Employee> emp = empDal.employee.Where(e => e.EmployeeEmail.Equals(Email)).ToList();
                if (emp.Count == 0)
                {
                    // no such email in database
                    TempData["Error"] = "Incorrect Email";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    //Employee email and password match
                    Employee e = emp[0];
                    if (Password.Equals(e.EmployeePassword))
                    {
                        Session["UserId"] = emp[0].EmployeeId;
                        Session["UserName"] = emp[0].EmployeeName;
                        Session["type"] = "Employee";
                        return View("EmployeeView");
                    }
                } }
            else if(Request.Form["EmpRadio2"] != null)
            {
                if (Request.Form["EmpRadio2"] != null)
                    return View("CustomersView");

                else {
                    ////search for match in Customers DAL
                    Customers temp = new Customers();
                    temp.Email = Request.Form["Email"];
                    temp.CustomerPassword = Request.Form["Password"];

                    CustomersDal custDal = new CustomersDal();
                    List<Customers> cust = custDal.customers.Where(c => c.Email.Equals(temp.Email)).ToList();
                    if (cust.Count == 0)
                    {
                        TempData["fail"] = "Incorrect Input";
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        //found customer email match, try password match
                        Customers c = cust[0];
                        if (c.CustomerPassword.Equals(temp.CustomerPassword))
                        {

                            Session["type"] = "Customer";
                            Session["UserName"] = cust[0].FirstName;
                            return View("CustomersView");
                        }
                        else
                        {
                            // password doesn't match email
                            TempData["fail"] = "Incorrect Detail Input";
                            return RedirectToAction("Login", "Home");
                        }
                    }
                }


            } 

            return View("Enter");

    }



        public ActionResult SubmitR()
        {

            
                string Email = Request.Form["Email"];
                string Password = Request.Form["Password"];
                if (ModelState.IsValid)
                {
                    CustomersDal custDal = new CustomersDal();
                    List<Customers> custo = (from x in custDal.customers where x.Email.Contains(Email) select x).ToList<Customers>();

                    if (custo.Count > 0)
                    {
                        // existing email
                        TempData["Error1"] = "The Email already exist";
                        return RedirectToAction("Register", "Home");
                    }
                    else
                    {
                        Customers cust = new Customers();
                        cust.FirstName = Request.Form["FirstName"];
                        cust.LastName = Request.Form["LastName"];
                        cust.Email = Request.Form["Email"];
                        cust.CustomerPassword = Request.Form["CustomerPassword"];
                        cust.PhoneNumber = Request.Form["PhoneNumber"];
                        custDal = new CustomersDal();
                        custDal.customers.Add(cust);
                        custDal.SaveChanges();

                        return View("Login");
                    }
            }
            else
                return View("Register", new Customers());
        }




        //conroller for about for guest
        public ActionResult Aboutg()
        {
            return View();
        }
        //controller for contact for guest
        public ActionResult Contactg()
        {
            return View();
        }

    }
}