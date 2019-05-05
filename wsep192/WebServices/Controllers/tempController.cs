using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebService.Controllers
{
    public class tempController : Controller
    {
        public ActionResult hi()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}