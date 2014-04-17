using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    /// <summary>
    /// Encapsulates an angle as a fraction of a complete circle to avoid calculation problems 
    /// caused by different units.
    /// </summary>
    public struct Angle
    {
        public readonly static Angle TwoPi = Angle.FromRadians(Math.PI * 2);
        public readonly static Angle Pi = Angle.FromRadians(Math.PI);
        public readonly static Angle PiOverTwo = Angle.FromRadians(Math.PI / 2);
        public readonly static Angle PiOverFour = Angle.FromRadians(Math.PI / 4);
        public readonly static Angle Zero = new Angle();

        private double value;

        private Angle(double radians)
        {
            value = radians;
        }

        public static Angle FromFraction(double fraction)
        {
            return new Angle(TwoPi.Radians * fraction);
        }

        public static Angle FromFraction(double numerator, double denominator)
        {
            return Angle.FromFraction(numerator / denominator);
        }

        public static Angle FromDegrees(double value)
        {
            return Angle.FromFraction(value, 360);
        }

        public static Angle FromDegrees(double degrees, double minutes, double seconds)
        {
            return Angle.FromFraction(degrees + minutes / 60 + seconds / 3600, 360);
        }

        public static Angle FromRadians(double value)
        {
            return new Angle(value);
        }

        public static Angle FromHours(double value)
        {
            return Angle.FromFraction(value, 24);
        }

        public static Angle FromHours(double hours, double minutes, double seconds)
        {
            return Angle.FromFraction(hours + minutes / 60 + seconds / 3600, 24);
        }

        public double Degrees
        {
            get
            {
                return Radians * 180 / Math.PI;
            }
        }

        public double Radians
        {
            get
            {
                return value;
            }
        }

        public double Hours
        {
            get
            {
                return Radians * 12 / Math.PI;
            }
        }

        public double Fraction
        {
            get
            {
                return Radians / 2 / Math.PI;
            }
        }

        public Angle Constrain(double maxExclusive)
        {
            if (maxExclusive > 1 || maxExclusive <= -1)
            {
                throw new ArgumentOutOfRangeException();
            }
            double minInclusive = maxExclusive - 1; 
            if (Fraction < minInclusive)
            {
                return Angle.FromFraction(1 + (Fraction % 1));
            }
            else if (Fraction >= maxExclusive)
            {
                return Angle.FromFraction(Fraction % 1);
            }
            else
            {
                return this;
            }
        }

        public Angle Constrain()
        {
            return Constrain(1);
        }

        public static bool operator <(Angle a, Angle b)
        {
            return a.Radians < b.Radians;
        }

        public static bool operator >(Angle a, Angle b)
        {
            return a.Radians > b.Radians;
        }

        public static bool operator <=(Angle a, Angle b)
        {
            return a.Radians <= b.Radians;
        }

        public static bool operator >=(Angle a, Angle b)
        {
            return a.Radians >= b.Radians;
        }

        public static bool operator !=(Angle a, Angle b)
        {
            return !a.Equals(b);
        }

        public static bool operator ==(Angle a, Angle b)
        {
            return a.Equals(b);
        }

        public override bool Equals(object other)
        {
            Angle otherAngle = (Angle)other;
            return Equals(otherAngle);
        }

        public bool Equals(Angle other)
        {
            return Radians == other.Radians;
        }

        public override int GetHashCode()
        {
            return Radians.GetHashCode();
        }

        public static Angle operator +(Angle a, Angle b)
        {
            return new Angle(a.Radians + b.Radians);
        }

        public static Angle operator -(Angle a, Angle b)
        {
            return new Angle(a.Radians - b.Radians);
        }

        public static Angle operator -(Angle a)
        {
            return new Angle(0 - a.Radians);
        }

        public static Angle operator *(Angle angle, double scale)
        {
            return new Angle(angle.Radians * scale);
        }

        public static Angle operator *(double scale, Angle angle)
        {
            return Angle.FromRadians(scale * angle.Radians);
        }

        public static Angle operator /(Angle angle, double scale)
        {
            return Angle.FromRadians(angle.Radians / scale);
        }
    }
}
