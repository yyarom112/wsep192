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

        public ActionResult ShoppingCart()
        {
            return View();
        }

    }
    /*
    public class CheckAdmin : ActionFilterAttribute
    {
        ServiceLayer service = ServiceLayer.getInstance();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] != null)
            {

                String hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
                if (hash == null)
                {
                    filterContext.Result = new RedirectResult(string.Format("/error/"));
                }
            }
        }
    }

    public class CheckLoggedIn : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["HashCode"] != null)
            {
                String hash = System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value;
                if (hash == null )//|| hashServices.getUserByHash(hash) == null || !hashServices.getUserByHash(hash).getState().isLogedIn())
                {
                    filterContext.Result = new RedirectResult(string.Format("/error/"));
                }
            }
            else
                filterContext.Result = new RedirectResult(string.Format("/error/"));
        }
    }*/

}