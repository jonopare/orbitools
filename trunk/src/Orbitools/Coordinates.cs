﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public static class CartesianCoordinates
    {
        public static Triplet ToCartesian(Angle ra, Angle dec, double geocentricDistance)
        {
            var z = Math.Sin(ra.Radians) * geocentricDistance;
            var h = Math.Cos(ra.Radians) * geocentricDistance;
            var x = Math.Sin(dec.Radians) * h;
            var y = Math.Cos(dec.Radians) * h;

            return new Triplet(x, y, z);
        }

        public static Tuple<Angle, Angle> FromCartesian(Triplet vector)
        {
            var az = Math.Atan2(vector.X, vector.Y);
            var alt = Math.Asin(vector.Z / vector.Length);

            return new Tuple<Angle, Angle>(Angle.FromRadians(alt), Angle.FromRadians(az));
        }
    }

    public static class Coordinates
    {
        public static Tuple<double, double, double> ToSpherical(Triplet cartesian)
        {
            //(r, θ, φ) = radial distance, inclination (or elevation), and azimuth

            var radius = cartesian.Length;
            var inclination = Math.Acos(cartesian.Z / radius);
            var azimuth = Math.Atan2(cartesian.Y, cartesian.X);
            return new Tuple<double, double, double>(radius, inclination, azimuth);
        }
    }
}