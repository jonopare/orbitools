using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class EarthBasedObserver
    {
        //public Angle Latitude { get; set; }
        //public Angle Longitude { get; set; }

        //public SiderealClock Clock { get; set; }
        public GeocentricParallaxEffect GeocentricParallax { get; set; }
        public AtmosphericRefractionEffect AtmosphericRefraction { get; set; }

        public EarthBasedObserver()
        {
            
            //GeocentricParallax = GeocentricParallaxEffect.Create(latitude, longitude, 100);
            //AtmosphericRefraction = new AtmosphericRefractionEffect { BarometricPressureMillibars = 1024, TempDegreesC = 16 };
        }

        public HorizontalCoordinates ToAltAz(HorizontalCoordinates position, double distance)
        {
            if (GeocentricParallax != null)
            {
                position = GeocentricParallax.Distort(position, distance);
            }
            if (AtmosphericRefraction != null)
            {
                position = AtmosphericRefraction.Distort(position);
            }
            return position;
        }

        //public static HorizontalCoordinates ToAltAz(EquatorialCoordinates vector, Angle lat)
        //{
            
        //}

    }
}