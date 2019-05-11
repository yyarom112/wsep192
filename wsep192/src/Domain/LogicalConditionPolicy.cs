using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using src.Domain.Dataclass;

namespace src.Domain
{
    class LogicalConditionPolicy : PurchasePolicy
    {
        private int id;
        private LogicalConnections inlog;
        private LogicalConnections outlog;
        private List<PurchasePolicy> children;

        public LogicalConditionPolicy(int id, LogicalConnections inlog, LogicalConnections outlog)
        {
            this.id = id;
            this.inlog = inlog;
            this.outlog = outlog;
            this.children = new List<PurchasePolicy>();
        }

        public bool addChild(PurchasePolicy child)
        {
            if (!children.Contains(child))
            {
                children.Add(child);
                return true;
            }
            return false;
        }
        public bool removeChild(PurchasePolicy child)
        {
            return children.Remove(child);
        }

        public PurchasePolicy getChild(int childId)
        {
            foreach(PurchasePolicy policy in this.children)
            {
                if (policy.getId() == childId)
                    return policy;
            }
            return null;
        }

        public int getId()
        {
            return id;
        }

        public bool CheckCondition(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            switch (this.inlog)
            {
                case LogicalConnections.and:
                    return CheckConditionAnd(cart, user);
                case LogicalConnections.or:
                    return CheckConditionOr(cart, user);
            }
            return false;//Remove me
        }

        public bool CheckConditionAnd(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            foreach(PurchasePolicy policy in children)
            {
                if (!policy.CheckCondition(cart, user))
                    return false;
            }
            return true;
        }

        public bool CheckConditionOr(List<KeyValuePair<ProductInStore, int>> cart, UserDetailes user)
        {
            foreach (PurchasePolicy policy in children)
            {
                if (policy.CheckCondition(cart, user))
                    return true;
            }
            return false;
        }




    }
}
