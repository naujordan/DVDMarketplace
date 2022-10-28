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
    public class OrderItemManagerTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            OrderItem orderItem = new OrderItem
            {
                OrderId = 99,
                MovieId = 99,
                Quantity = 5,
                Cost = 15.99
            };
            Assert.AreEqual(1, OrderItemManager.Insert(orderItem, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderItemManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByOrderIdTest()
        {
            Assert.AreEqual(1, OrderItemManager.LoadByOrderId(1).Count) ;
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(3, OrderItemManager.LoadById(3).Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            OrderItem orderItem = OrderItemManager.LoadById(3);
            orderItem.Cost = 3.99;
            int results = OrderItemManager.Update(orderItem, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.AreEqual(1, OrderItemManager.Delete(1, true));
        }
    }
}