using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Extellect.Utilities.Math;

namespace Orbitools
{
    [TestClass]
    public class EquatorialCoordinatesUnitTests
    {
        [TestMethod]
        public void ToHorizontal_NcpAtNorthPole_Alt90AzUndefined()
        {
            var eq = new EquatorialCoordinates(Angle.FromHours(6), Angle.FromDegrees(90));
            var altaz = eq.ToHorizontal(Angle.PiOverTwo);
            Assert.AreEqual(Angle.PiOverTwo, altaz.Alt);
        }

        [TestMethod]
        public void ToHorizontal_PolarisAtNorthPole_AltIsAlmost90()
        {
            var eq = new EquatorialCoordinates(Angle.FromHours(1), Angle.FromDegrees(89));
            var altaz = eq.ToHorizontal(Angle.FromDegrees(89));
            Assert.AreEqual(Angle.PiOverTwo, altaz.Alt);
            Assert.AreEqual(Angle.TwoPi - Angle.PiOverFour, altaz.Az);
        }
    }
}
