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
        public readonly static double TwoPi = Math.PI * 2;
        public readonly static double PiOverTwo = Math.PI / 2;

        public double Fraction;

        public Angle(double fraction)
        {
            Fraction = fraction;
        }

        public Angle(double numerator, double denominator)
        {
            Fraction = numerator / denominator;
        }

        public static Angle FromDegrees(double value)
        {
            return new Angle(value, 360);
        }

        public static Angle FromRadians(double value)
        {
            return new Angle(value, TwoPi);
        }

        public static Angle FromHours(double value)
        {
            return new Angle(value, 24);
        }

        public double Degrees
        {
            get
            {
                return Fraction * 360;
            }
        }

        public double Radians
        {
            get
            {
                return Fraction * Angle.TwoPi;
            }
        }

        public double Hours
        {
            get
            {
                return Fraction * 24;
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
                return new Angle(1 + (Fraction % 1));
            }
            else if (Fraction >= maxExclusive)
            {
                return new Angle(Fraction % 1);
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
            return a.Fraction < b.Fraction;
        }

        public static bool operator >(Angle a, Angle b)
        {
            return a.Fraction > b.Fraction;
        }

        public static bool operator <=(Angle a, Angle b)
        {
            return a.Fraction <= b.Fraction;
        }

        public static bool operator >=(Angle a, Angle b)
        {
            return a.Fraction >= b.Fraction;
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
            return Fraction == other.Fraction;
        }

        public override int GetHashCode()
        {
            return Fraction.GetHashCode();
        }

        public static Angle operator +(Angle a, Angle b)
        {
            return new Angle(a.Fraction + b.Fraction);
        }

        public static Angle operator -(Angle a, Angle b)
        {
            return new Angle(a.Fraction - b.Fraction);
        }
    }
}
