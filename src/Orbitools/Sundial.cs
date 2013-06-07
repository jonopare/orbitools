using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class Sundial
    {
        public double PerpendicularStyleHeight { get; set; }

        /// <summary>
        /// Y+ve = North
        /// X+ve = East
        /// Y-ve = South
        /// X-ve = West
        /// </summary>
        public Triplet Shadow { get; private set; }

        public void Light(HorizontalCoordinates apparent)
        {
            if (apparent.Alt.Radians < 0)
            {
                throw new ArgumentException("Cannot light the sundial when the source is below the horizon.");
            }

            var w = 0 - PerpendicularStyleHeight / Math.Tan(apparent.Alt.Radians);

            Shadow = new Triplet(w * Math.Sin(apparent.Az.Radians), w * Math.Cos(apparent.Az.Radians), 0);
        }

    }
}
