using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{

    public class Spheroid
    {
        /// <summary>
        /// http://en.wikipedia.org/wiki/World_Geodetic_System
        /// </summary>
        public readonly static Spheroid WGS84 = new Spheroid(6378137, 1 / 298.257223563);

        public readonly static Spheroid UnitSphere = new Spheroid(1, 1);

        public double TransverseRadius { get; private set; }
        public double Flattening { get; private set; }

        public double ConjugateRadius
        {
            get
            {
                return TransverseRadius * (1 - Flattening);
            }
        }

        public bool IsOblate
        {
            get
            {
                return TransverseRadius > ConjugateRadius;
            }
        }

        public bool IsProlate
        {
            get
            {
                return TransverseRadius < ConjugateRadius;
            }
        }

        /// <summary>
        /// Construct with semi-major axis as transverse radius for oblate, or semi-minor for prolate.
        /// </summary>
        /// <param name="transverseRadius"></param>
        /// <param name="flattening"></param>
        public Spheroid(double transverseRadius, double flattening)
        {
            TransverseRadius = transverseRadius;
            Flattening = flattening;
        }

        public double Radius(double latitude)
        {
            var numerator = Math.Pow(Math.Pow(TransverseRadius, 2) * Math.Cos(latitude), 2) + Math.Pow(Math.Pow(ConjugateRadius, 2) * Math.Sin(latitude), 2);
            var denominator = Math.Pow(TransverseRadius * Math.Cos(latitude), 2) + Math.Pow(ConjugateRadius * Math.Sin(latitude), 2);
            return Math.Sqrt(numerator / denominator);
        }

        public Triplet FromLatLon(double latitude, double longitude)
        {
            var r = Radius(latitude);
            return new Triplet(r * Math.Cos(latitude) * Math.Cos(longitude), r * Math.Cos(latitude) * Math.Sin(longitude), r * Math.Sin(latitude));
        }

        public void FromXYZ(Triplet xyz)
        {
            var lat = Math.Asin(xyz.Z / xyz.Magnitude);
            var lon = Math.Atan2(xyz.Y, xyz.X);
        }

        public IEnumerable<Triplet> Vertices(int divisions = 360)
        {
            var rads = Enumerable.Range(0, divisions / 4).Select(d => 2 * Math.PI * d / divisions).ToArray();

            yield return FromLatLon(Math.PI / 2, 0); // north pole

            for (int lat = rads.Length - 1; lat > 0 - rads.Length; lat--)
            {
                var radLat = Math.Sign(lat) * rads[Math.Abs(lat)];
                for (int q = 0; q < 4; q++)
                {
                    for (int lon = 0; lon < rads.Length; lon++)
                    {
                        yield return FromLatLon(radLat, rads[lon] + q * Math.PI / 2);
                    }
                }
            }

            yield return FromLatLon(0 - Math.PI / 2, 0); // south pole
        }
    }
}
