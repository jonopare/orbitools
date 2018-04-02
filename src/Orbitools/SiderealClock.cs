using Extellect.Utilities.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    /// <summary>
    /// Implementation of a clock that converts from coordinated universal time to sidereal time.
    /// </summary>
    public class SiderealClock
    {
        /// <summary>
        /// Observer's longitude in degrees.
        /// </summary>
        public Angle Longitude { get; private set; }

        /// <summary>
        /// Returns true if this clock is based at Greenwich; false otherwise.
        /// False indicates that the time is local to the clock's longitude.
        /// </summary>
        public bool IsGreenwich { get { return Longitude.Fraction == 0; } }

        /// <summary>
        /// Constructs a new Greenwich based clock.
        /// </summary>
        public SiderealClock()
        {
        }

        /// <summary>
        /// Constructs a new local clock based at the specified longitude.
        /// </summary>
        /// <param name="longitude">Observer's longitude in degrees.</param>
        public SiderealClock(Angle longitude)
        {
            Longitude = longitude.Constrain(); 
        }

        public TimeSpan Now
        {
            get
            {
                return ToSiderealTime(DateTime.UtcNow);
            }
        }

        /// <summary>
        /// Algorithm taken from http://www.csgnetwork.com/siderealjuliantimecalc.html and tested against
        /// my own Celestron NexStar handset for validity.
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public TimeSpan ToSiderealTime(DateTime utc)
        {
            if (utc.Kind != DateTimeKind.Utc)
            {
                throw new NotSupportedException();
            }

            double DD = utc.Day;
            double MM = utc.Month;
            double YY = utc.Year;

            var HR = utc.TimeOfDay.TotalHours;
            DD = DD + HR / 24;
            double DY = Math.Floor(DD);
            if (MM < 3)
            {
                YY = YY - 1;
                MM = MM + 12;
            }

            double GR;
            if (YY + MM / 100 + DY / 10000 >= 1582.1015)
            {
                GR = 2 - Math.Floor(YY / 100) + Math.Floor(Math.Floor(YY / 100) / 4);
            }
            else
            {
                GR = 0;
            }

            double JD = Math.Floor(365.25 * YY) + Math.Floor(30.6001 * (MM + 1)) + DY + 1720994.5 + GR;
            double JD2 = JD + HR / 24;

            //        time sidereal 0h
            double T = (JD - 2415020) / 36525;
            double SS = 6.6460656 + 2400.051 * T + 0.00002581 * T * T;
            double ST = (SS / 24 - Math.Floor(SS / 24)) * 24;
            double GSTH = Math.Floor(ST);
            double GSTM = Math.Floor((ST - Math.Floor(ST)) * 60);
            double GSTS = ((ST - Math.Floor(ST)) * 60 - GSTM) * 60;

            //        time sidereal local
            double SA = ST + (DD - Math.Floor(DD)) * 24 * 1.002737908;
            SA = SA + Longitude.Hours;
            while (SA < 0)
            {
                SA = SA + 24;
            }
            while (SA >= 24)
            {
                SA = SA - 24;
            }

            return TimeSpan.FromHours(SA);
        }
    }
}
