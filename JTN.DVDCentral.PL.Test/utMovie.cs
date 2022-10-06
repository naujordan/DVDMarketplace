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
    public class utMovie
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
            var rows = dvd.tblMovies;
            actual = rows.Count();
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InsertTest()
        {
            tblMovie newRow = new tblMovie();

            newRow.Id = -99;
            newRow.Title = "Cool Movie Title";
            newRow.Description = "Movie Description";
            newRow.Cost = 7.49;
            newRow.RatingId = 1;
            newRow.FormatId = 1;
            newRow.DirectorId = 1;
            newRow.Quantity = 25;
            newRow.ImagePath = "Default Image Path";

            dvd.tblMovies.Add(newRow);
            int result = dvd.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblMovie row = (from dt in dvd.tblMovies
                              where dt.Id == 2
                              select dt).FirstOrDefault();

            if (row != null)
            {
                row.Description = "New movie description";


                int result = dvd.SaveChanges();
                Assert.IsTrue(result == 1);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            //Fetch a row to update
            tblMovie row = (from dt in dvd.tblMovies
                              where dt.Id == 2
                              select dt).FirstOrDefault();

            if (row != null)
            {
                dvd.tblMovies.Remove(row);
                int result = dvd.SaveChanges();

                Assert.AreNotEqual(0, result);
            }

        }
    }
}
