using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public static class Coordinates
    {
        public static Tuple<double, double, double> ToSpherical(Triplet cartesian)
        {
            //(r, θ, φ) = radial distance, inclination (or elevation), and azimuth

            var radius = cartesian.Magnitude;
            var inclination = Math.Acos(cartesian.Z / radius);
            var azimuth = Math.Atan2(cartesian.Y, cartesian.X);
            return new Tuple<double, double, double>(radius, inclination, azimuth);
        }
    }
}
