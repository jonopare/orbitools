using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public struct HorizontalCoordinates
    {
        public Angle Alt;
        public Angle Az;

        public HorizontalCoordinates(Angle alt, Angle az)
        {
            if (alt.Degrees > 90 || alt.Degrees < -90)
            {
                // todo
            }

            Alt = alt;
            Az = az.Constrain();
        }

        public static HorizontalCoordinates operator -(HorizontalCoordinates a, HorizontalCoordinates b)
        {
            return new HorizontalCoordinates(a.Alt - b.Alt, a.Az - b.Az);
        }

        public override string ToString()
        {
            return "AltDeg=" + Alt.Degrees + ",AzDeg=" + Az.Degrees;
        }

        public EquatorialCoordinates ToEquatorial(TimeSpan localSiderealTime)
        {
            throw new NotImplementedException();
        }

        public bool IsInvalid { get { return double.IsNaN(Alt.Radians) || double.IsNaN(Az.Radians); } }
    }
}
