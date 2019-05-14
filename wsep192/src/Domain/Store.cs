using src.Domain.Dataclass;
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

        public Store(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.products = new Dictionary<int, ProductInStore>();
            this.storeRate = 0;
            this.roles = new TreeNode<Role>(null);
            this.rolesDictionary = new Dictionary<int, TreeNode<Role>>();
            this.purchasePolicy = new List<PurchasePolicy>();
            this.discountPolicy = new List<DiscountPolicy>();
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
                bool flag = p.Product.compareProduct(filter);
                if ((flag && filter.StoreRate == -1) || (flag && filter.StoreRate != -1 && filter.StoreRate == this.storeRate))
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
                    newManager.Store = this;
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
                if (!this.products.ContainsKey(p.Product.Id))
                {
                    cart.Products[p.Product.Id].Quantity = 0;
                    LogManager.Instance.WriteToLog("The attempt to purchase product " + p.Product.Id + " failed because it does not belong to the store.\n");

                }
                else
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
            LogManager.Instance.WriteToLog("Products have declined / returned to store stock successfully.\n");
        }

        public virtual void checkQuntity(ShoppingCart cart)
        {
            foreach (ProductInCart p in cart.Products.Values)
            {
                if (p.Quantity > this.products[p.Product.Id].Quantity)
                    p.Quantity = this.products[p.Product.Id].Quantity;
            }
        }
        public virtual bool confirmPurchasePolicy(Dictionary<int, ProductInCart> products, UserDetailes user)
        {
            if (this.PurchasePolicy == null)
                return true;
            List<KeyValuePair<ProductInStore, int>> productsInStore = new List<KeyValuePair<ProductInStore, int>>();
            foreach (ProductInCart p in products.Values)
            {
                ProductInStore productInStore = new ProductInStore(-1, this, p.Product);
                if (!this.products.ContainsKey(p.Product.Id))
                    return false;
                if (p.Quantity > this.products[p.Product.Id].Quantity)
                {
                    p.Quantity = this.products[p.Product.Id].Quantity;
                    p.ShoppingCart.Products[p.Product.Id].Quantity = this.products[p.Product.Id].Quantity;
                }
                productsInStore.Add(new KeyValuePair<ProductInStore, int>(productInStore, p.Quantity));
            }
            foreach (PurchasePolicy pp in purchasePolicy)
            {
                if (!pp.CheckCondition(productsInStore, user))
                    return false;
            }
            return true;

        }

        //Initials list all products and all discounts
        public virtual double calculateDiscountPolicy(Dictionary<int, ProductInCart> products)
        {
            if (this.DiscountPolicy == null)
                return 0;
            int sum = 0;
            List<KeyValuePair<ProductInStore, int>> productsInStore = new List<KeyValuePair<ProductInStore, int>>();
            foreach (ProductInCart p in products.Values)
            {
                productsInStore.Add(new KeyValuePair<ProductInStore, int>(this.products[p.Product.Id], p.Quantity));
            }
            List<DiscountPolicy> discount = new List<DiscountPolicy>(this.discountPolicy);

            return calculateDiscountPolicy(productsInStore, discount, 0);
        }

        public virtual double calculateDiscountPolicy(List<KeyValuePair<ProductInStore, int>> products, List<DiscountPolicy> discounts, double sum)
        {
            //As long as there are discounts on the list will continue to run
            if (discounts.Count == 0)
                return sum;
            DiscountPolicy maxDiscount = null;
            double maxDiscountSum = 0;
            //Find the best discount right now
            foreach (DiscountPolicy discount in discounts)
            {
                double tmp = 0;
                if (discount.checkCondition(products))
                    tmp = discount.calculate(products);
                if (maxDiscountSum <= tmp)
                {
                    maxDiscount = discount;
                    maxDiscountSum = tmp;
                }
            }

            discounts.Remove(maxDiscount);
            List<KeyValuePair<ProductInStore, int>> productsWithout = CopyProductsList(products);
            List<DiscountPolicy> discountsWithout = CopyDiscountsList(discounts);
            double sumwithout = sum;
            sum += maxDiscountSum;
            maxDiscount.UpdateProductPrice(products);
            switch (maxDiscount.GetDuplicatePolicy())
            {
                case DuplicatePolicy.WithMultiplication:
                    for (int i = discounts.Count - 1; i >= 0; i--)
                    {
                        if (discounts[i].GetDuplicatePolicy() == DuplicatePolicy.WithoutMultiplication)
                        {
                            foreach (ProductInStore product in maxDiscount.GetRelevantProducts().Values)
                                discounts[i].removeProduct(product);
                        }
                    }
                    break;
                case DuplicatePolicy.WithoutMultiplication:
                    foreach (ProductInStore product in maxDiscount.GetRelevantProducts().Values)
                    {
                        bool find = false;
                        for (int i = 0; i < products.Count && !find; i++)
                        {
                            if (products[i].Key.Product.Id == product.Product.Id)
                            {
                                find = true;
                                products.RemoveAt(i);
                            }
                        }
                    }
                    break;
            }
            return Math.Max(calculateDiscountPolicy(productsWithout, discountsWithout, sumwithout), calculateDiscountPolicy(products, discounts, sum));
        }

        private List<KeyValuePair<ProductInStore, int>> CopyProductsList(List<KeyValuePair<ProductInStore, int>> toCopy)
        {
            List<KeyValuePair<ProductInStore, int>> output = new List<KeyValuePair<ProductInStore, int>>();
            foreach (KeyValuePair<ProductInStore, int> p in toCopy)
            {
                output.Add(new KeyValuePair<ProductInStore, int>(new ProductInStore(p.Key.Quantity, this, new Product(p.Key.Product.Id, p.Key.Product.ProductName, null, null, p.Key.Product.Price)), p.Value));
            }
            return output;
        }

        private List<DiscountPolicy> CopyDiscountsList(List<DiscountPolicy> toCopy)
        {
            List<DiscountPolicy> output = new List<DiscountPolicy>();
            foreach (DiscountPolicy discount in toCopy)
            {
                output.Add(discount.copy());
            }
            return output;
        }

        public bool removeOwner(int userID, Role owner)
        {
            TreeNode<Role> ownerNode = RolesDictionary[owner.User.Id];
            TreeNode<Role> roleNode = null;
            bool flag = false;

            if (RolesDictionary.ContainsKey(userID))
                roleNode = RolesDictionary[userID];
            if (roleNode != null && roleNode.Parent != null)
            {
                if (roleNode.Data.GetType() == typeof(Owner))
                {
                    if (roleNode.getChildren() == null || roleNode.getChildren().Count == 0)
                        flag = true;
                    foreach (TreeNode<Role> child in roleNode.getChildren())
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

        public bool removeManager(int userID, Role owner)
        {
            TreeNode<Role> roleNode = null;
            TreeNode<Role> ownerNode = null;
            if (RolesDictionary.ContainsKey(userID) && RolesDictionary.ContainsKey(owner.User.Id))
            {
                ownerNode = RolesDictionary[owner.User.Id];
                roleNode = RolesDictionary[userID];

            }
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
            foreach (int p in Products.Keys)
            {
                if ((Products[p].Product.ProductName).Equals(product))
                    return p;
            }
            return -1;
        }

        public bool createNewProductInStore(string productName, string category, string details, int price, int productID, int userID)
        {
            TreeNode<Role> roleNode = null;
            if (RolesDictionary.ContainsKey(userID))
                roleNode = RolesDictionary[userID];
            if (roleNode != null)
            {
                if ((roleNode.Data.GetType() == typeof(Owner)) || (roleNode.Data.GetType() == typeof(Manager) && ((Manager)(roleNode.Data)).validatePermission(3)))
                {
                    Product p = new Product(productID, productName, category, details, price);
                    ProductInStore pis = new ProductInStore(0, this, p);
                    foreach (ProductInStore pp in Products.Values)
                        if (pp.Product.ProductName == productName)
                            return false;
                    Products.Add(productID, pis);
                    return true;
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
                if ((roleNode.Data.GetType() == typeof(Owner)) || (roleNode.Data.GetType() == typeof(Manager) && ((Manager)(roleNode.Data)).validatePermission(4)))
                {
                    foreach (KeyValuePair<int, int> p in productsQuantityList)
                        if (Products.ContainsKey(p.Key))
                            Products[p.Key].Quantity += p.Value;
                    return true;
                }
            }

            return false;
        }

        public bool removeProductsInStore(List<KeyValuePair<int, int>> productsQuantityList, int userID)
        {
            TreeNode<Role> roleNode = null;
            if (RolesDictionary.ContainsKey(userID))
                roleNode = RolesDictionary[userID];
            if (roleNode != null)
            {
                if ((roleNode.Data.GetType() == typeof(Owner)) || (roleNode.Data.GetType() == typeof(Manager) && ((Manager)(roleNode.Data)).validatePermission(5)))
                {
                    foreach (KeyValuePair<int, int> p in productsQuantityList)
                        if (Products.ContainsKey(p.Key) && Products[p.Key].Quantity >= p.Value)
                            Products[p.Key].Quantity -= p.Value;
                        else return false;
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
                if ((roleNode.Data.GetType() == typeof(Owner)) || (roleNode.Data.GetType() == typeof(Manager) && ((Manager)(roleNode.Data)).validatePermission(6)))
                {
                    if (Products.ContainsKey(productID))
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

        public bool assignOwner(User assignedUser, Role owner)
        {
            TreeNode<Role> assignedNode = null;
            TreeNode<Role> ownerNode = RolesDictionary[owner.User.Id];
            if (RolesDictionary.ContainsKey(assignedUser.Id))
                return false;
            if (ownerNode != null)
            {
                Owner assignedOwner = new Owner(this, assignedUser);
                assignedNode = ownerNode.AddChild(assignedOwner);
                RolesDictionary.Add(assignedOwner.User.Id, assignedNode);
                assignedNode.Data.User.Roles.Add(this.Id, assignedNode.Data);
                return true;
            }
            return false;
        }

        public PurchasePolicy addSimplePurchasePolicy(PurchesPolicyData purchesData)
        {
            if (purchesData == null)
                return null;
            PurchasePolicy toRemove = null;

            switch (purchesData.Type)
            {
                case 0:
                    ProductConditionPolicy toInsert0 = new ProductConditionPolicy(purchesData.Id, purchesData.ProductID, purchesData.Min, purchesData.Max, purchesData.Act);
                    foreach (PurchasePolicy p in PurchasePolicy)
                    {
                        if (p.GetType() == typeof(ProductConditionPolicy) && ((ProductConditionPolicy)p).ProductID == purchesData.ProductID)
                            toRemove = p;
                    }
                    this.PurchasePolicy.Remove(toRemove);
                    purchasePolicy.Add(toInsert0);
                    LogManager.Instance.WriteToLog("Product Condition Policy add to store " + this.name + " successfully");
                    return toInsert0;
                case 1:
                    inventoryConditionPolicy toInsert1 = new inventoryConditionPolicy(purchesData.Id, purchesData.ProductID, purchesData.Min, purchesData.Act);
                    foreach (PurchasePolicy p in this.PurchasePolicy)
                    {
                        if (p.GetType() == typeof(inventoryConditionPolicy) && ((inventoryConditionPolicy)p).ProductID == purchesData.ProductID)
                            toRemove = p;
                    }
                    this.PurchasePolicy.Remove(toRemove);
                    purchasePolicy.Add(toInsert1);
                    LogManager.Instance.WriteToLog("inventory Condition Policy add to store " + this.name + " successfully");
                    return toInsert1;
                case 2:
                    foreach (PurchasePolicy p in this.PurchasePolicy)
                    {
                        if (p.GetType() == typeof(BuyConditionPolicy))
                        {
                            BuyConditionPolicy bcp = (BuyConditionPolicy)p;
                            if (purchesData.Min != -1)
                                bcp.Min = purchesData.Min;
                            if (purchesData.Max != -1)
                                bcp.Max = purchesData.Max;
                            if (purchesData.SumMin != -1)
                                bcp.SumMin = purchesData.SumMin;
                            if (purchesData.SumMax != -1)
                                bcp.Min = purchesData.SumMax;
                            return bcp;
                        }

                    }
                    BuyConditionPolicy toinsert2 = new BuyConditionPolicy(purchesData.Id, purchesData.Min, purchesData.Max, purchesData.SumMin, purchesData.SumMax, purchesData.Act);
                    purchasePolicy.Add(toinsert2);
                    LogManager.Instance.WriteToLog("Buy Condition Policy add to store " + this.name + " successfully");
                    return toinsert2;
                case 3:
                    UserConditionPolicy toinsert3 = new UserConditionPolicy(purchesData.Id, purchesData.Adress, purchesData.Isregister, purchesData.Act);
                    purchasePolicy.Add(toinsert3);
                    LogManager.Instance.WriteToLog("User Condition Policy add to store " + this.name + " successfully");
                    return toinsert3;
                default:
                    return null;

            }
        }

        public PurchasePolicy addComplexPurchasePolicy(List<Object> purchesData)
        {
            return addComplexPurchasePolicyRec(purchesData, -1);
        }

        public PurchasePolicy addComplexPurchasePolicyRec(List<Object> purchesData, int multiplcation)
        {
            if (purchesData == null)
                return null;
            switch ((int)purchesData.First())
            {
                case 0:
                    factoryProductConditionPolicy(purchesData, multiplcation);
                    break;
                case 1:
                    factoryinventoryConditionPolicy(purchesData, multiplcation);
                    break;
                case 2:
                    factoryBuyConditionPolicy(purchesData, multiplcation);
                    break;
                case 3:
                    factoryUserConditionPolicy(purchesData, multiplcation);
                    break;
                case 4:
                    factoryIfThenCondition(purchesData, multiplcation);
                    break;
                case 5:
                    LogicalConnections inLog, outLog;
                    if ((int)purchesData.ElementAt(2) == 0)
                        inLog = LogicalConnections.and;
                    else
                        inLog = LogicalConnections.or;
                    if ((int)purchesData.ElementAt(3) == 0)
                        outLog = LogicalConnections.and;
                    else
                        outLog = LogicalConnections.or;
                    try
                    {
                        List<List<Object>> children = (List<List<Object>>)purchesData.ElementAt(4);
                        LogicalConditionPolicy lcp = new LogicalConditionPolicy((int)purchesData.ElementAt(1), inLog, outLog);
                        foreach (List<Object> child in children)
                            lcp.addChild(addComplexPurchasePolicyRec(child, multiplcation));
                        return lcp;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
            }
            return null;
        }

        internal PurchasePolicy factoryProductConditionPolicy(List<Object> purchesData, int multiplcation)
        {
            try
            {
                return new ProductConditionPolicy((int)purchesData.ElementAt(1), (int)purchesData.ElementAt(2), (int)purchesData.ElementAt(3), (int)purchesData.ElementAt(4), EnumActivaties.ConvertIntToLogicalConnections(purchesData.ElementAt(5)));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal PurchasePolicy factoryinventoryConditionPolicy(List<Object> purchesData, int multiplcation)
        {
            try
            {
                return new inventoryConditionPolicy((int)purchesData.ElementAt(1), (int)purchesData.ElementAt(2), (int)purchesData.ElementAt(3), EnumActivaties.ConvertIntToLogicalConnections(purchesData.ElementAt(4)));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal PurchasePolicy factoryBuyConditionPolicy(List<Object> purchesData, int multiplcation)
        {
            try
            {
                return new BuyConditionPolicy((int)purchesData.ElementAt(1), (int)purchesData.ElementAt(2), (int)purchesData.ElementAt(3), (int)purchesData.ElementAt(4), (int)purchesData.ElementAt(5), EnumActivaties.ConvertIntToLogicalConnections(purchesData.ElementAt(6)));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal PurchasePolicy factoryUserConditionPolicy(List<Object> purchesData, int multiplcation)
        {
            try
            {
                return new UserConditionPolicy((int)purchesData.ElementAt(1), (string)purchesData.ElementAt(2), (bool)purchesData.ElementAt(3), EnumActivaties.ConvertIntToLogicalConnections(purchesData.ElementAt(4)));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal PurchasePolicy factoryIfThenCondition(List<Object> purchesData, int multiplcation)
        {
            if (multiplcation == -1)
                multiplcation = (int) EnumActivaties.ConvertIntToLogicalConnections(purchesData.ElementAt(5));
            try
            {
                List<Object> oprand1 = (List<Object>)purchesData.ElementAt(2);
                oprand1.Insert(1, (int)purchesData.ElementAt(1));
                List<Object> oprand2 = (List<Object>)purchesData.ElementAt(3);
                oprand1.Insert(1, (int)purchesData.ElementAt(1));
                return new IfThenCondition((int)purchesData.ElementAt(1), addComplexPurchasePolicyRec(oprand1, multiplcation), addComplexPurchasePolicyRec(oprand2, multiplcation), EnumActivaties.ConvertIntToLogicalConnections(purchesData.ElementAt(5)));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual int addRevealedDiscountPolicy(Dictionary<int, KeyValuePair<ProductInStore, int>> products, double discountPrecentage, DateTime expiredDate, int discountId, DuplicatePolicy logic)
        {
            logic = DuplicatePolicy.WithMultiplication;
            RevealedDiscount newRevealedDiscount = new RevealedDiscount(discountId, discountPrecentage, products, expiredDate, logic);
            discountPolicy.Add(newRevealedDiscount);
            LogManager.Instance.WriteToLog("Store - addRevealedDiscountPolicy - new discount policy added\n");
            return discountId;
        }

        public virtual int removeDiscountPolicy(int discountId)
        {
            foreach(DiscountPolicy discount in discountPolicy)
            {
                if(discount.getID() ==  discountId)
                {
                    discountPolicy.Remove(discount);
                    LogManager.Instance.WriteToLog("Store - removeRevealedDiscountPolicy - discount policy removed\n");
                    return 0;
                }
            }
            return -1;
        }
    }
}
