using src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.DataLayer
{
    class DBtransactions
    {
        private DBmanager db;
        private static DBtransactions instance = null;

        internal DBmanager Db { get => db; set => db = value; }

        private DBtransactions(bool isTest)
        {
            this.Db = new DBmanager();
            Db.IsTest = isTest;
        }


        public static DBtransactions getInstance(bool isTest)
        {
            if (instance == null)
            {
                instance = new DBtransactions(isTest);
            }
            return instance;
        }

        public void isTest(bool isTest)
        {
            Db.IsTest = isTest;
        }


        public bool registerNewUserDB(User user)
        {
            var session = Db.Client.StartSession();
            session.StartTransaction();
            try
            {
                if (!Db.addNewUser(user))
                {
                    session.AbortTransaction();
                    return false;
                }
                foreach (ShoppingCart cart in user.Basket.ShoppingCarts.Values)
                {
                    foreach (ProductInCart product in cart.Products.Values)
                    {
                        if (!Db.addProductInCart(product, user.Id))
                        {
                            session.AbortTransaction();
                            return false;
                        }
                    }
                }

                foreach (Role role in user.Roles.Values)
                {
                    if (role != null && role.GetType() == typeof(Manager))
                    {
                        if (!Db.addNewManager((Manager)role))
                        {
                            session.AbortTransaction();
                            return false;
                        }
                    }

                    else
                    {
                        if (!Db.addNewOwner((Owner)role))
                        {
                            session.AbortTransaction();
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                session.AbortTransaction();
                ErrorManager.Instance.WriteToLog("User-registerNewUser- Add new user failed - " + e + " .");
                return false;
            }
            return true;
        }
    }
}
