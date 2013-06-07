using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public struct GeographicCoordinates
    {
        public Angle Latitude;
        public Angle Longitude;

        public GeographicCoordinates(Angle latitude, Angle longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
