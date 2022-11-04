using Microsoft.VisualStudio.TestTools.UnitTesting;
using JTN.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace JTN.DVDCentral.PL.Test
{
    [TestClass]
    public class utUser
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
            //Expected amount
            int expected = 5;

            //Amount that came back
            int actual = 0;

            //Get rows SELECT * FROM tblUsers
            //var rows = (from dt in dc.tblUsers);
            var rows = dvd.tblUsers;
            actual = rows.Count();

            //Verify amount
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InsertTest()
        {
            //Creates new row in memory
            tblUser newRow = new tblUser();

            //Sets properties
            newRow.FirstName = "Chris";
            newRow.LastName = "Jericho";
            newRow.Id = -99;
            newRow.UserId = "cJericho";
            newRow.Password = "Password";


            //inserts row into table
            dvd.tblUsers.Add(newRow);
            int result = dvd.SaveChanges();

            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();
            //Fetch a row to update
            tblUser row = (from dt in dvd.tblUsers
                           where dt.Id == -99
                           select dt).FirstOrDefault();

            if (row != null)
            {
                //Sets properties
                row.FirstName = "Christopher";

                //updates row into table
                int result = dvd.SaveChanges();

                Assert.IsTrue(result == 1);
            }

        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();
            //Fetch a row to update
            tblUser row = (from dt in dvd.tblUsers
                           where dt.Id == -99
                           select dt).FirstOrDefault();

            if (row != null)
            {
                //delete the row into table
                dvd.tblUsers.Remove(row);
                int result = dvd.SaveChanges();

                Assert.AreNotEqual(0, result);
            }

        }
    }
}