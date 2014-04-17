using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Orbitools
{
    [DebuggerDisplay("X={X} Y={Y} Z={Z} Len={Length}")]
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

        public double Length
        {
            get
            {
                return Math.Sqrt(LengthSquared);
            }
        }

        public double LengthSquared
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
                return this / Length;
            }
        }

        public double Dot(Triplet other)
        {
            return X * other.X + Y * other.Y + Z * other.Z;
        }

        public Triplet Cross(Triplet other)
        {
            return new Triplet(
                Y * other.Z - other.Y * Z,
                Z * other.X - other.Z * X,
                X * other.Y - other.X * Y);
        }

        public Angle AngularDistance(Triplet other)
        {
            return Angle.FromRadians(Math.Acos(Dot(other) / Length / other.Length));
        }

        public Triplet RotateZ(Angle clockwise)
        {
            var s = Math.Sin(clockwise.Radians);
            var c = Math.Cos(clockwise.Radians);
            return new Triplet(X * c + Y * s, Y * c - X * s, Z);
        }

        public override string ToString()
        {
            return string.Format("X={0} Y={1} Z={2} Len={3}", X, Y, Z, Length);
        }
    }
}
