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
    public class Lines2DTests
    {
        [TestMethod()]
        public void SineOfAngleBetweenLines_RightAngleAntiClockwise_plus1()
        {
            Vector2 line1 = new Vector2(4.0, 0.0);
            Vector2 line2 = new Vector2(0.0, 3.0);

            double sineTheta = Lines2D.SineOfAngleBetween(line1, line2);

            Assert.IsTrue(sineTheta == 1);
        }

        [TestMethod()]
        public void SineOfAngleBetweenLines_RightAngleClockwise_minus1()
        {
            Vector2 line1 = new Vector2(4.0, 0.0);
            Vector2 line2 = new Vector2(0.0, -3.0);

            double sineTheta = Lines2D.SineOfAngleBetween(line1, line2);

            Assert.IsTrue(sineTheta == -1);
        }

        [TestMethod()]
        public void SineOfAngleBetweenLines_180degreesParallel_0()
        {
            Vector2 line1 = new Vector2(4.0, 4.0);
            Vector2 line2 = new Vector2(-4.0, -4.0);

            double sineTheta = Lines2D.SineOfAngleBetween(line1, line2);

            Assert.IsTrue(sineTheta == 0);
        }

        public void SineOfAngleBetweenLines_45degreesNonOrigin_half()
        {
            Vector2 l1Start = new Vector2(1.0, 0.0);
            Vector2 l1End = new Vector2(6.0, 0.0);
            Vector2 l2Start = new Vector2(0.0, 1.0);
            Vector2 l2End = new Vector2(6.0, 4.0);

            double sineTheta = Lines2D.SineOfAngleBetween(l1Start, l1End, l2Start, l2End);

            Assert.IsTrue(sineTheta == 0.5);
        }

        [TestMethod()]
        public void SineOfHalfAngleBetweenLines_RightAngleAntiClockwise_half()
        {
            Vector2 line1 = new Vector2(4.0, 0.0);
            Vector2 line2 = new Vector2(0.0, 3.0);

            double sineHalfTheta = Lines2D.SineOfHalfAngleBetween(line1, line2);

            Assert.IsTrue(sineHalfTheta == Math.Sin(Math.PI/4));
        }

        [TestMethod()]
        public void TempTest()
        {
            Vector2 line1 = new Vector2(8.0, 0.0);
            Vector2 line2 = new Vector2(0.0, 8.0);
            double distSq = 50.0;
            double expectedResult = 5.0;
            double epsilon = 0.000005;

            var cross = Lines2D.SineOfAngleBetween(line1, line2);
            var h = Math.Sqrt(distSq) / (2 * Math.Sin(0.5 * Math.Asin(cross)));

            Assert.IsTrue(h >= (expectedResult - epsilon) && h <= (expectedResult + epsilon));
        }
    }
}