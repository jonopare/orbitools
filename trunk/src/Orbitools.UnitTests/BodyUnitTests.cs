using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orbitools
{
    [TestClass]
    public class BodyUnitTests
    {
        [TestMethod]
        public void AttractiveForce_FromOriginToUnit_IsDirectedTowardOrigin()
        {
            var origin = new BodyBuilder().Mass(1).Position(new Triplet(0, 0, 0)).ToBody();
            var unit = new BodyBuilder().Mass(1).Position(new Triplet(10, 100, 1)).ToBody();

            var df = unit.Gravity(origin);

        }
    }
}
