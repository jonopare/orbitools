using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gravity
{
    public class SolarSystem
    {
        public readonly static IBody Sun = new Planet(132712440018, Vector3.Zero, 1);
        public readonly static IBody Mercury = new Planet(22032, new Vector3(58910000, 0, 0), 88.969);
        public readonly static IBody Venus = new Planet(324859, new Vector3(108200000, 0, 0), 224.7);
        public readonly static IBody Earth = new Planet(398600.4418, new Vector3(149600000, 0, 0), 365.25);
        //public readonly static IBody Moon = new Planet(4902.8);
        public readonly static IBody Mars = new Planet(42828, new Vector3(227900000, 0, 0), 687);
        //public readonly static IBody Ceres = new Planet(63.1);
        public readonly static IBody Jupiter = new Planet(126686534, new Vector3(778500000, 0, 0), 4333);
        public readonly static IBody Saturn = new Planet(37931187, new Vector3(1433000000, 0, 0), 10832);
        public readonly static IBody Uranus = new Planet(5793939, new Vector3(2877000000, 0, 0), 30667);
        public readonly static IBody Neptune = new Planet(6836529, new Vector3(4503000000, 0, 0), 60290);
        //public readonly static IBody Pluto = new Planet(871);
        //public readonly static IBody Eris = new Planet(1108);
    }
}
