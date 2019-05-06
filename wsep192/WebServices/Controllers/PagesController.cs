using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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
        public ActionResult AddProductInStore()
        {
            return View();
        }
        public ActionResult EditProductInStore()
        {
            return View();
        }


    }
}