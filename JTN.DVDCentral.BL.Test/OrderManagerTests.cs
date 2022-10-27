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
    public class OrderManagerTests
    {
        //Tested this without the rollback and it inserted the orderId into the orderItem table
        [TestMethod()]
        public void InsertTest()
        {
            Order order = new Order
            {
                CustomerId = 99,
                OrderDate = new DateTime(1985, 1, 1),
                UserId = 5,
                ShipDate = new DateTime(1999, 1, 1)
            };
            Assert.AreEqual(1, OrderManager.Insert(order, true));
            
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByCustomerIdTest()
        {
            Assert.AreEqual(2, OrderManager.Load(1).Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(3, OrderManager.LoadById(3).Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Order order = OrderManager.LoadById(3);
            order.ShipDate = new DateTime(2022, 10, 12);
            int results = OrderManager.Update(order, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.AreEqual(1, OrderManager.Delete(1, true));
        }
    }
}