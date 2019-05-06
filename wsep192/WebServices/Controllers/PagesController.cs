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

    }
}