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
    public class utCustomer
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

            var rows = dvd.tblCustomers;
            actual = rows.Count();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InsertTest()
        {
            tblCustomer newRow = new tblCustomer();

            newRow.Id = -99;
            newRow.FirstName = "Dennis";
            newRow.LastName = "Richardson";
            newRow.Address = "515 California Ave";
            newRow.City = "Stockton";
            newRow.State = "CA";
            newRow.Zip = "12321";
            newRow.Phone = "999-999-9999";
            newRow.UserId = -99;


            dvd.tblCustomers.Add(newRow);
            int result = dvd.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblCustomer row = (from dt in dvd.tblCustomers
                                where dt.Id == 2
                                select dt).FirstOrDefault();

            if (row != null)
            {
                row.State = "MI";
                int result = dvd.SaveChanges();

                Assert.IsTrue(result == 1);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            tblCustomer row = (from dt in dvd.tblCustomers
                                where dt.Id == 2
                                select dt).FirstOrDefault();

            if (row != null)
            {
                dvd.tblCustomers.Remove(row);
                int result = dvd.SaveChanges();

                Assert.AreNotEqual(0, result);
            }

        }
    }
}
