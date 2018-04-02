using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Extellect.Utilities.Math;

namespace Orbitools
{
    [TestClass]
    public class SundialUnitTests
    {
        [Flags]
        private enum Direction
        {
            North = 1,
            East = 2,
            South = 4,
            West = 8
        }

        #region Helpers
        /// <summary>
        /// Y+ve = North
        /// X+ve = East
        /// Y-ve = South
        /// X-ve = West
        /// </summary>
        private static Direction ToDirection(Triplet value)
        {
            var result = (Direction)0;
            if (value.X > 1e-9) result |= Direction.East;
            if (value.X < -1e-9) result |= Direction.West;
            if (value.Y > 1e-9) result |= Direction.North;
            if (value.Y < -1e-9) result |= Direction.South;
            return result;
        }
        #endregion

        [TestMethod]
        public void Light_NorthernHemisphereWinterMidMorning_ShadowNW()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(22.5), Angle.FromDegrees(90 + 22.5));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.North | Direction.West, ToDirection(shadow));

        }

        #region Equator
        [TestMethod]
        public void Light_EquatorEquinoxSunrise_ShadowW()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(0), Angle.FromDegrees(90));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.South | Direction.West, ToDirection(shadow));

        }

        [TestMethod]
        public void Light_EquatorEquinoxSunrise_ShadowOverhead()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(90), Angle.FromDegrees(0));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual((Direction)0, ToDirection(shadow));

        }

        [TestMethod]
        public void Light_EquatorEquinoxSunset_ShadowE()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            // cos of 270 degrees in radians is marginally off-zero, and causes unexpected 
            // results when multiplied by infinite length
            var sun = new HorizontalCoordinates(Angle.FromDegrees(0), Angle.FromDegrees(270.01));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.East | Direction.South, ToDirection(shadow));
        }
        #endregion

        #region Southern Hemisphere
        [TestMethod]
        public void Light_SouthernHemisphereWinterSunrise_ShadowSW()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(0), Angle.FromDegrees(90 - 11.25));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.South | Direction.West, ToDirection(shadow));

        }

        [TestMethod]
        public void Light_SouthernHemisphereWinterEarlyMorning_ShadowSW()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(22.5), Angle.FromDegrees(90 - 22.5));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.South | Direction.West, ToDirection(shadow));

        }

        [TestMethod]
        public void Light_SouthernHemisphereWinterLateMorning_ShadowSW()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(90 - 22.5), Angle.FromDegrees(22.5));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.South | Direction.West, ToDirection(shadow));

        }

        [TestMethod]
        public void Light_SouthernHemisphereWinterMidday_ShadowS()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(60), Angle.FromDegrees(0));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.South, ToDirection(shadow));
        }

        [TestMethod]
        public void Light_SouthernHemisphereWinterEarlyAfternoon_ShadowSE()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(90 - 22.5), Angle.FromDegrees(360 - 22.5));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.South | Direction.East, ToDirection(shadow));

        }

        [TestMethod]
        public void Light_SouthernHemisphereWinterLateAfternoon_ShadowSE()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(22.5), Angle.FromDegrees(360 - 90 + 22.5));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.South | Direction.East, ToDirection(shadow));
        }

        [TestMethod]
        public void Light_SouthernHemisphereWinterSunset_ShadowSE()
        {
            var sundial = new Sundial();
            sundial.PerpendicularStyleHeight = 1;

            var sun = new HorizontalCoordinates(Angle.FromDegrees(0), Angle.FromDegrees(360 - 90 + 11.25));

            sundial.Light(sun);

            var shadow = sundial.Shadow;

            Assert.AreEqual(Direction.South | Direction.East, ToDirection(shadow));
        }
        #endregion
    }
}
