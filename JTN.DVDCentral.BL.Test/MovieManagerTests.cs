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
    public class MovieManagerTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            Movie movie = new Movie
            {
                Title = "The Hateful Eight",
                Description = "Movie description",
                Cost = 19.99,
                RatingId = 1,
                FormatId = 1,
                DirectorId = 1,
                Quantity = 5,
                ImagePath = "Default umage path",
        };
            Assert.AreEqual(1, MovieManager.Insert(movie, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, MovieManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(3, MovieManager.LoadById(3).Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Movie movie = MovieManager.LoadById(3);
            movie.Description = "Test";
            int results = MovieManager.Update(movie, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.AreEqual(1, MovieManager.Delete(1, true));
        }
    }
}
