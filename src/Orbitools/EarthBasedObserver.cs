using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class EarthBasedObserver
    {
        public Angle Latitude { get; private set; }
        public Angle Longitude { get; private set; }

        public SiderealClock Clock { get; private set; }

        public GeocentricParallaxEffect GeocentricParallax { get; set; }
        public AtmosphericRefractionEffect AtmosphericRefraction { get; set; }

        public EarthBasedObserver(Angle latitude, Angle longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

            Clock = new SiderealClock(Longitude);

            GeocentricParallax = GeocentricParallaxEffect.Create(latitude, longitude, 100);
            AtmosphericRefraction = new AtmosphericRefractionEffect { BarometricPressureMillibars = 1050, TempDegreesC = 20 };
        }

        public HorizontalCoordinates ToAltAz(DateTime utc, Angle rightAscension, Angle declination)
        {
            var lst = Clock.ToSiderealTime(utc);
            var hourAngle = (Angle.FromHours(lst.TotalHours) - rightAscension).Constrain();       

            var geocentric = EarthBasedObserver.ToAltAz(hourAngle, declination, Latitude);
            //var parallaxAdjusted = GeocentricParallax.Distort(geocentric, 1e10);
            return AtmosphericRefraction.Distort(geocentric);
        }

        public static HorizontalCoordinates ToAltAz(Angle ha, Angle dec, Angle lat)
        {
            double sinAlt = Math.Sin(dec.Radians) * Math.Sin(lat.Radians) + Math.Cos(dec.Radians) * Math.Cos(lat.Radians) * Math.Cos(ha.Radians);
            double alt = Math.Asin(sinAlt);
            double cosAz = (Math.Sin(dec.Radians) - Math.Sin(alt) * Math.Sin(lat.Radians)) / (Math.Cos(alt) * Math.Cos(lat.Radians));
            double az = Math.Acos(cosAz);

            //If sin(HA) is negative, then AZ = A, otherwise AZ = 360 - A
            if (Math.Sin(ha.Radians) >= 0)
            {
                az = Angle.TwoPi - az;
            }

            return new HorizontalCoordinates(Angle.FromRadians(alt), Angle.FromRadians(az));
        }

    }
}