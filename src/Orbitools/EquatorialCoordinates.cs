using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public struct EquatorialCoordinates
    {
        public Angle RA;
        public Angle Dec;

        public EquatorialCoordinates(Angle rightAscension, Angle declination)
        {
            RA = rightAscension;
            Dec = declination;
        }

        public override string ToString()
        {
            return "RAHours=" + RA.Hours + ",DecDeg=" + Dec.Degrees;
        }

        public HorizontalCoordinates ToHorizontal(Angle latitude, TimeSpan localSiderealTime)
        {
            var hourAngle = (Angle.FromHours(localSiderealTime.TotalHours) - RA).Constrain();
            return new EquatorialCoordinates(hourAngle, Dec).ToHorizontal(latitude);
        }

        public HorizontalCoordinates ToHorizontal(Angle latitude)
        {
            double sinAlt = Math.Sin(Dec.Radians) * Math.Sin(latitude.Radians) + Math.Cos(Dec.Radians) * Math.Cos(latitude.Radians) * Math.Cos(RA.Radians);
            double alt = Math.Asin(sinAlt);
            double cosAz = (Math.Sin(Dec.Radians) - Math.Sin(alt) * Math.Sin(latitude.Radians)) / (Math.Cos(alt) * Math.Cos(latitude.Radians));
            double az = Math.Acos(cosAz);

            //If sin(HA) is negative, then AZ = A, otherwise AZ = 360 - A
            if (Math.Sin(RA.Radians) >= 0)
            {
                az = Angle.TwoPi.Radians - az;
            }

            return new HorizontalCoordinates(Angle.FromRadians(alt), Angle.FromRadians(az));
        }
    }
}
