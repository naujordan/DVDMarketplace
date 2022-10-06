using Microsoft.EntityFrameworkCore.Storage;
using JTN.DVDCentral.PL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.PL.Test
{
    [TestClass]
    public class utRating
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
            int expected = 5;
            int actual = 0;

            var rows = dvd.tblRatings;
            actual = rows.Count();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InsertTest()
        {
            tblRating newRow = new tblRating();

            newRow.Id = -99;
            newRow.Description = "New";

            dvd.tblRatings.Add(newRow);
            int result = dvd.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblRating row = (from dt in dvd.tblRatings
                               where dt.Id == 2
                               select dt).FirstOrDefault();

            if (row != null)
            {
                row.Description = "New2";
                int result = dvd.SaveChanges();

                Assert.IsTrue(result == 1);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            tblRating row = (from dt in dvd.tblRatings
                               where dt.Id == 2
                               select dt).FirstOrDefault();

            if (row != null)
            {
                dvd.tblRatings.Remove(row);
                int result = dvd.SaveChanges();

                Assert.AreNotEqual(0, result);
            }

        }
    }
}
