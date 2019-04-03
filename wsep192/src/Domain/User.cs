﻿using System;
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

        public User(int id, string userName, string password, string address, state state, bool isAdmin, bool isRegistered, Dictionary<int, Role> roles)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.address = address;
            this.state = state;
            this.isAdmin = isAdmin;
            this.isRegistered = isRegistered;
            this.basket = new ShoppingBasket();
            if (roles == null)
                this.roles = new Dictionary<int, Role>();
            else
                this.roles = roles;

        }
        public void removeOwner(int userID,int storeID)
        {
            Role role = searchRoleByStoreID(storeID);
            if (role != null&&role.GetType()==typeof(Owner))
                role.Store.removeOwner(userID);
            
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
    }
}
