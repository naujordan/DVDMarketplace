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
    public class utOrderItem
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

            var rows = dvd.tblOrderItems;
            actual = rows.Count();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InsertTest()
        {
            tblOrderItem newRow = new tblOrderItem();

            newRow.Id = -99;
            newRow.OrderId = -99;
            newRow.MovieId = 1;
            newRow.Quantity = 5;
            newRow.Cost = 59.65;

            dvd.tblOrderItems.Add(newRow);
            int result = dvd.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblOrderItem row = (from dt in dvd.tblOrderItems
                             where dt.Id == 2
                             select dt).FirstOrDefault();

            if (row != null)
            {
                row.Quantity = 10;
                row.Cost = 150.25;
                int result = dvd.SaveChanges();

                Assert.IsTrue(result == 1);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            tblOrderItem row = (from dt in dvd.tblOrderItems
                             where dt.Id == 2
                             select dt).FirstOrDefault();

            if (row != null)
            {
                dvd.tblOrderItems.Remove(row);
                int result = dvd.SaveChanges();

                Assert.AreNotEqual(0, result);
            }

        }
    }
}
