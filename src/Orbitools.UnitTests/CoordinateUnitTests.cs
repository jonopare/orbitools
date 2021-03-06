﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Extellect.Utilities.Math;

namespace Orbitools
{
    [TestClass]
    public class CoordinateUnitTests
    {
        [TestMethod]
        public void Spherical_ConvertTo_Cartesian()
        {
            // RA will be high, DEC should be 15 degrees from vertical (e.g. 75 degrees)
            var dec = Angle.FromDegrees(75);
            var xyz = CartesianCoordinates.ToCartesian(3 * Angle.PiOverFour, dec, 1);
            var d = xyz.AngularDistance(new Triplet(0, 0, 1));
            AssertAreEqual(Angle.PiOverTwo - dec, d); 
        }

        [TestMethod]
        public void Spherical_ConvertTo_Cartesian_Polaris()
        {
            var ra = Angle.FromDegrees(2, 31, 49.09); // should be completely ignored
            var dec = Angle.FromDegrees(89, 15, 50.8);
            var xyz = CartesianCoordinates.ToCartesian(ra, dec, 1);
            var d = xyz.AngularDistance(new Triplet(0, 0, 1));
            AssertAreEqual(Angle.PiOverTwo - dec, d, Angle.FromDegrees(10e-9)); 
        }

        [TestMethod]
        public void Spherical_ToRightHanded_Polaris()
        {
            var ra = Angle.FromDegrees(2, 31, 49.09); 
            var dec = Angle.FromDegrees(89, 15, 50.8);
            var xyz = CartesianCoordinates.ToRightHanded(ra, dec, 1);
            var d = xyz.AngularDistance(new Triplet(0, 1, 0));
            AssertAreEqual(Angle.PiOverTwo - dec, d, Angle.FromDegrees(10e-9));
        }

        private void AssertAreEqual(Angle expected, Angle actual)
        {
            AssertAreEqual(expected, actual, Angle.Zero);
        }

        private void AssertAreEqual(Angle expected, Angle actual, Angle delta)
        {
            Assert.AreEqual(expected.Radians, actual.Radians, delta.Radians);
        }
    }
}
