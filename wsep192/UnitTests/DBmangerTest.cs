using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.DataLayer;
using src.Domain;

namespace UnitTests
{
    [TestClass]
    public class DBmangerTest
    {
        [TestMethod]
        public void TestMethod_usertableTest()
        {
            DBmanager db = new DBmanager();
            User user = new User(0, "raul", "1234", false, true);
            user.State = state.signedIn;

            Assert.AreEqual(true, db.addNewUser(user), "Add to user table failed");

            Assert.AreEqual(user.UserName, db.getUser(user.Id).UserName, "Get user from table failed");

            user.UserName = "segio";
            user.State = state.visitor;
            user.Password = "471421";
            user.IsAdmin = true;
            Assert.AreEqual(true, db.updateUser(user), "Update user from table failed");

            Assert.AreEqual(user.UserName, db.getUser(user.Id).UserName, "Update user from table failed");

            Assert.AreEqual(true, db.removeUser(user.Id), "Remove user from table failed");

            Assert.AreEqual(null, db.getUser(user.Id), "Remove user from table failed");

        }
    }
}
