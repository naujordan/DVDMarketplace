using JTN.DVDCentral.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTN.DVDCentral.BL.Test
{
    [TestClass()]
    public class RatingManagerTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            Rating rating = new Rating
            {
                Description = "test"
            };
            Assert.AreEqual(1, RatingManager.Insert(rating, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(5, RatingManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(1, RatingManager.LoadById(1).Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Rating rating = RatingManager.LoadById(3);
            rating.Description = "Test";
            int results = RatingManager.Update(rating, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.AreEqual(1, RatingManager.Delete(1, true));
        }
    }
}
