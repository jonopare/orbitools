using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Orbitools
{
    [DebuggerDisplay("X={X} Y={Y} Z={Z} Mag={Magnitude}")]
    public struct Triplet
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public Triplet(double x, double y, double z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Magnitude
        {
            get
            {
                return Math.Sqrt(MagnitudeSquared);
            }
        }

        public double MagnitudeSquared
        {
            get
            {
                return Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2);
            }
        }

        public static Triplet operator +(Triplet a, Triplet b)
        {
            return new Triplet(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Triplet operator -(Triplet a, Triplet b)
        {
            return new Triplet(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Triplet operator *(Triplet value, double scalar)
        {
            return new Triplet(value.X * scalar, value.Y * scalar, value.Z * scalar);
        }

        public static Triplet operator *(double scalar, Triplet value)
        {
            return new Triplet(value.X * scalar, value.Y * scalar, value.Z * scalar);
        }

        public static Triplet operator /(Triplet value, double scalar)
        {
            return new Triplet(value.X / scalar, value.Y / scalar, value.Z / scalar);
        }

        /// <summary>
        /// Undefined when Magnitude is zero.
        /// </summary>
        public Triplet Unit
        {
            get
            {
                return this / Magnitude;
            }
        }

        public override string ToString()
        {
            return string.Format("X={0} Y={1} Z={2} Mag={3}", X, Y, Z, Magnitude);
        }
    }
}
