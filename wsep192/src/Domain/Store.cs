using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Common;

namespace src.Domain
{
    class Store
    {
        private int id;
        private String name;
        private Dictionary<int, ProductInStore> products;
        private int storeRate;
        private TreeNode<Role> roles;
        private Dictionary<int, TreeNode<Role>> rolesDictionary;
        private List<PurchasePolicy> purchasePolicy;
        private List<DiscountPolicy> discountPolicy;

        public Store(int id, string name, List<PurchasePolicy> purchasePolicy, List<DiscountPolicy> discountPolicy)
        {
            this.id = id;
            this.name = name;
            this.products = new Dictionary<int, ProductInStore>();
            this.storeRate = 0;
            this.roles = new TreeNode<Role>(null);
            this.rolesDictionary = new Dictionary<int, TreeNode<Role>>();
            this.purchasePolicy = purchasePolicy;
            this.discountPolicy = discountPolicy;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int StoreRate { get => storeRate; set => storeRate = value; }
        internal Dictionary<int, ProductInStore> Products { get => products; set => products = value; }
        internal TreeNode<Role> Roles { get => roles; set => roles = value; }
        internal List<PurchasePolicy> PurchasePolicy { get => purchasePolicy; set => purchasePolicy = value; }
        internal List<DiscountPolicy> DiscountPolicy { get => discountPolicy; set => discountPolicy = value; }
        internal Dictionary<int, TreeNode<Role>> RolesDictionary { get => rolesDictionary; set => rolesDictionary = value; }

        public bool searchProduct(Filter filter, List<ProductInStore> listToAdd)
        {
            bool result = false;
            foreach (ProductInStore p in products.Values)
            {
                if (p.Product.compareProduct(filter) && filter.StoreRate != -1 && filter.StoreRate == this.storeRate)
                {
                    listToAdd.Add(p);
                    result = true;
                }
            }
            return result;
        }

        public Role initOwner(User user)
        {
            Owner owner = new Owner(this, user);
            roles.setData(owner);
            RolesDictionary.Add(user.Id, roles);
            user.addRole(owner);
            return owner;
        }

        public virtual Boolean assignManager(Role newManager, Owner owner)
        {

            TreeNode<Role> currOwner = RolesDictionary[owner.User.Id];
            if (currOwner != null)
            {
                if (!RolesDictionary.ContainsKey(newManager.User.Id))
                {

                    TreeNode<Role> managerRole = currOwner.AddChild(newManager);
                    RolesDictionary.Add(newManager.User.Id, managerRole);
                    newManager.User.Roles.Add(this.Id, newManager);
                    LogManager.Instance.WriteToLog("Store - assign manger succeed");
                    return true;
                }
                LogManager.Instance.WriteToLog("Store - assign manger fail - new manager already exist in the store");
            }
            LogManager.Instance.WriteToLog("Store - assign manger fail - owner not exist in the tree");
            return false;
        }

        public virtual void updateCart(ShoppingCart cart, String opt)
        {
            foreach (ProductInCart p in cart.Products.Values)
            {
                if (opt.Equals("-"))
                {
                    if (p.Quantity <= this.products[p.Product.Id].Quantity)
                        if (opt.Equals("-"))
                            this.products[p.Product.Id].Quantity -= p.Quantity;

                        else
                        {
                            p.Quantity = this.products[p.Product.Id].Quantity;
                            this.products[p.Product.Id].Quantity = 0;
                        }
                }
                else
                {
                    this.products[p.Product.Id].Quantity += p.Quantity;
                }

            }
        }
        public virtual bool confirmPurchasePolicy(Dictionary<int, ProductInCart> products)
        {
            if (this.PurchasePolicy == null)
                return true;
            List<ProductInStore> productsInStore = new List<ProductInStore>();
            foreach (ProductInCart p in products.Values)
            {
                ProductInStore productInStore = new ProductInStore(p.Quantity, this, p.Product);
                productsInStore.Add(productInStore);
            }
            foreach (PurchasePolicy pp in purchasePolicy)
            {
                if (!pp.confirmPolicy())
                    return false;
            }
            return true;

        }
        public void updateCart(ShoppingCart cart)
        {
            foreach (ProductInCart p in cart.Products.Values)
            {
                if (p.Quantity <= this.products[p.Product.Id].Quantity)
                    this.products[p.Product.Id].Quantity -= p.Quantity;
                else
                {
                    p.Quantity = this.products[p.Product.Id].Quantity;
                    this.products[p.Product.Id].Quantity = 0;
                }
            }
        }

        public virtual int calculateDiscountPolicy(Dictionary<int, ProductInCart> products)
        {
            if (this.DiscountPolicy == null)
                return 0;
            int sum = 0;
            List<ProductInStore> productsInStore = new List<ProductInStore>();
            foreach (ProductInCart p in products.Values)
            {
                ProductInStore productInStore = new ProductInStore(p.Quantity, this, p.Product);
                productsInStore.Add(productInStore);
            }
            foreach (DiscountPolicy dp in discountPolicy)
            {
                sum += dp.calculate(productsInStore);
            }
            return sum;
        }

        public bool removeOwner(int userID,Role owner)
        {
            TreeNode<Role> ownerNode = RolesDictionary[owner.User.Id];
            TreeNode<Role> roleNode = null;
            bool flag = false;
            
            if (RolesDictionary.ContainsKey(this.id))
                roleNode = RolesDictionary[userID];
            if (roleNode != null&&roleNode.Parent!=null)
            {
                if (roleNode.Data.GetType() == typeof(Owner))
                {
                    if (roleNode.getChildren() == null || roleNode.getChildren().Count == 0)
                        flag = true;
                    foreach(TreeNode<Role> child in roleNode.getChildren())
                        flag = removeOwner(child.Data.User.Id, roleNode.Data);
                }
                if (flag && ownerNode.RemoveChild(roleNode)
                     && RolesDictionary.Remove(userID)
                    && roleNode.Data.User.Roles.Remove(this.Id))
                {

                    LogManager.Instance.WriteToLog("Store-Remove owner Succeeded- The user " + userID);

                    return true;
                }


            }
            LogManager.Instance.WriteToLog("Store-Remove owner Fail- The user " + userID);
            return false;

        }

        public bool removeManager(int userID,Role owner)
        {
            TreeNode<Role> roleNode = null;
            TreeNode<Role> ownerNode = RolesDictionary[owner.User.Id];
            if (RolesDictionary.ContainsKey(userID))
                roleNode = RolesDictionary[userID];
            if (roleNode != null)
            {
                if (ownerNode.RemoveChild(roleNode)
                     && RolesDictionary.Remove(userID)
                    && roleNode.Data.User.Roles.Remove(this.Id))
                    return true;
            }
            LogManager.Instance.WriteToLog("Store-Remove manager Fail- The user " + userID + " is not manger in the store " + this.id + ".\n");
            return false;
        }


        internal bool productExist(string product)
        {
            foreach (int p in Products.Keys)
            {
                if ((Products[p].Product.ProductName).Equals(product))
                    return true;
            }
            return false;
        }

        internal int getProduct(string product)
        {
            foreach (int p in Products.Keys) {
                if((Products[p].Product.ProductName).Equals(product))
                return p;
            }
            return -1;
        }

        public bool createNewProductInStore(string productName, string category, string details, int price, int productID,int userID)
        {
            TreeNode<Role> roleNode = null;
            if (RolesDictionary.ContainsKey(userID))
                roleNode = RolesDictionary[userID];
            if (roleNode != null)
            {
                if ((roleNode.Data.GetType() == typeof(Owner))|| (roleNode.Data.GetType()==typeof(Manager) && ((Manager)(roleNode.Data)).validatePermission(2)))
                {
                    Product p = new Product(productID, productName, category, details, price);
                    ProductInStore pis = new ProductInStore(0, this, p);
                    if (!Products.ContainsKey(productID))
                    {
                        Products.Add(productID, pis);
                        return true;
                    }
                }
            }       

            return false;
        }

        public bool addProductsInStore(List<KeyValuePair<int, int>> productsQuantityList, int userID)
        {
            TreeNode<Role> roleNode = null;
            if (RolesDictionary.ContainsKey(userID))
                roleNode = RolesDictionary[userID];
            if (roleNode != null)
            {
                if ((roleNode.Data.GetType() == typeof(Owner))|| (roleNode.Data.GetType()==typeof(Manager) && ((Manager)(roleNode.Data)).validatePermission(5)))
                {
                    foreach (KeyValuePair<int, int> p in productsQuantityList)
                        if (Products.ContainsKey(p.Key))
                            Products[p.Key].Quantity += p.Value;
                    return true;
                }
            }

            return false;
        }

        public bool removeProductsInStore(List<KeyValuePair<int, int>> productsQuantityList , int userID)
        {
            TreeNode<Role> roleNode = null;
            if (RolesDictionary.ContainsKey(userID))
                roleNode = RolesDictionary[userID];
            if (roleNode != null)
            {
                if ((roleNode.Data.GetType() == typeof(Owner))|| (roleNode.Data.GetType()==typeof(Manager) && ((Manager)(roleNode.Data)).validatePermission(5)))
                {
                    foreach (KeyValuePair<int, int> p in productsQuantityList)
                        if (Products.ContainsKey(p.Key))
                            Products[p.Key].Quantity -= p.Value;
                    return true;
                }
            }

            return false;
        }

        public bool editProductsInStore(int productID, string productName, string category, string details, int price, int userID)
        {
            TreeNode<Role> roleNode = null;
            if (RolesDictionary.ContainsKey(userID))
                roleNode = RolesDictionary[userID];
            if (roleNode != null)
            {
                if ((roleNode.Data.GetType() == typeof(Owner))|| (roleNode.Data.GetType()==typeof(Manager) && ((Manager)(roleNode.Data)).validatePermission(6)))
                {
                    if(Products.ContainsKey(productID))
                    {
                        Products[productID].Product.ProductName = productName;
                        Products[productID].Product.Category = category;
                        Products[productID].Product.Details = details;
                        Products[productID].Product.Price = price;
                    }
                    return true;
                }
            }

            return false;
        }

        public bool assignOwner(User assignedUser, Role owner)//CHANGED SIGNATURE
        {
            TreeNode<Role> assignedNode = null;
            TreeNode<Role> ownerNode = RolesDictionary[owner.User.Id];
            if (RolesDictionary.ContainsKey(assignedUser.Id))
                return false;
            if (ownerNode != null)
            {
                Owner assignedOwner = new Owner(this, assignedUser);
                assignedNode = ownerNode.AddChild(owner);
                RolesDictionary.Add(assignedOwner.User.Id, assignedNode);
                assignedNode.Data.User.Roles.Add(this.Id,assignedNode.Data);
                return true;
            }
            return false;
        }
    }
}
