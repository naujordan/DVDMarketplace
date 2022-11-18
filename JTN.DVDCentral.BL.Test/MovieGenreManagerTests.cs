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
    public class MovieGenreManagerTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            Assert.AreEqual(1, MovieGenreManager.Insert(99, 99, true));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            int results = MovieGenreManager.Update(1, 2, 2, true);
            Assert.AreEqual(1, results);
        }

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    Assert.AreEqual(1, MovieGenreManager.Delete(1, 2, true));
        //}
    }
}
