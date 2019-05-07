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
        ServiceLayer service = ServiceLayer.getInstance();
        [Route("api/user/RegisterUser")]
        [HttpGet]
        public string register(String Username, String Password)
        {
            string user = service.initUser();
            bool ans = service.register(Username, Password, user);

            switch (ans)
            {
                case true:
                    return "User successfuly registered";
                case false:
                    return "Error in register";
            }
            return "server error: RegisterUser";
        }



        [Route("api/user/ShoppingCart")]
        [HttpGet]
        public string showCart(String Store, String User)
        {
            string res = service.showCart(Store, User);
            return res;
        }







        [Route("api/user/LoginUser")]
        [HttpGet]
        public string login(String Username, String Password)
        {
            bool ans = service.signIn(Username, Password);
            switch (ans)
            {
                case true:
                    return "User successfuly logged in";
                case false:
                    return "Error in login";
            }
            return "Server error: LoginUser";
        }

        [Route("api/user/LogOutUser")]
        [HttpGet]
        public string logout(String Username)
        {
            bool ans = service.signOut(Username);
            switch (ans)
            {
                case true:
                    return "User successfuly logged out";
                case false:
                    return "Error in logging out";
            }
            return "Server error: logOutUser";
        }

        [Route("api/user/generateUserID")]
        [HttpGet]
        public Object generateUserID()
        {
            return service.initUser();
        }

        [Route("api/user/OpenStore")]
        [HttpGet]
        public string openStore(String Username,String StoreName)
        {
            bool ans = service.openStore(StoreName,Username);
            switch (ans)
            {
                case true:
                    return "Store created successfully";
                case false:
                    return "Error in creating store";
            }
            return "Server error: openStore";
        }


    }
}