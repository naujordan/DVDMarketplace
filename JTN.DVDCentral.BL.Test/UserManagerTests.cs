using JTN.DVDCentral.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.BL.Test
{
    [TestClass()]
    public class UserManagerTests
    {
        [TestMethod()]
        public void LoginSuccededTest()
        {
            try
            {
                UserManager.Seed();
                Assert.IsTrue(UserManager.Login(new User { UserId = "jnau", Password = "password" }));
                UserManager.DeleteAll();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [TestMethod()]
        public void LoginFailedTest()
        {
            try
            {
                UserManager.Seed();
                UserManager.Login(new User { UserId = "jnau", Password = "NotPassword" });
                Assert.Fail();

            }
            catch (LoginFailureException)
            {
                Debug.WriteLine("LoginFailureException");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Assert.Fail();
            }
            UserManager.DeleteAll();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            User user = UserManager.LoadById(3);
            user.Password = "Test";
            int results = UserManager.Update(user, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, UserManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(3, UserManager.LoadById(3).Id);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.AreEqual(1, UserManager.Delete(1, true));
        }

        [TestMethod()]
        public void InsertTest()
        {
            User user = new User
            {
                FirstName = "Barry",
                LastName = "Allen",
                UserId = "bAllen",
                Password = "NotTheFlash123"
                
            };
            Assert.AreEqual(1, UserManager.Insert(user, true));
        }
    }


}
