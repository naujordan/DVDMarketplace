using Microsoft.VisualStudio.TestTools.UnitTesting;
using JTN.DVDCentral.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTN.DVDCentral.BL.Models;

namespace JTN.DVDCentral.BL.Test
{
    [TestClass()]
    public class CustomerManagerTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            Customer customer = new Customer
            {
                FirstName = "Freddie",
                LastName = "Gibbs",
                Address = "123 Test",
                City = "Gary",
                State = "IN",
                Zip = "55555",
                Phone = "777-777-7777",
                UserId = 99
            };
            Assert.AreEqual(1, CustomerManager.Insert(customer, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, CustomerManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(3, CustomerManager.LoadById(3).Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Customer customer = CustomerManager.LoadById(3);
            customer.FirstName = "Test";
            int results = CustomerManager.Update(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.AreEqual(1, CustomerManager.Delete(1, true));
        }
    }
}