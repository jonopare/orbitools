﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orbitools
{
    [TestClass]
    public class TripletUnitTests
    {
        public static void AssertAreEqual(Triplet expected, Triplet actual)
        {
            Assert.AreEqual(expected.X, actual.X);
            Assert.AreEqual(expected.Y, actual.Y);
            Assert.AreEqual(expected.Z, actual.Z);
        }

        public static void AssertAreEqual(Triplet expected, Triplet actual, double tolerance)
        {
            Assert.AreEqual(expected.X, actual.X, tolerance);
            Assert.AreEqual(expected.Y, actual.Y, tolerance);
            Assert.AreEqual(expected.Z, actual.Z, tolerance);
        }

        #region Length
        [TestMethod]
        public void Length_Origin_IsZero()
        {
            var t = new Triplet(0, 0, 0);
            Assert.AreEqual(0, t.Length);
        }

        [TestMethod]
        public void Length_UnitX_IsOne()
        {
            var t = new Triplet(1, 0, 0);
            Assert.AreEqual(1, t.Length);
        }

        [TestMethod]
        public void Length_UnitY_IsOne()
        {
            var t = new Triplet(0, 1, 0);
            Assert.AreEqual(1, t.Length);
        }

        [TestMethod]
        public void Length_UnitZ_IsOne()
        {
            var t = new Triplet(0, 0, 1);
            Assert.AreEqual(1, t.Length);
        }

        [TestMethod]
        public void Length_UnitXY_IsRoot2()
        {
            var t = new Triplet(1, 1, 0);
            Assert.AreEqual(Math.Sqrt(2), t.Length);
        }

        [TestMethod]
        public void Length_UnitXZ_IsRoot2()
        {
            var t = new Triplet(1, 0, 1);
            Assert.AreEqual(Math.Sqrt(2), t.Length);
        }

        [TestMethod]
        public void Length_UnitYZ_IsRoot2()
        {
            var t = new Triplet(0, 1, 1);
            Assert.AreEqual(Math.Sqrt(2), t.Length);
        }

        [TestMethod]
        public void Length_UnitXYZ_IsOnePointSevenThree()
        {
            var t = new Triplet(1, 1, 1);
            Assert.AreEqual(1.7320508075688772, t.Length, 1e-15);
        }
        #endregion Length

        #region Unit
        [TestMethod]
        public void Unit_RandomXYZ_LengthIsOne()
        {
            var t = new Triplet(200, 2, 20).Unit;
            Assert.AreEqual(1d, t.Length, 1e-15);
        }
        #endregion

        #region Add
        [TestMethod]
        public void Add_UnitXToUnitY_IsUnitXY()
        {
            var t = new Triplet(1, 0, 0) + new Triplet(0, 1, 0);
            Assert.AreEqual(new Triplet(1, 1, 0), t);
        }

        [TestMethod]
        public void Add_Umm_IsOrigin()
        {
            var t = new Triplet(1, 10, 100) + new Triplet(-1, -10, -100);
            Assert.AreEqual(new Triplet(0, 0, 0), t);
        }

        #endregion

        #region Subtract
        [TestMethod]
        public void Subtract_UnitYFromUnitX_IsUmm()
        {
            var t = new Triplet(1, 0, 0) - new Triplet(0, 1, 0);
            Assert.AreEqual(new Triplet(1, -1, 0), t);
        }

        [TestMethod]
        public void Subtract_Umm_IsOrigin()
        {
            var t = new Triplet(1, 10, 100) - new Triplet(1, 10, 100);
            Assert.AreEqual(new Triplet(0, 0, 0), t);
        }
        #endregion

        #region Dot
        [TestMethod]
        public void Dot_()
        {
            var a = new Triplet(1, 2, 3);
            var b = new Triplet(10, 100, 1000);
            Assert.AreEqual(3210, a.Dot(b));
        }
        #endregion

        #region Cross
        [TestMethod]
        public void Cross_()
        {
            var a = new Triplet(1, 2, 3);
            var b = new Triplet(10, 100, 1000);
            AssertAreEqual(new Triplet(1700, -970, 80), a.Cross(b));
        }
        #endregion
    }
}
