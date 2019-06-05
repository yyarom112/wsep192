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

                        if (p.Quantity <= this.products[p.Product.Id].Quantity) //if quntity in store bigger then quntity to buy
                            this.products[p.Product.Id].Quantity -= p.Quantity; //Save the quntity
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

        public PurchasePolicy addComplexPurchasePolicy(int ID, String purchesData)
        {
            String[] purchesDataArr = purchesData.Split(',');
            PurchasePolicy output = addComplexPurchasePolicyRec(0, purchesDataArr.Length - 1, ID, purchesDataArr, -1);
            this.purchasePolicy.Add(output);
            return output;
        }

        public PurchasePolicy addComplexPurchasePolicyRec(int begin, int end, int ID, String[] purchesData, int multiplcation)
        {
            if (purchesData[0].Trim(' ').Equals("("))
                begin++;
            switch (Int32.Parse(purchesData[begin].Trim(new char[] { ' ', '(', ')' })))
            {
                case 0:
                    begin++;
                    return factoryProductConditionPolicy(begin, end, ID, purchesData, multiplcation);
                case 1:
                    begin++;
                    return factoryinventoryConditionPolicy(begin, end, ID, purchesData, multiplcation);
                case 2:
                    begin++;
                    return factoryBuyConditionPolicy(begin, end, ID, purchesData, multiplcation);
                case 3:
                    begin++;
                    return factoryUserConditionPolicy(begin, end, ID, purchesData, multiplcation);
                case 4:
                    begin++;
                    return factoryIfThenCondition(begin, end, ID, purchesData, multiplcation);
                case 5:
                    begin++;
                    return factoryLogicalCondition(begin, end, ID, purchesData, multiplcation);

                default:
                    begin++;
                    return null;

            }


        }


        internal PurchasePolicy factoryProductConditionPolicy(int begin, int end, int ID, String[] purchesData, int multiplcation)
        {
            try
            {
                int productID = Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' }));
                int min = Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' }));
                int max = Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' }));
                LogicalConnections act = EnumActivaties.ConvertIntToLogicalConnections(Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' })));
                return new ProductConditionPolicy(ID, productID, min, max, act);
            }
            catch (Exception e)
            {
                return null;
            }

        }


        internal PurchasePolicy factoryinventoryConditionPolicy(int begin, int end, int ID, String[] purchesData, int multiplcation)
        {
            try
            {
                int productID = Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' }));
                int min = Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' }));
                LogicalConnections act = EnumActivaties.ConvertIntToLogicalConnections(Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' })));
                return new inventoryConditionPolicy(ID, productID, min, act);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("Store- factoryinventoryConditionPolicy- Parge string to condition policy faild");
                return null;
            }

        }

        internal PurchasePolicy factoryBuyConditionPolicy(int begin, int end, int ID, String[] purchesData, int multiplcation)
        {
            try
            {
                int min = Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' }));
                int max = Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' }));
                int SumMin = Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' }));
                int SumMax = Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' }));
                LogicalConnections act = EnumActivaties.ConvertIntToLogicalConnections(Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' })));
                return new BuyConditionPolicy(ID, min, max, SumMin, SumMax, act);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("Store- factoryBuyConditionPolicy- Parge string to condition policy faild");
                return null;
            }

        }

        internal PurchasePolicy factoryUserConditionPolicy(int begin, int end, int ID, String[] purchesData, int multiplcation)
        {
            try
            {
                String adress = purchesData[begin++].Trim(new char[] { ' ', '(', ')' });
                bool Isregister = (purchesData[begin++].Trim(new char[] { ' ', '(', ')' }) == "1");
                LogicalConnections act = EnumActivaties.ConvertIntToLogicalConnections(Int32.Parse(purchesData[begin++].Trim(new char[] { ' ', '(', ')' })));
                return new UserConditionPolicy(ID, adress, Isregister, act);
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("Store- factoryUserConditionPolicy- Parge string to condition policy faild");
                return null;
            }
        }

        internal PurchasePolicy factoryIfThenCondition(int begin, int end, int ID, String[] purchesData, int multiplcation)
        {
            int beginIf = -1, endIf = -1, beginThen = -1, endThen = -1, dif = 0;
            ExtractOperand(begin, ref beginIf, ref endIf, purchesData);
            ExtractOperand(endIf + 1, ref beginThen, ref endThen, purchesData);
            LogicalConnections act = EnumActivaties.ConvertIntToLogicalConnections(Int32.Parse(purchesData[endThen + 1].Trim(new char[] { ' ', '(', ')' })));
            return new IfThenCondition(ID, addComplexPurchasePolicyRec(beginIf, endIf, 0, purchesData, multiplcation), addComplexPurchasePolicyRec(beginThen, endThen, 0, purchesData, multiplcation), act);

        }

        internal PurchasePolicy factoryLogicalCondition(int begin, int end, int ID, String[] purchesData, int multiplcation)
        {
            try
            {
                LogicalConnections actIn = EnumActivaties.ConvertIntToLogicalConnections(Int32.Parse(purchesData[end - 1].Trim(new char[] { ' ', '(', ')' })));
                LogicalConnections actOut = EnumActivaties.ConvertIntToLogicalConnections(Int32.Parse(purchesData[end].Trim(new char[] { ' ', '(', ')' })));
                end += -2;
                int childID = 0;
                LogicalConditionPolicy output = new LogicalConditionPolicy(ID, actIn, actOut);
                while (begin <= end)
                {
                    int beginOp = -1, endOp = -1;
                    ExtractOperand(begin, ref beginOp, ref endOp, purchesData);
                    if (beginOp == -1 || endOp == -1)
                        return output;
                    output.addChild(addComplexPurchasePolicyRec(beginOp, endOp, childID++, purchesData, (int)actIn));
                    begin = endOp + 1;

                }
                return output;
            }
            catch (Exception e)
            {
                ErrorManager.Instance.WriteToLog("Store- factoryLogicalCondition- Parge string to condition policy faild");
                return null;
            }

        }

        private int ExtractOperand(int i, ref int begin, ref int end, String[] purchesData)
        {
            int diff = 0; //the current diff between open bracket to close bracket
            begin = -1;
            end = -1;
            bool done = false;
            while (i < purchesData.Length && !done)
            {
                if (purchesData[i].Contains("(("))
                    diff++;
                if (purchesData[i].Contains("("))
                {
                    if (begin == -1)
                        begin = i;
                    else
                        diff++;
                }
                if (purchesData[i].Contains("))"))
                    diff--;
                if (purchesData[i].Contains(")"))
                {
                    if (begin != -1 && diff == 0)
                    {
                        end = i;
                        done = true;
                    }
                    else
                        diff--;
                }
                i++;
            }
            return i;
        }

        public virtual int addRevealedDiscountPolicy(List<KeyValuePair<String, int>> products, double discountPrecentage, DateTime expiredDate, int discountId, DuplicatePolicy logic)
        {
            logic = DuplicatePolicy.WithMultiplication;
            Dictionary<int, KeyValuePair<ProductInStore, int>> relatedProduct = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
            foreach (KeyValuePair<String, int> product in products)
            {
                bool found = false;
                for (int i = 0; i < this.products.Count && !found; i++)
                {
                    if (this.products.ElementAt(i).Value.Product.ProductName.Equals(product.Key))
                    {
                        relatedProduct.Add(this.products.ElementAt(i).Value.Product.Id, new KeyValuePair<ProductInStore, int>(this.products.ElementAt(i).Value, product.Value));
                        found = true;
                    }
                }
            }
            RevealedDiscount newRevealedDiscount = new RevealedDiscount(discountId, discountPrecentage, relatedProduct, expiredDate, logic);
            discountPolicy.Add(newRevealedDiscount);
            LogManager.Instance.WriteToLog("Store - addRevealedDiscountPolicy - new discount policy added\n");
            return discountId;
        }

        public virtual int removeDiscountPolicy(int discountId)
        {
            foreach (DiscountPolicy discount in discountPolicy)
            {
                if (discount.getID() == discountId)
                {
                    discountPolicy.Remove(discount);
                    LogManager.Instance.WriteToLog("Store - removeDiscountPolicy - discount policy removed\n");
                    return 0;
                }
            }
            return -1;
        }

        public virtual int removePurchasePolicy(int purchaseId)
        {
            foreach (PurchasePolicy purchase in purchasePolicy)
            {
                if (purchase.getId() == purchaseId)
                {
                    purchasePolicy.Remove(purchase);
                    LogManager.Instance.WriteToLog("Store - removePurchasePolicy - purchase policy removed\n");
                    return 0;
                }
            }
            return -1;
        }
        public virtual int addConditionalDiscuntPolicy(int discountId, List<String> productsList, String condition, double discountPrecentage, DateTime expiredDiscountDate, DuplicatePolicy dup, LogicalConnections logic)
        {
            Dictionary<int, ProductInStore> discountProduct = new Dictionary<int, ProductInStore>();
            foreach (String product in productsList)
            {
                ProductInStore pis = ConvertProductNameToProductInStore(product);
                discountProduct.Add(pis.Product.Id, pis);
            }
            LogicalCondition toAdd = new LogicalCondition(discountId, discountPrecentage, discountProduct, expiredDiscountDate, dup, logic);
            conditionConvert(toAdd, 0, condition.Split(',').Length, condition.Split(','), 0);
            this.discountPolicy.Add(toAdd);
            return toAdd.Id1;
        }

        public virtual int conditionConvert(LogicalCondition father, int start, int end, String[] condition, int childID)
        {
            int diff = 0 ;
            int s=0, e=0;
            while (start < end)
            {
                if (condition[start].Contains("("))
                {
                    diff++;
                }
                else if (condition[start].Contains(")"))
                {
                    diff--;
                }
                //"( + , - ==conditional discount ||(product id , quntitiy)==leaf"
                if (condition[start].Trim(new char[] { ' ', '(', ')' }).Contains("+"))
                {
                    start++;
                    LogicalCondition toAdd = new LogicalCondition(childID++, 0, null, new DateTime(2222,1,1), DuplicatePolicy.WithMultiplication, LogicalConnections.and);
                    ExtractOperand(start, ref s,ref e, condition);
                    start = conditionConvert(toAdd, s,e+1, condition, 0);
                    father.addChild(childID++,toAdd);
                    start = e + 1;
                }
                else if (condition[start].Trim(new char[] { ' ', '(', ')' }).Contains("-"))
                {
                    start++;
                    LogicalCondition toAdd = new LogicalCondition(childID++, 0, null, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication, LogicalConnections.or);
                    ExtractOperand(start, ref s, ref e, condition);
                    start = conditionConvert(toAdd, s, e + 1, condition, 0);
                    father.addChild(childID++, toAdd);
                    start = e + 1;

                }
                else if (condition[start].Trim(new char[] { ' ', '(', ')' }).Contains("#"))
                {
                    start++;
                    LogicalCondition toAdd = new LogicalCondition(childID++, 0, null, new DateTime(2222, 1, 1), DuplicatePolicy.WithMultiplication, LogicalConnections.xor);
                    ExtractOperand(start, ref s, ref e, condition);
                    start = conditionConvert(toAdd, s, e + 1, condition, 0);
                    father.addChild(childID++, toAdd);
                    start = e + 1;
                }
                else
                {
                    try
                    {
                        ProductInStore product = ConvertProductNameToProductInStore(condition[start++].Trim(new char[] { ' ', '(', ')' }));
                        int quntity = Int32.Parse(condition[start++].Trim(new char[] { ' ', '(', ')' }));
                        Dictionary<int, KeyValuePair<ProductInStore, int>> ConditionProducts = new Dictionary<int, KeyValuePair<ProductInStore, int>>();
                        ConditionProducts.Add(product.Product.Id, new KeyValuePair<ProductInStore, int>(product, quntity));
                        father.addChild(childID++, new LeafCondition(ConditionProducts, childID++, 0, null, new DateTime(), DuplicatePolicy.WithMultiplication));
                    }
                    catch (Exception ex)
                    {
                        ErrorManager.Instance.WriteToLog("Store- conditionConvert- Parge string to condition policy faild");
                    }

                }
            }
            return start;
        }

        public ProductInStore ConvertProductNameToProductInStore(string name)
        {
            foreach (ProductInStore curProduct in this.products.Values)
            {
                if (curProduct.Product.ProductName.Equals(name))
                {
                    return curProduct;
                }
            }
            return null;
        }
    }
}
