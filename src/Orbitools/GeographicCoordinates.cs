using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Extellect.Utilities.Math;

namespace Orbitools
{
    [DebuggerDisplay("Lat={Latitude} Lon={Longitude}")]
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
