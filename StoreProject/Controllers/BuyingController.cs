using StoreProject.Dal;
using StoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreProject.Controllers
{
    public class BuyingController : Controller
    { 
        ProductDal Productdal;
        CartDal Cartdal;
        // GET: Buying
        public ActionResult Index()
        {
            return View();

        }
            public ActionResult ShoppingCart()
            {
            Session["BuyingP"] = "Remove";
            return View();
            }


        public ActionResult getCartByJSON()
        {

            Cartdal = new CartDal();
            List<Cart> CartList = Cartdal.cart.ToList<Cart>();
            return Json(CartList, JsonRequestBehavior.AllowGet);
        }
        //function add item to cart
        public ActionResult AddToCart()
        {
             CartDal dal = new CartDal();
            Cart cart = new Cart();
            cart.Model = Request.Form["Model"].ToString();
            string mo = Request.Form["Model"].ToString();
            cart.Type = Request.Form["Type"].ToString();
            cart.Price= Convert.ToInt32(Request.Form["Price"]);
            
            
            List<Cart> p = (from x in dal.cart where x.Model.Contains(mo) select x).ToList();
            if (p.Count == 0)
            {
                cart.CartQuantity++;
            dal.cart.Add(cart);

            }
            else if (p.Count == 1)
            {
                var cartv = dal.cart.First(m => m.Model == mo);
                cart.CartQuantity = cartv.CartQuantity;
 

                cart.CartQuantity++;
                var ToDeleteC = dal.cart.First(m => m.Model == mo);
                dal.cart.Remove(ToDeleteC);
                dal.cart.Add(cart);
            }
            
            dal.SaveChanges();
            UpdateMQuantity(cart.Model,"Add");
            return Json(cart, JsonRequestBehavior.AllowGet);
        }

        //return product list
        public ActionResult getProductsByJSON()
        {
            Productdal = new ProductDal();
            List<Product> ProductList = Productdal.product.ToList<Product>();
            return Json(ProductList, JsonRequestBehavior.AllowGet);
        }

        //return cart list
        public ActionResult getCartProductsByJSON()
        {
            Cartdal = new CartDal();
            List<Cart> CartList = Cartdal.cart.ToList<Cart>();
            return Json(CartList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Shop()
        {

            Session["BuyingP"] = "Add";
            return View();
        }



         //function that updates the Quantity of the item in the product database
        public ActionResult UpdateMQuantity(string mo,string Buying)
        {
           
            
                ProductDal dal = new ProductDal();
                Product pro = new Product();
                // mo = Request.Form["Model"].ToString();
                //pro.Quantity = Convert.ToInt32(Request.Form["Quantity"]);

                List<Product> p = (from x in dal.product where x.Model.Contains(mo) select x).ToList();
               // if (ModelState.IsValid && p.Count != 0)
                //{
                 
                    //there  is such model ... upgrade

                    var ToDeleteP = dal.product.First(m => m.Model == mo);
                    pro.Model = ToDeleteP.Model;
                    pro.Type = ToDeleteP.Type;
                    pro.Quantity = ToDeleteP.Quantity;
                    pro.Price = ToDeleteP.Price;
                if (Buying=="Add")
                    {
                        
                        pro.Quantity--;
                    }
                    else if (Buying=="Remove")
                    {
                        pro.Quantity++;
                    }

                    // Delete 
                    dal.product.Remove(ToDeleteP);
                    //add
                    dal.product.Add(pro);
                    dal.SaveChanges();
                    List<Product> prodL1 = dal.product.ToList<Product>();
                    return Json(prodL1, JsonRequestBehavior.AllowGet);
                //}

               // return RedirectToAction("Shop", "Buying");

        }
         
        

        //removes item from cart
        public ActionResult RemoveFromCart()
        {
            //if (((string)Session["type"]).Equals("Employee"))
            
                CartDal dal = new CartDal();
                Cart pro = new Cart();
                pro.Model = Request.Form["Model"].ToString();
                string mo = Request.Form["Model"].ToString();

                List<Cart> p = (from x in dal.cart where x.Model.Contains(mo) select x).ToList();
                if (ModelState.IsValid && p.Count != 0)
                {
                //there  is such model ... delete him
               
                    var ToDelete = dal.cart.First(m => m.Model == pro.Model);
                // Delete 
                pro.Type = ToDelete.Type;
                pro.CartQuantity = ToDelete.CartQuantity;
                pro.Price = ToDelete.Price;
                    dal.cart.Remove(ToDelete);
                    dal.SaveChanges();
                    UpdateMQuantity(mo, "Remove");
                    
                if (pro.CartQuantity > 1)
                {
                    pro.CartQuantity--;
                    dal.cart.Add(pro);
                    dal.SaveChanges();
                }


                List<Cart> cartL1 = dal.cart.ToList<Cart>();
                    return Json(cartL1, JsonRequestBehavior.AllowGet);
                
                }

                else
                    return RedirectToAction("ShoppingCart", "Buying");
            }
            

                //return View("Home");
        


    }
}