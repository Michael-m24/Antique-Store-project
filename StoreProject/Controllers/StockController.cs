using StoreProject.Dal;
using StoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StoreProject.Controllers
{
    public class StockController : Controller
    {
        int id;
        ProductDal productd;

        // GET: Stock for update product
        public ActionResult UpdateStock()
        {
            if (!((string)Session["type"]).Equals("Employee"))//check if employee if not go to home
            {
                return RedirectToAction("Home");
            }
            else
            {
                id = (int)Session["userID"];
            }
            return View();
        }
        // DeleteProduct
        public ActionResult DeleteStock()
        {
            if (!((string)Session["type"]).Equals("Employee"))//check if employee if not go to home
            {
                return RedirectToAction("Home");
            }
            else
            {
                id = (int)Session["userID"];
            }
            return View();


        }

        public ActionResult addStock()
        {
            if (!((string)Session["type"]).Equals("Employee"))//check if employee if not go to home
            {
                return RedirectToAction("Home");
            }
            else
            {
                id = (int)Session["userID"];
            }
            return View();

        }


        public ActionResult GetProductJSON()
        {
            productd = new ProductDal();
            List<Product> prodList = productd.product.ToList<Product>();
            return Json(prodList, JsonRequestBehavior.AllowGet);

        }

        //action for restoke or update the product 
        public ActionResult Submit()
        {
            if (((string)Session["type"]).Equals("Employee"))
            {
                ProductDal dal = new ProductDal();
                Product pro = new Product();
                pro.Model = Request.Form["Model"].ToString();
                string mo = Request.Form["Model"].ToString();
                pro.Quantity = Convert.ToInt32(Request.Form["Quantity"]);
                pro.Type = Request.Form["Type"].ToString();
                pro.Price= Convert.ToInt32(Request.Form["Price"]);

                List<Product> p = (from x in dal.product where x.Model.Contains(mo) select x).ToList();
                if (ModelState.IsValid && p.Count == 0)
                {
                    //there is no such model now we can add new one
                    if (pro.Quantity > 0)//there is only two type of products
                    {
                        dal.product.Add(pro);
                        dal.SaveChanges();
                        List<Product> prodList = dal.product.ToList<Product>();
                        return Json(prodList, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    TempData["Error1"] = "The item already exist";
                     return RedirectToAction("EmployeeView", "Home");
                    //return RedirectToAction("addStock", "Stock");
                }
            }
            else
                return View("Home");

            return View("Home");

        }


        //manger removing exist product from table
        public ActionResult RemoveModel()
        {
            if (((string)Session["type"]).Equals("Employee"))
            {
                ProductDal dal = new ProductDal();
                Product pro = new Product();
                pro.Model = Request.Form["Model"].ToString();
                string mo = Request.Form["Model"].ToString();
                TempData["Empty"] = "Please insert model number";

                List<Product> p = (from x in dal.product where x.Model.Contains(mo) select x).ToList();
                if (ModelState.IsValid && p.Count != 0)
                {
                    //there  is such model ... delete him

                    var ToDelete = dal.product.First(m => m.Model == pro.Model);
                    // Delete 
                    dal.product.Remove(ToDelete);
                    dal.SaveChanges();
                    List<Product> prodL1 = dal.product.ToList<Product>();
                    return Json(prodL1, JsonRequestBehavior.AllowGet);
                }

                else
                    return RedirectToAction("EmployeeView", "Home");
            }
            else

                return View("Home");
        }

        //manager upgrade quantity or name of product
        public ActionResult UpgradeModel()
        {
            if (((string)Session["type"]).Equals("Employee"))
            {
                ProductDal dal = new ProductDal();
                Product pro = new Product();
                pro.Model = Request.Form["Model"].ToString();
                string mo = Request.Form["Model"].ToString();
                pro.Quantity = Convert.ToInt32(Request.Form["Quantity"]);
                pro.Type = Request.Form["Type"].ToString();
                pro.Price = Convert.ToInt32(Request.Form["Price"]);
                List<Product> p = (from x in dal.product where x.Model.Contains(mo) select x).ToList();
                if (ModelState.IsValid && p.Count != 0)
                {
                    //there  is such model ... upgrade

                    var ToDelete = dal.product.First(m => m.Model == pro.Model);

                    // Delete 
                    dal.product.Remove(ToDelete);
                    //add
                    dal.product.Add(pro);
                    dal.SaveChanges();
                    List<Product> prodL1 = dal.product.ToList<Product>();
                    return Json(prodL1, JsonRequestBehavior.AllowGet);
                }

                return RedirectToAction("EmployeeView", "Home");

            }
            return View("Home");
        }

        //logout and return to home page
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Enter", "Home");
        }



    }

}