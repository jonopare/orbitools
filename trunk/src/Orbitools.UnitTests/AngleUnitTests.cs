using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orbitools
{
    [TestClass]
    public class AngleUnitTests
    {
        

        [TestMethod]
        public void TwoPi_ReadonlyStatic_HasCorrectValue()
        {
            Assert.AreEqual(2 * Math.PI, Angle.TwoPi.Radians, 1e-6);
        }

        [TestMethod]
        public void PiOverTwo_ReadonlyStatic_HasCorrectValue()
        {
            Assert.AreEqual(Math.PI / 2, Angle.PiOverTwo.Radians, 1e-6);
        }

        [TestMethod]
        public void PiOverFour_ReadonlyStatic_HasCorrectValue()
        {
            Assert.AreEqual(Math.PI / 4, Angle.PiOverFour.Radians, 1e-6);
        }

        [TestMethod]
        public void Zero_ReadonlyStatic_HasCorrectValue()
        {
            Assert.AreEqual(0, Angle.Zero.Radians, 1e-6);
        }

        [TestMethod]
        public void Constrain_BelowZero_WithinBoundary()
        {
            Assert.AreEqual(359, Angle.FromDegrees(-1).Constrain().Degrees, 1e-6);
        }

        [TestMethod]
        public void Constrain_EqualsOne_WithinBoundary()
        {
            Assert.AreEqual(0, Angle.FromDegrees(360).Constrain().Degrees, 1e-6);
        }

        [TestMethod]
        public void Constrain_AboveOne_WithinBoundary()
        {
            Assert.AreEqual(1, Angle.FromDegrees(361).Constrain().Degrees, 1e-6);
        }

        [TestMethod]
        public void Degrees_SixQuadrants_InputEqualsOutput()
        {
            for (int q = -1; q < 5; q++)
            {
                var degrees = q * 90.0 + 45.0;
                Assert.AreEqual(degrees, Angle.FromDegrees(degrees).Degrees, 1e-6);
            }
        }

        [TestMethod]
        public void Fraction_SixQuadrants_InputEqualsOutput()
        {
            for (int q = -1; q < 5; q++)
            {
                var fraction = q / 4.0 + 0.125;
                Assert.AreEqual(fraction, Angle.FromFraction(fraction).Fraction, 1e-6);
            }
        }

        [TestMethod]
        public void Fraction2_SixQuadrants_InputEqualsOutput()
        {
            for (int q = -1; q < 5; q++)
            {
                var numerator = q + 0.5;
                var denominator = 4.0;
                var fraction = numerator / denominator;
                Assert.AreEqual(fraction, Angle.FromFraction(numerator, denominator).Fraction, 1e-6);
            }
        }

        [TestMethod]
        public void Hours_SixQuadrants_InputEqualsOutput()
        {
            for (int q = -1; q < 5; q++)
            {
                var hours = q * 24 + 8.0;
                Assert.AreEqual(hours, Angle.FromHours(hours).Hours, 1e-6);
            }
        }

        [TestMethod]
        public void Radians_SixQuadrants_InputEqualsOutput()
        {
            for (int q = -1; q < 5; q++)
            {
                var radians = q * Angle.PiOverTwo.Radians + Angle.PiOverFour.Radians;
                Assert.AreEqual(radians, Angle.FromRadians(radians).Radians, 1e-6);
            }
        }

        [TestMethod]
        public void Add_PositivePositive_BiggerAngle()
        {
            Assert.AreEqual(3, (Angle.FromRadians(2) + Angle.FromRadians(1)).Radians, 1e-6);
        }

        [TestMethod]
        public void Add_PositiveNegative_SmallerAngle()
        {
            Assert.AreEqual(1, (Angle.FromRadians(2) + Angle.FromRadians(-1)).Radians, 1e-6);
        }

        [TestMethod]
        public void Subtract_PositivePositive_SmallerAngle()
        {
            Assert.AreEqual(1, (Angle.FromRadians(2) - Angle.FromRadians(1)).Radians, 1e-6);
        }

        [TestMethod]
        public void Add_PositiveNegative_BiggerAngle()
        {
            Assert.AreEqual(3, (Angle.FromRadians(2) - Angle.FromRadians(-1)).Radians, 1e-6);
        }
    }
}
