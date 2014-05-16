using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gravity
{
    public static class Constants
    {
        public const double GravitationalConstant = 6.67e-11;
        
        public static double Mu(double mass)
        {
            return GravitationalConstant * mass;
        }
    }
}
