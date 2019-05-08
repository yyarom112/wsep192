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
            string res = listToString(service.showCart(Store, User));
            return res;
        }
        private String listToString(List<KeyValuePair<string, int>> list)
        {
            String str = "";
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i].Key + "," + list[i].Value.ToString() + ",";
            }
            return str;
        }

        [Route("api/user/RemoveFromCart")]
        [HttpGet]
        public string removeFromCart(String list, String store, String user)
        {
            List<string> l = toList(list);
            bool res = service.removeProductsFromCart(l,store, user);
            switch (res)
            {
                case true:
                    return "true";
                case false:
                    return "false";
            }
            return "Server error: removeFromCart";
            
        }


        private List<String> toList(string products)
        {
            List<String> list;
            if (products != null)
                list = new List<String>(products.Split(','));
            else list = new List<String>();
            list.Remove("");
            return list;
        }

        [Route("api/user/EditCart")]
        [HttpGet]
        public string editCart(String Store, String User)
        {
            //string res = service.showCart(Store, User);
            //return res;
            return null;
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
        [Route("api/user/assignOwner")]
        [HttpGet]
        public string assignOwner(String ownerName, String userToAssign,String storeName)
        {
            bool ans = service.assignOwner(ownerName, userToAssign,storeName);
            switch (ans)
            {
                case true:
                    return "User successfuly was assigned";
                case false:
                    return "Error in assigning owner";
            }
            return "Server error: assignOwner";
        }
        [Route("api/user/removeOwner")]
        [HttpGet]
        public string removeOwner(String ownerToRemove, String storeName,String ownerName)
        {
            bool ans = service.removeOwner(ownerToRemove, storeName,ownerName);
            switch (ans)
            {
                case true:
                    return "Owner successfuly was removed";
                case false:
                    return "Error in removing owner";
            }
            return "Server error: removeOwner";
        }
        [Route("api/user/assignManager")]
        [HttpGet]
        public string assignManager(String ownerName, String userToAssign, String storeName,String permissions)
        {
            List<String> perms= permissions.Split(' ').ToList();
            bool ans = service.assignManager(userToAssign, storeName,perms, ownerName);
            switch (ans)
            {
                case true:
                    return "Manager successfuly was assigned";
                case false:
                    return "Error in assining manager";
            }
            return "Server error: assignManager";
        }

        [Route("api/user/setUp")]
        [HttpGet]
        public string setUp()
        {
            bool ans = service.setUp();
            switch (ans)
            {
                case true:
                    return "System setup completed";
                case false:
                    return "Error in setUp";
            }
            return "Server error: setUp";
        }


        [Route("api/user/RemoveUser")]
        [HttpGet]
        public string removeUser(String Username, String RemoveUser)
        {
            bool ans = service.removeUser(RemoveUser, Username);
            switch (ans)
            {
                case true:
                    return "User successfuly removed";
                case false:
                    return "Error in remove user";
            }
            return "Server error: removeUser";
        }
        [Route("api/user/RemoveManager")]
        [HttpGet]
        public string removeManager(String managerToRemove, String storeName,String ownerName)
        {
            bool ans = service.removeManager(managerToRemove, storeName, ownerName);
            switch (ans)
            {
                case true:
                    return "Manager successfuly was removed";
                case false:
                    return "Error in remove user";
            }
            return "Server error: removeUser";
        }



    }
}