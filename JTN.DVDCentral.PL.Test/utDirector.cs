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
    public class utDirector
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

            var rows = dvd.tblDirectors;
            actual = rows.Count();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InsertTest()
        {
            tblDirector newRow = new tblDirector();

            newRow.Id = -99;
            newRow.FirstName = "John";
            newRow.LastName = "Smith";

            dvd.tblDirectors.Add(newRow);
            int result = dvd.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblDirector row = (from dt in dvd.tblDirectors
                            where dt.Id == 2
                            select dt).FirstOrDefault();

            if (row != null)
            {
                row.FirstName = "Thomas";
                int result = dvd.SaveChanges();

                Assert.IsTrue(result == 1);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            tblDirector row = (from dt in dvd.tblDirectors
                            where dt.Id == 2
                            select dt).FirstOrDefault();

            if (row != null)
            {
                dvd.tblDirectors.Remove(row);
                int result = dvd.SaveChanges();

                Assert.AreNotEqual(0, result);
            }

        }
    }
}
