using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orbitools
{
    public static class TripletExtensions
    {
        private static bool IsEqualWithinTolerance(double a, double b, double tolerance)
        {
            return Math.Abs(a - b) < Math.Abs(tolerance);
        }

        public static bool IsEqualWithinTolerance(this Triplet expected, Triplet actual, double tolerance)
        {
            return IsEqualWithinTolerance(expected.X, actual.X, tolerance)
            && IsEqualWithinTolerance(expected.Y, actual.Y, tolerance)
            && IsEqualWithinTolerance(expected.Z, actual.Z, tolerance);
        }
    }
}
