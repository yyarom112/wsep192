
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using src.ServiceLayer;
using System.Text;
using System.Threading.Tasks;

namespace src.ServiceLayer
{
    public class SystemState
    {

        internal static bool fileSetUp()
        {
            string user = "";
            bool flag = true;
            ServiceLayer service = ServiceLayer.getInstance();
            // Open the file to read from.
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\src\\State.json");
            StreamReader sr=null;
            try {
                sr = new StreamReader(path);
            }
            catch (Exception e) {
                ErrorManager.Instance.WriteToLog("SystemState - fileSetUp - File not exist");
                return false;
            }
            using (sr)
            {

                string json = sr.ReadToEnd();
                State state = JsonConvert.DeserializeObject<State>(json);
                foreach (Request r in state.Requests)
                {
                    switch (r.RequestName)
                    {
                        case "Init":
                            flag = flag & service.init(r.User, r.Password);
                            break;
                        case "InitUser":
                            user = service.initUser();
                            break;
                        case "Login":
                            flag = flag & service.signIn(r.User, r.Password);
                            break;
                        case "Register":
                            flag = flag & service.register(r.User, r.Password, user);
                            break;
                        case "Logout":
                            if (r.User != null)
                                flag = flag & service.signOut(r.User);
                            else return false;
                            break;
                        case "AddProductsToCart":
                            if (r.User != null)
                                flag = flag & service.addProductsToCart(r.Products, r.Store, r.User);
                            else
                                flag = flag & service.addProductsToCart(r.Products, r.Store, user);
                            break;
                        case "EditProductQuantityInCart":
                            if (r.User != null)
                                flag = flag & service.editProductQuantityInCart(r.Product, r.Quantity, r.Store, r.User);
                            else
                                flag = flag & service.editProductQuantityInCart(r.Product, r.Quantity, r.Store, user);
                            break;
                        case "RemoveProductsFromCart":
                            if (r.User != null)
                                flag = flag & service.removeProductsFromCart(r.ProductsToRemove, r.Store, r.User);
                            else
                                flag = flag & service.removeProductsFromCart(r.ProductsToRemove, r.Store, user);
                            break;
                        case "OpenStore":
                            if (r.User != null)
                                flag = flag & service.openStore(r.Store, r.User);
                            else return false;
                            break;
                        case "CreateNewProductInStore":
                            if (r.User != null)
                                flag = flag & service.createNewProductInStore(r.ProductName, r.Category, r.Details, r.Price, r.Store, r.User);
                            else return false;
                            break;
                        case "AddProductsInStore":
                            if (r.User != null)
                                flag = flag & service.addProductsInStore(r.Products, r.Store, r.User);
                            else return false;
                            break;
                        case "RemoveProductsInStore":
                            if (r.User != null)
                                flag = flag & service.removeProductsInStore(r.Products, r.Store, r.User);
                            else return false;
                            break;
                        case "EditProductInStore":
                            if (r.User != null)
                                flag = flag & service.editProductInStore(r.Product, r.ProductName, r.Category, r.Details, r.Price, r.Store, r.User);
                            else return false;
                            break;
                        case "AssignOwner":
                            if (r.User != null)
                                flag = flag & service.assignOwnerSetUp(r.User, r.Owner, r.Store);
                            else return false;
                            break;
                        case "RemoveOwner":
                            if (r.User != null)
                                flag = flag & service.removeOwner(r.Owner, r.Store, r.User);
                            else return false;
                            break;
                        case "AssignManager":
                            if (r.User != null)
                                flag = flag & service.assignManager(r.Manager, r.Store, r.Permissions, r.User);
                            else return false;
                            break;
                        case "RemoveManager":
                            if (r.User != null)
                                flag = flag & service.removeOwner(r.Manager, r.Store, r.User);
                            else return false;
                            break;
                        case "RemoveUser":
                            if (r.User != null)
                                flag = flag & service.removeUser(r.UserToRemove, r.User);
                            else return false;
                            break;
                        default:
                            return false;
                    }
                }
            }
            return flag;
        }

        public static void fileCreation(State state)
         {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\src\\State.json");

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(sw, state);
                }
            }
            else {

                string json = JsonConvert.SerializeObject(state);
                File.WriteAllText(path,json);
            }
        }

    }

    public class State
    {
        public List<Request> Requests { get; set; }
    }

    public class Request
    {
        public string RequestName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public List<KeyValuePair<String, int>> Products { get; set; }
        public string Store { get; set; }
        public string Product { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public List<string> ProductsToRemove { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public int Price { get; set; }
        public string Owner { get; set; }
        public string Manager { get; set; }
        public List<string> Permissions { get; set; }
        public string UserToRemove { get; set; }
    }


}
