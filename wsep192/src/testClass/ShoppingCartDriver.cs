using src.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.testClass
{
    public class ShoppingCartDriver
    {
        private ShoppingCart toTest;

        public ShoppingCartDriver()
        {
            this.toTest = new ShoppingCart(147142123, new Store());
        }

        public ShoppingCart ToTest
        {
            get
            {
                return toTest;
            }

            set
            {
                toTest = value;
            }
        }
         
    }
}
