using src.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;



namespace WebService.Controllers
{
    
    public class UserController : ApiController
    {
        ServiceLayer service = new ServiceLayer();
        
        [Route("api/user/register")]
        [HttpGet]
        public string register(String Username, String Password)
        {
            service.init("admin","admin");
            String user = service.initUser();
            bool ans = service.register(Username, Password, user);
            //User session = hashServices.getUserByHash(System.Web.HttpContext.Current.Request.Cookies["HashCode"].Value);
            //int ans = userServices.getInstance().register(session, Username, Password);
            switch (ans)
            {
                case true:
                    return "user successfuly added";
                case false:
                    return "error";
            }
            return "server error: not suppose to happend";
        }

    }
}