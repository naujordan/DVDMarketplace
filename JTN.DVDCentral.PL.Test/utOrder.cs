using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
    {
        protected DVDCentralEntities dvd;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dvd = new DVDCentralEntities();
            transaction = dvd.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
        }


        [TestMethod]
        public void LoadTest()
        {
            int expected = 3;
            int actual = 0;

            var rows = dvd.tblOrders;
            actual = rows.Count();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InsertTest()
        {
            tblOrder newRow = new tblOrder();

            newRow.Id = -99;
            newRow.CustomerId = -99;
            newRow.OrderDate = new DateTime(2022, 10, 10);
            newRow.UserId = -99;
            newRow.ShipDate = new DateTime(2022, 10, 10);

            dvd.tblOrders.Add(newRow);
            int result = dvd.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrder row = (from dt in dvd.tblOrders
                                where dt.Id == 2
                                select dt).FirstOrDefault();

            if (row != null)
            {
                row.ShipDate = new DateTime(2022, 10, 10);
                int result = dvd.SaveChanges();

                Assert.IsTrue(result == 1);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            tblOrder row = (from dt in dvd.tblOrders
                                where dt.Id == 2
                                select dt).FirstOrDefault();

            if (row != null)
            {
                dvd.tblOrders.Remove(row);
                int result = dvd.SaveChanges();

                Assert.AreNotEqual(0, result);
            }

        }
    }
}
