using Microsoft.VisualStudio.TestTools.UnitTesting;
using GISUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GISUtilities.Tests
{
    [TestClass()]
    public class UtilitiesTests
    {
        [TestMethod()]
        public void IntersectionTest_ParallelLines_IntersectReturnsNull()
        {
            NVector path1Start = new NVector(-40, 50);
            NVector path1End = new NVector(-20, 50);
            NVector path2Start = new NVector(-40, 60);
            NVector path2End = new NVector(-20, 60);

            NVector intersect = Utilities.Intersection(path1Start, path1End, path2Start, path2End);

            Assert.IsTrue(intersect == null);
        }

        [TestMethod()]
        public void GeoCentricRadiusTest_PolarInput_GivesEarthsPolarRadius()
        {
            double radius = Utilities.GeoCentricRadius(89.99999);
            Assert.AreEqual(radius, Utilities.EarthsPolarRadius);
        }
        [TestMethod()]
        public void GeoCentricRadiusTest_EquatorInput_GiveEarthsEquatorialRadius()
        {
            double radius = Utilities.GeoCentricRadius(0.0);
            Assert.AreEqual(radius, Utilities.EarthsEquatorialRadius);
        }
    }
}