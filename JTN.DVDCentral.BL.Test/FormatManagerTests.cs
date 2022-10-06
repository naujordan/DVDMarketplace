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
    public class FormatManagerTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            Format format = new Format
            {
                Description = "New format"
            };
            Assert.AreEqual(1, FormatManager.Insert(format, true));
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.AreEqual(3, FormatManager.Load().Count);
        }

        [TestMethod()]
        public void LoadByIdTest()
        {
            Assert.AreEqual(3, FormatManager.LoadById(3).Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Format format = FormatManager.LoadById(3);
            format.Description = "Test";
            int results = FormatManager.Update(format, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.AreEqual(1, FormatManager.Delete(1, true));
        }
    }
}
