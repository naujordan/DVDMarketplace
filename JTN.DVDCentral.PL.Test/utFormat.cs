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
    public class utFormat
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

            var rows = dvd.tblFormats;
            actual = rows.Count();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InsertTest()
        {
            tblFormat newRow = new tblFormat();

            newRow.Id = -99;
            newRow.Description = "Rental";

            dvd.tblFormats.Add(newRow);
            int result = dvd.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblFormat row = (from dt in dvd.tblFormats
                             where dt.Id == 2
                             select dt).FirstOrDefault();

            if (row != null)
            {
                row.Description = "Updated Format";
                int result = dvd.SaveChanges();

                Assert.IsTrue(result == 1);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            tblFormat row = (from dt in dvd.tblFormats
                             where dt.Id == 2
                             select dt).FirstOrDefault();

            if (row != null)
            {
                dvd.tblFormats.Remove(row);
                int result = dvd.SaveChanges();

                Assert.AreNotEqual(0, result);
            }

        }
    }
}
