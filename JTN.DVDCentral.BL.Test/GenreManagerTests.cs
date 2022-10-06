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
    public class GenreManagerTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            Genre genre = new Genre
            {
                Description = "New Genre Type"
            };
            Assert.AreEqual(1, GenreManager.Insert(genre, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, GenreManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(3, GenreManager.LoadById(3).Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Genre genre = GenreManager.LoadById(3);
            genre.Description = "Test";
            int results = GenreManager.Update(genre, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.AreEqual(1, GenreManager.Delete(1, true));
        }
    }
}
