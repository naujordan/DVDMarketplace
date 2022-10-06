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
    public class utGenre
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

            var rows = dvd.tblGenres;
            actual = rows.Count();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InsertTest()
        {
            tblGenre newRow = new tblGenre();

            newRow.Id = -99;
            newRow.Description = "New genre";

            dvd.tblGenres.Add(newRow);
            int result = dvd.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGenre row = (from dt in dvd.tblGenres
                            where dt.Id == 2
                            select dt).FirstOrDefault();

            if (row != null)
            {
                row.Description = "New degree type";
                int result = dvd.SaveChanges();

                Assert.IsTrue(result == 1);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            tblGenre row = (from dt in dvd.tblGenres
                            where dt.Id == 2
                            select dt).FirstOrDefault();

            if (row != null)
            {
                dvd.tblGenres.Remove(row);
                int result = dvd.SaveChanges();

                Assert.AreNotEqual(0, result);
            }

        }
    }
}
