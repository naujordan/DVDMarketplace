using Microsoft.VisualStudio.TestTools.UnitTesting;
using JTN.DVDCentral.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTN.DVDCentral.BL.Models;

namespace JTN.DVDCentral.BL.Test
{
    [TestClass()]
    public class DirectorManagerTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            Director director = new Director
            {
                FirstName = "Tim",
                LastName = "Burton"
            };
            Assert.AreEqual(1, DirectorManager.Insert(director, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, DirectorManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(3, DirectorManager.LoadById(3).Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Director director = DirectorManager.LoadById(3);
            director.FirstName = "Test";
            int results = DirectorManager.Update(director, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.AreEqual(1, DirectorManager.Delete(1, true));
        }
    }
}