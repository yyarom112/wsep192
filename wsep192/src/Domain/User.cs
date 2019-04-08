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
            Role role = searchRoleByStoreID(storeID);
            if (role != null && role.GetType() == typeof(Owner))
            {
                Owner owner = (Owner)role;
                return owner.removeOwner(userID);
                
            }
            return false;
            
        }
        public Role searchRoleByStoreID(int storeID)
        {
            foreach (Role role in roles.Values)
                if (role.Store.Id == storeID)
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

        internal bool signOut()
        {
            if (state != state.signedIn)
                return false;
            state = state.visitor;
            return true;

        }

        public ShoppingCart addProductsToCart(LinkedList<KeyValuePair<Product, int>> productsToInsert, int storeId)
        {
            return this.basket.addProductsToCart(productsToInsert, storeId);
        }
        public Boolean signIn(string userName, string password)
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

        public Boolean register(string userName, string password)
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
    }
}
