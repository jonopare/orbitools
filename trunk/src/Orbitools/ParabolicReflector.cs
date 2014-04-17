using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class ParabolicReflector
    {
        public double FocalLength { get; private set; }
        public double Diameter { get; private set; }

        private Triplet[] normals;
        private Triplet[] surfaces;
        
        public ParabolicReflector(double focalLength, double diameter)
        {
            FocalLength = focalLength;
            Diameter = diameter;
        }

        public double Radius
        {
            get { return Diameter / 2; }
        }

        public double FocalRatio
        {
            get { return FocalLength / Diameter; }
        }

        public double SurfaceZ(double r)
        {
            return r * r / 4 / FocalLength;
        }

        public double SurfaceDzDr(double r)
        {
            return r / 2 / FocalLength;
        }

        public double SurfaceToDirectrix(double r)
        {
            return SurfaceZ(r) + FocalLength;
        }

        public double SurfaceToFocus(double r)
        {
            var z = FocalLength - SurfaceZ(r);
            return Math.Sqrt(z * z + r * r);
        }

        /// <summary>
        /// Reflected angle of 2D incoming ray parallel to primary axis.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public double ReflectedAngle(double r)
        {
            return -1 / Math.Tan(2 * Math.Atan2(SurfaceDzDr(r), 1));
        }

        /// <summary>
        /// Component of reflected angle of 2D incoming ray.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public double ReflectedAngle(double r, Angle angle)
        {
            return -1 / Math.Tan(2 * Math.Atan2(SurfaceDzDr(r), 1) - angle.Radians);
        }

        public Triplet AxisOfRefection(double x, double y)
        {
            var radius = Math.Sqrt(x * x + y * y);
            var gradient = SurfaceDzDr(radius); //ReflectedAngle(radius);
            var clockwise = Math.Atan2(y, x);
            return new Triplet(0 - 1, 0, 1 / gradient).RotateZ(Angle.FromRadians(0-clockwise));
        }

        public static Triplet Reflect(Triplet incident, Triplet normal)
        {
            var n = normal.Unit; // the normal should always be unit on the reflective surface
            return incident - 2 * incident.Dot(n) * n;
        }

        public IEnumerable<Tuple<Triplet, Triplet>> Surface(int halfResolution)
        {
            var scale = Radius / halfResolution;
            for (int y = 0 - halfResolution; y <= halfResolution; y++)
            {
                for (int x = 0 - halfResolution; x <= halfResolution; x++)
                {
                    var pointRadius = Math.Sqrt(y * y + x * x);
                    if (pointRadius <= halfResolution)
                    {
                        var surface = new Triplet(scale * x, scale * y, SurfaceZ(scale * pointRadius));
                        var normal = AxisOfRefection(scale * x, scale * y);
                        yield return new Tuple<Triplet, Triplet>(surface, normal);
                    }
                }
            }
        }
    }
}
