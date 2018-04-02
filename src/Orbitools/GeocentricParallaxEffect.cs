using Extellect.Utilities.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    /// <summary>
    /// This effect is very important for correcting vectors to close-up objects such
    /// as artificial satellites and the Moon. I expect the class could be generalised
    /// for reuse as the logic of stellar parallax is similar but where the Earth's orbit takes it 
    /// away from the center of the Solar System.
    /// </summary>
    public class GeocentricParallaxEffect
    {
        public double GeocentricDistanceToObserver { get; set; }

        //public GeocentricParallaxEffect(double geocentricDistanceToObserver)
        //{
        //    GeocentricDistanceToObserver = geocentricDistanceToObserver;
        //}

        //public static GeocentricParallaxEffect Create(Angle latitude, Angle longitude, double amsl)
        //{
        //    var radius = Spheroid.WGS84.FromLatLon(latitude.Radians, longitude.Radians).Magnitude;
        //    return new GeocentricParallaxEffect(radius + amsl);
        //}

        /// <summary>
        /// I decided to write my own parallax adjustment instead of taking it from the Practical Astronomy book.
        /// Let c be the center of the Earth (center of mass)
        /// Let o be an observer at the surface of the Earth
        /// Let m be the moon, which the observer is observing.
        /// Let z be the observer's zenith.
        /// The line CZ passes through O.
        /// The plane OCM will never change and this means the azimuth won't change either.
        /// The "horizontal" plane perpendicular to OC just needs to shift from C to O, with an accompanying change in altitude
        /// </summary>
        /// <param name="vector">Alt-az vector to the subject.</param>
        /// <param name="geocentricDistance">Distance in meters from the center of the earth to the subject.</param>
        /// <returns></returns>
        public HorizontalCoordinates Distort(HorizontalCoordinates vector, double geocentricDistance)
        {
            var z = Math.Sin(vector.Alt.Radians) * geocentricDistance;
            var h = Math.Cos(vector.Alt.Radians) * geocentricDistance;
            var x = Math.Sin(vector.Az.Radians) * h;
            var y = Math.Cos(vector.Az.Radians) * h;

            z -= GeocentricDistanceToObserver;

            var r = Math.Sqrt(x * x + y * y + z * z);

            //var az = Math.Atan2(x, y);
            var alt = Math.Asin(z / r);

            return new HorizontalCoordinates(Angle.FromRadians(alt), vector.Az/*Angle.FromRadians(az)*/);
        }

        [Obsolete("Untested")]
        public EquatorialCoordinates Distort(EquatorialCoordinates vector, double geocentricDistance)
        {
            var z = Math.Sin(vector.Dec.Radians) * geocentricDistance;
            var h = Math.Cos(vector.Dec.Radians) * geocentricDistance;
            var x = Math.Sin(vector.RA.Radians) * h;
            var y = Math.Cos(vector.RA.Radians) * h;

            z -= GeocentricDistanceToObserver;

            var r = Math.Sqrt(x * x + y * y + z * z);

            var dec = Math.Atan2(x, y);
            var ra = Math.Asin(z / r);

            return new EquatorialCoordinates(Angle.FromRadians(ra), Angle.FromRadians(dec));
        }
    }
}
