using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orbitools;

namespace Opticks
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ParabolicReflectorUnitTests
    {
        private const double Tolerance = 1e-9;

        private ParabolicReflector reflector;

        [TestInitialize]
        public void Init()
        {
            reflector = new ParabolicReflector(650, 130);
        }

        [TestMethod]
        public void Radius_IsHalfTheDiameter()
        {
            Assert.AreEqual(65, reflector.Radius, Tolerance);
        }

        [TestMethod]
        public void FocalRatio_IsF5()
        {
            Assert.AreEqual(5, reflector.FocalRatio, Tolerance);
        }

        [TestMethod]
        public void SurfaceZ_AtCircumference_Is1625()
        {
            Assert.AreEqual(1.625, reflector.SurfaceZ(65), Tolerance);
        }

        [TestMethod]
        public void SurfaceDzDr_AtCircumference_Is005()
        {
            Assert.AreEqual(0.05, reflector.SurfaceDzDr(65), Tolerance);
        }

        [TestMethod]
        public void SurfaceToDirectrix_AtCircumference_Is651625()
        {
            Assert.AreEqual(651.625, reflector.SurfaceToDirectrix(65), Tolerance);
        }

        [TestMethod]
        public void SurfaceToFocus_AtCircumference_Is651625()
        {
            Assert.AreEqual(651.625, reflector.SurfaceToFocus(65), Tolerance);
        }

        [TestMethod]
        public void ReflectedAngle_AtCircumference_IsNeg9975()
        {
            Assert.AreEqual(-9.975, reflector.ReflectedAngle(65), Tolerance);
        }

        [TestMethod]
        public void ReflectedAngle_HalfDegreeAtCircumference_IsSteeper()
        {
            Assert.AreEqual(-10.935683666766, reflector.ReflectedAngle(65, Angle.FromDegrees(0.5)), Tolerance);
        }

        [TestMethod]
        public void AxisOfReflection_Right()
        {
            var axis = reflector.AxisOfRefection(65, 0);
            var reflected = ParabolicReflector.Reflect(new Triplet(0, 0, -1), axis);
            TripletUnitTests.AssertAreEqual(new Triplet(-1, 0, 9.975).Unit, reflected.Unit, Tolerance);
            //Assert.AreEqual(-10.935683666766, reflector.ReflectedAngle(65, Angle.FromDegrees(0.5).Radians), Tolerance);
        }

        [TestMethod]
        public void AxisOfReflection_Left()
        {
            var axis = reflector.AxisOfRefection(-65, 0);
            var reflected = ParabolicReflector.Reflect(new Triplet(0, 0, -1), axis);
            TripletUnitTests.AssertAreEqual(new Triplet(1, 0, 9.975).Unit, reflected.Unit, Tolerance);
            //Assert.AreEqual(-10.935683666766, reflector.ReflectedAngle(65, Angle.FromDegrees(0.5).Radians), Tolerance);
        }

        [TestMethod]
        public void AxisOfReflection_Top()
        {
            var axis = reflector.AxisOfRefection(0, 65);
            var reflected = ParabolicReflector.Reflect(new Triplet(0, 0, -1), axis);
            TripletUnitTests.AssertAreEqual(new Triplet(0, -1, 9.975).Unit, reflected.Unit, Tolerance);
        }

        [TestMethod]
        public void AxisOfReflection_Bottom()
        {
            var axis = reflector.AxisOfRefection(0, -65);
            var reflected = ParabolicReflector.Reflect(new Triplet(0, 0, -1), axis);
            TripletUnitTests.AssertAreEqual(new Triplet(0, 1, 9.975).Unit, reflected.Unit, Tolerance);
        }

        [TestMethod]
        public void AxisOfReflection_NearEdgeButIncludesRotation()
        {
            var axis = reflector.AxisOfRefection(11, 64);
            var reflected = ParabolicReflector.Reflect(new Triplet(0, 0, -1), axis);
            var expected = new Triplet(-0.016880954446803455, -0.098216462235947363, 0.99502188916768042);
            TripletUnitTests.AssertAreEqual(expected, reflected.Unit, Tolerance);
        }
    }
}
