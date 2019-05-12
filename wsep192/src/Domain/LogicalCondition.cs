using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Domain
{
    class LogicalCondition : ConditionalDiscount
    {
        private LogicalConnections logical;
        private Dictionary<int, ConditionalDiscount> Children;

        public LogicalCondition(int id, double discountPrecentage, Dictionary<int, ProductInStore> products, DateTime endDateDiscount, DuplicatePolicy dup,LogicalConnections logical):base(id,discountPrecentage,products,endDateDiscount,dup)
        {
            this.logical = logical;
            Children = new Dictionary<int, ConditionalDiscount>();
        }

        public override bool checkCondition(List<KeyValuePair<ProductInStore, int>> productList)
        {
            if(this.EndDateDiscount < DateTime.Now)
                return false;
            bool output=false;
            int sum = 0;
            switch (logical)
            {
                case LogicalConnections.and:
                    output = true;
                    break;
                case LogicalConnections.or:
                    output = false;
                    break;
 

            }
            foreach(ConditionalDiscount child in Children.Values)
            {
                bool tmp = child.checkCondition(productList);
                switch (logical)
                {
                    case LogicalConnections.and:
                        output &= tmp;
                        break;
                    case LogicalConnections.or:
                        output |= tmp;
                        break;
                    case LogicalConnections.xor:
                        if (tmp)
                            sum++;
                        break;
                }
            }
            if (logical == LogicalConnections.xor)
                return sum % 2 == 1;
            return output;

        }
        public void addChild(int id, ConditionalDiscount discount)
        {
            Children.Add(id,discount);
        }
        public void removeChild(int discountID)
        {
            Children.Remove(discountID);
        }
        public ConditionalDiscount getChild(int discountId)
        {
            if (Children.ContainsKey(discountId))
                return Children[discountId];
            return null;
        }

        public override DiscountPolicy copy()
        {
            LogicalCondition output = new LogicalCondition(this.Id1, this.DiscountPrecentage, new Dictionary<int, ProductInStore>(this.Products), this.EndDateDiscount, this.Logic, this.logical);
            int i = 0;
            foreach(ConditionalDiscount child in this.Children.Values)
            {
                output.addChild(i,(ConditionalDiscount)child.copy());
            }
            return output;
        }
    }
}
