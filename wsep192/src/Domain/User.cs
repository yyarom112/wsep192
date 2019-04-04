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


        public User()
        {
            this.id = -1;

        }

        public User(int id, string userName, string password, string address, state state, bool isAdmin, bool isRegistered, ShoppingBasket basket, Dictionary<int, Role> roles)
        {
            this.Id = id;
            this.UserName = userName;
            this.Password = password;
            this.Address = address;
            this.State = state;
            this.IsAdmin = isAdmin;
            this.IsRegistered = isRegistered;
            this.Basket = basket;
            if (roles == null)
                this.Roles = new Dictionary<int, Role>();
            else
                this.Roles = roles;

        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public String UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public String Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public String Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

        internal state State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public Boolean IsAdmin
        {
            get
            {
                return isAdmin;
            }

            set
            {
                isAdmin = value;
            }
        }

        public Boolean IsRegistered
        {
            get
            {
                return isRegistered;
            }

            set
            {
                isRegistered = value;
            }
        }

        internal ShoppingBasket Basket
        {
            get
            {
                return basket;
            }

            set
            {
                basket = value;
            }
        }

        internal Dictionary<int, Role> Roles
        {
            get
            {
                return roles;
            }

            set
            {
                roles = value;
            }
        }

        public ShoppingCart addProductsToCart(LinkedList<KeyValuePair<Product, int>> productsToInsert,int storeId)
        {
            return this.basket.addProductsToCart(productsToInsert, storeId);
        }
    }
}
