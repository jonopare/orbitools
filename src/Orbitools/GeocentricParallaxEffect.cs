using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class GeocentricParallaxEffect
    {
        public double GeocentricDistanceToObserver { get; private set; }

        public GeocentricParallaxEffect(double geocentricDistanceToObserver)
        {
            GeocentricDistanceToObserver = geocentricDistanceToObserver;
        }

        public static GeocentricParallaxEffect Create(Angle latitude, Angle longitude, double amsl)
        {
            var radius = Spheroid.WGS84.FromLatLon(latitude.Radians, longitude.Radians).Magnitude;
            return new GeocentricParallaxEffect(radius + amsl);
        }

        /// <summary>
        /// Not taken from here:
        /// http://books.google.co.uk/books?id=MTGYxQyW998C&pg=PA83&lpg=PA83&dq=geocentric+parallax+practical+astronomy+spreadsheet&source=bl&ots=ULfLQxMnWH&sig=jjpLbLJ6A797iIkpufmAJUToWGs&hl=en&sa=X&ei=ROytUdbkEebB0gWgnYD4DQ&redir_esc=y#v=onepage&q=geocentric%20parallax%20practical%20astronomy%20spreadsheet&f=false
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="geocentricDistance"></param>
        /// <returns></returns>
        public HorizontalCoordinates Distort(HorizontalCoordinates vector, double geocentricDistance)
        {
            var x = geocentricDistance * Math.Cos(vector.Az.Radians) * Math.Sin(vector.Alt.Radians);
            var y = geocentricDistance * Math.Sin(vector.Az.Radians) * Math.Sin(vector.Alt.Radians);
            var z = geocentricDistance * Math.Cos(vector.Alt.Radians);

            x -= GeocentricDistanceToObserver;

            var r = Math.Sqrt(x * x + y * y + z * z);

            var theta = Math.Atan2(y, x);
            var phi = Math.Acos(z / r);

            return new HorizontalCoordinates(Angle.FromRadians(phi), Angle.FromRadians(theta));
        }
    }
}
