using System;
using System.Collections.Generic;
using System.Text;
using src.ServiceLayer;

namespace InputCreation
{
    public class StateCreator
    {

        internal static State createState()
        {
            List<Request> requests = new List<Request>();
            requests.Add(new Request{RequestName="Init", User = "admin", Password = "admin" });
            //first user
            requests.Add(new Request { RequestName="InitUser" });
            requests.Add(new Request{ RequestName = "Register", User="Aviv" , Password= "Aviv" });
            requests.Add(new Request { RequestName = "InitUser" });
            requests.Add(new Request { RequestName = "Register", User = "Maor", Password = "Maor" });
            requests.Add(new Request { RequestName = "InitUser" });
            //requests.Add(new Request { RequestName = "Register", User = "Seifan", Password = "Seifan" });
            requests.Add(new Request { RequestName = "InitUser" });
            requests.Add(new Request { RequestName = "Register", User = "Rotem", Password = "Rotem" });
            requests.Add(new Request { RequestName = "InitUser" });
            requests.Add(new Request { RequestName = "Register", User = "Yuval", Password = "Yuval" });
            requests.Add(new Request { RequestName = "InitUser" });
            requests.Add(new Request { RequestName = "Register", User = "Gal", Password = "Gal" });
            
            requests.Add(new Request { RequestName = "Login", User = "Aviv", Password="Aviv"});
            requests.Add(new Request { RequestName = "OpenStore", Store= "Zara", User = "Aviv"});
            requests.Add(new Request { RequestName = "CreateNewProductInStore", ProductName= "Top", Category="Tops", Details = "New!" , Price=120 , Store="Zara", User="Aviv"});
            List<KeyValuePair<string, int>> products = new List<KeyValuePair<string, int>>();
            products.Add(new KeyValuePair<string, int>("Top",30));
            requests.Add(new Request { RequestName = "AddProductsInStore", Store="Zara" , Products=products, User = "Aviv" });
            List<string> permissions = new List<string>();
            permissions.Add("AddProductsInStore");
            permissions.Add("RemoveProductsInStore");
            requests.Add(new Request { RequestName = "AssignManager", Store="Zara" , Manager="Yuval" , Permissions=permissions , User="Aviv" } );
            requests.Add(new Request { RequestName = "AssignOwner", Store = "Zara", Owner = "Maor", User = "Aviv" });
            //requests.Add(new Request { RequestName = "AssignOwner", Store = "Zara", Owner = "Seifan",  User = "Aviv" });
            requests.Add(new Request { RequestName = "AssignOwner", Store = "Zara", Owner = "Rotem",  User = "Aviv" });
            requests.Add(new Request { RequestName = "Logout", User = "Aviv" });
            State state = new State();
            state.Requests = requests;
            return state;
        }

    }
}
