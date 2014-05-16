using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gravity
{
    class Program
    {
        static void Main(string[] args)
        {
            var fs = new []
                {
                    SolarSystem.Mercury.F(SolarSystem.Sun),
                    SolarSystem.Venus.F(SolarSystem.Sun),
                    SolarSystem.Earth.F(SolarSystem.Sun),
                    SolarSystem.Mars.F(SolarSystem.Sun),
                };

            

        }
    }
}
