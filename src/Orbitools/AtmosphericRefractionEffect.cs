using Extellect.Utilities.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class AtmosphericRefractionEffect
    {
        public double TempDegreesC { get; set; }
        public double BarometricPressureMillibars { get; set; }

        private Angle thresholdHorizontalAltitude;

        public AtmosphericRefractionEffect()
        {
            thresholdHorizontalAltitude = Angle.FromDegrees(15);
        }

        public HorizontalCoordinates Distort(HorizontalCoordinates vector)
        {
            return vector.Alt > thresholdHorizontalAltitude ? UpperAltitudeDistortion(vector) : LowerAltitudeDistortion(vector);
        }

        /// <summary>
        /// From Practical Astronomy with your Calculator:
        /// "This formula is usually accurate to about 6 arcsec for altitudes greater than 15 degrees."
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        internal HorizontalCoordinates UpperAltitudeDistortion(HorizontalCoordinates vector)
        {
            var z = Angle.PiOverTwo - vector.Alt;
            var R = 0.00452 * BarometricPressureMillibars * Math.Tan(z.Radians) / (273 + TempDegreesC);
            return new HorizontalCoordinates(vector.Alt + Angle.FromDegrees(R), vector.Az);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        internal HorizontalCoordinates LowerAltitudeDistortion(HorizontalCoordinates vector)
        {
            var a = vector.Alt.Degrees; // strictly, it's the apparent altitude as measured through the atmosphere
            var R = BarometricPressureMillibars * (0.1594 + 0.0196 * a + 0.00002 * a * a) / (273 + TempDegreesC) / (1 + 0.505 * a + 0.0845 * a * a);
            return new HorizontalCoordinates(vector.Alt + Angle.FromDegrees(R), vector.Az);
        }
    }
}
