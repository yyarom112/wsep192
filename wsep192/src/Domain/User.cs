using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    enum state { visitor,signedIn }
    class User
    {
        private int id;
        private String userName;
        private String password;
        private String address;
        private state state;
        private Boolean isAdmin;
        private Boolean isRegistered;
        private ShoppingBasket basket;
        private Dictionary<int, Role> roles;
        private state signedIn;
        private state visitor;

        public User(int id, string userName, string password, bool isAdmin, bool isRegistered)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.address = "";
            this.state = state.visitor;
            this.isAdmin = isAdmin;
            this.isRegistered = isRegistered;
            this.basket = new ShoppingBasket();
            this.roles = new Dictionary<int, Role>();

        }
        public bool removeOwner(int userID,int storeID)
        {
            if (this.state != state.signedIn)
                return false;
            Role role = searchRoleByStoreID(storeID,this.Id);
            if (role != null && role.GetType() == typeof(Owner))
            {
                Owner owner = (Owner)role;
                return owner.removeOwner(userID);
                
            }
            return false;
            
        }
        public virtual Boolean assignManager(User managerUser, int storeId, List<int> permissionToManager)
        {
            if (this.state != state.signedIn || managerUser.state != state.signedIn)
            {
                return false;
            }
            if (this.Roles.ContainsKey(this.id))
            {
                Role role = Roles[this.id];
                if (role != null && role.GetType() == typeof(Owner))
                {
                    Owner owner = (Owner)role;
                    return owner.assignManager(managerUser, permissionToManager);

                }
            }
            return false;
        }

        internal string showCart(int storeId)
        {
            return basket.showCart(storeId);
        }


        public virtual Boolean register(string userName, string password)
        {
            if (userName == null || password == null)
            {
                return false;
            }
            this.userName = userName;
            this.password = password;
            this.IsRegistered = true;
            return true;
        }

        public void addRole(Role role)

        {

            Roles.Add(Id, role);

        }
        public Role searchRoleByStoreID(int storeID,int userID)
        {
            foreach (Role role in roles.Values)
                if (role.Store.Id == storeID&&role.User.Id == userID)
                    return role;
            return null;
        }
        public int Id { get => id; set => id = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Address { get => address; set => address = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsRegistered { get => isRegistered; set => isRegistered = value; }
        internal state State { get => state; set => state = value; }
        internal ShoppingBasket Basket { get => basket; set => basket = value; }
        internal Dictionary<int, Role> Roles { get => roles; set => roles = value; }


        public virtual int basketCheckout(String address)
        {
            this.address = address;
            return basket.basketCheckout() + calcAddressFee(address);
        }
        internal bool signOut()
        {
            if (state != state.signedIn)
                return false;
            state = state.visitor;
            return true;

        }

        public virtual ShoppingCart addProductsToCart(LinkedList<KeyValuePair<Product, int>> productsToInsert, int storeId)
        {
            return this.basket.addProductsToCart(productsToInsert, storeId);
        }
        public virtual Boolean signIn(string userName, string password)
        {
            if (userName != null && password != null)
            {
                this.userName = userName;
                this.password = password;
                this.state = state.signedIn;
                return true;
            }
            return false;
        }
        private int calcAddressFee(string address)
        {
            switch (address)
            {
                case "telaviv":
                    return 50;
                case "beersheva":
                    return 10;
                case "haifa":
                    return 60;
                default:
                    return 40;
            }
        }


        internal bool removeProductsFromCart(List<KeyValuePair<int, int>> productsToRemove, int storeId)
        {
            return basket.removeProductsFromCart(productsToRemove, storeId);
        }
        internal bool editProductQuantityInCart(int productId, int quantity, int storeId)
        {
            return basket.editProductQuantityInCart(productId, quantity, storeId);
        }

        public virtual bool removeManager(int userID, int storeID)
        {
            if (this.state != state.signedIn)
            {
                LogManager.Instance.WriteToLog("User-Remove manager fail- User is not logged in\n");
                return false;
            }
            Role role = searchRoleByStoreID(storeID,userID);
            try
            {
                Owner owner = (Owner)role;
                return owner.removeManager(userID);
            }
            catch (Exception)
            {
                LogManager.Instance.WriteToLog("User-remove manager fail- User " + this.id + " does not have appropriate permissions in Store " + storeID + " .\n");
                return false;
            }

        }
    }
}
