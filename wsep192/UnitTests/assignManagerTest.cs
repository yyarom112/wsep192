
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Domain;

namespace UnitTests
{
   
    [TestClass]
    public class assignManagerTest
    {
        private TradingSystem system;
        private User ownerUser;
        private User managerUser;
        private Store store1;

        public void setUp()
        {
            system = new TradingSystem(null, null);
            ownerUser = new User(1234, "Seifan", "2457", true, true);
            
            system.Users.Add(ownerUser.Id, ownerUser);
        }


        [TestMethod]
        public void TestMethod1()
        {
            
        }
    }
}
