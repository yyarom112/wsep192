using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using src.ServiceLayer;

namespace WebServices.Controllers
{

    public class PagesController : Controller
    {
        // GET: Pages
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        public ActionResult LoginUser()
        {
            return View();
        }
        public ActionResult AssignOwner()
        {
            return View();
        }
        public ActionResult AssignManager()
        {
            return View();
        }
        public ActionResult RemoveManager()
        {
            return View();
        }
        public ActionResult RemoveOwner()
        {
            return View();
        }
        public ActionResult OpenStore()
        {
            return View();
        }
        public ActionResult AddProductInStore()
        {
            return View();
        }
        public ActionResult EditProductInStore()
        {
            return View();
        }
        public ActionResult CreateProductInStore()
        {
            return View();
        }
        public ActionResult RemoveProductInStore()
        {
            return View();
        }
        public ActionResult SearchProduct()
        {
            return View();
        }

        public ActionResult SetUp()
        {
            return View();
        }
        public ActionResult ShowProduct()
        {
            return View();
        }
        public ActionResult RemoveUser()
        {
            return View();
        }
        public ActionResult CheckoutBasket()
        {
            return View();
        }


        public ActionResult ShoppingCart()
        {
            return View();
        }


        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult AddPurchasePolicy()
        {
            return View();
        }
        public ActionResult AddDiscountPolicy()
        {
            return View();
        }

    }

}