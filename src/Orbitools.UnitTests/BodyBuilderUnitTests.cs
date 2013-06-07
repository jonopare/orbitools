using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orbitools
{
    [TestClass]
    public class BodyBuilderUnitTests
    {
        [TestMethod]
        public void RotateZ_OneQuarter_a()
        {
            var actual = BodyBuilder.RotateZ(new Triplet(1, 0, 0), Angle.PiOverTwo);
            Assert.IsTrue(new Triplet(0, -1, 0).IsEqualWithinTolerance(actual, 1e-6));
        }

        [TestMethod]
        public void RotateZ_OneQuarter_FromDiagonal()
        {
            var actual = BodyBuilder.RotateZ(new Triplet(1, 1, 0), Angle.PiOverTwo);
            Assert.IsTrue(new Triplet(1, -1, 0).IsEqualWithinTolerance(actual, 1e-6));
        }

        [TestMethod]
        public void RotateZ_TwoQuarters_a()
        {
            var actual = BodyBuilder.RotateZ(new Triplet(1, 0, 0), Angle.Pi);
            Assert.IsTrue(new Triplet(-1, 0, 0).IsEqualWithinTolerance(actual, 1e-6));
        }

        [TestMethod]
        public void RotateZ_TwoQuarters_FromDiagonal()
        {
            var actual = BodyBuilder.RotateZ(new Triplet(1, 1, 0), Angle.Pi);
            Assert.IsTrue(new Triplet(-1, -1, 0).IsEqualWithinTolerance(actual, 1e-6));
        }

        [TestMethod]
        public void RotateZ_ThreeQuarters_a()
        {
            var actual = BodyBuilder.RotateZ(new Triplet(1, 0, 0), Angle.Pi + Angle.PiOverTwo);
            Assert.IsTrue(new Triplet(0, 1, 0).IsEqualWithinTolerance(actual, 1e-6));
        }

        [TestMethod]
        public void RotateZ_FourQuarters_a()
        {
            var actual = BodyBuilder.RotateZ(new Triplet(1, 0, 0), Angle.TwoPi);
            Assert.IsTrue(new Triplet(1, 0, 0).IsEqualWithinTolerance(actual, 1e-6));
        }

        #region Rotate X
        [TestMethod]
        public void RotateX_OneQuarter_FromFirstDiagonal()
        {
            var actual = BodyBuilder.RotateX(new Triplet(0, 1, 2), Angle.PiOverTwo);
            Assert.IsTrue(new Triplet(0, 2, -1).IsEqualWithinTolerance(actual, 1e-6));
        }

        [TestMethod]
        public void RotateX_TwoQuarters_FromFirstDiagonal()
        {
            var actual = BodyBuilder.RotateX(new Triplet(0, 1, 2), Angle.Pi);
            Assert.IsTrue(new Triplet(0, -1, -2).IsEqualWithinTolerance(actual, 1e-6));
        }

        [TestMethod]
        public void RotateX_ThreeQuarters_FromFirstDiagonal()
        {
            var actual = BodyBuilder.RotateX(new Triplet(0, 1, 2), Angle.Pi + Angle.PiOverTwo);
            Assert.IsTrue(new Triplet(0, -2, 1).IsEqualWithinTolerance(actual, 1e-6));
        }
        #endregion

        #region Rotate Y
        [TestMethod]
        public void RotateY_OneQuarter_FromFirstDiagonal()
        {
            var actual = BodyBuilder.RotateY(new Triplet(3, 0, 4), Angle.PiOverTwo);
            Assert.IsTrue(new Triplet(4, 0, -3).IsEqualWithinTolerance(actual, 1e-6));
        }

        [TestMethod]
        public void RotateY_TwoQuarters_FromFirstDiagonal()
        {
            var actual = BodyBuilder.RotateY(new Triplet(3, 0, 4), Angle.Pi);
            Assert.IsTrue(new Triplet(-3, 0, -4).IsEqualWithinTolerance(actual, 1e-6));
        }

        [TestMethod]
        public void RotateY_ThreeQuarters_FromFirstDiagonal()
        {
            var actual = BodyBuilder.RotateY(new Triplet(3, 0, 4), Angle.Pi + Angle.PiOverTwo);
            Assert.IsTrue(new Triplet(-4, 0, 3).IsEqualWithinTolerance(actual, 1e-6));
        }
        #endregion
    }
}
