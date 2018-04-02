using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Extellect.Utilities.Math;

namespace Orbitools
{
    [TestClass]
    public class GeocentricParallaxEffectUnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var effect = new GeocentricParallaxEffect { GeocentricDistanceToObserver = 1 };
            var geo = new HorizontalCoordinates(Angle.FromDegrees(90), Angle.FromDegrees(127));
            var surface = effect.Distort(geo, 9);

        }
    }
}
