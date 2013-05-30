using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class SolarSystem
    {
        public static NBodyModel v_0
        {
            get
            {
                var random = new Random();
                Func<double> r = () => 2 * Math.PI * random.NextDouble();
                var result = new NBodyModel()
                {
                    new BodyBuilder().Mass(1.989E+30).Position(0, 0, 0).ToBody("Sun"),
                    new BodyBuilder().Mass(3.302E+23).Position(57909175000, 0, 0).Speed(47873.1729201086).Direction(0, 1, 0).Rotate(r()).ToBody("Mercury"),
                    new BodyBuilder().Mass(4.869E+24).Position(108208930000, 0, 0).Speed(35021.4598038072).Direction(0, 1, 0).Rotate(r()).ToBody("Venus"),
                    new BodyBuilder().Mass(5.9742E+24).Position(149597890000, 0, 0).Speed(29785.4006434612).Direction(0, 1, 0).Rotate(r()).ToBody("Earth"),
                    new BodyBuilder().Mass(6.4191E+23).Position(227936640000, 0, 0).Speed(24129.3748122509).Direction(0, 1, 0).Rotate(r()).ToBody("Mars"),
                    new BodyBuilder().Mass(1.8987E+27).Position(778412010000, 0, 0).Speed(13065.1568256159).Direction(0, 1, 0).Rotate(r()).ToBody("Jupiter"),
                    new BodyBuilder().Mass(5.6851E+26).Position(1426725400000, 0, 0).Speed(9646.67232611922).Direction(0, 1, 0).Rotate(r()).ToBody("Saturn"),
                    new BodyBuilder().Mass(8.6849E+25).Position(2870972200000, 0, 0).Speed(6803.74687553784).Direction(0, 1, 0).Rotate(r()).ToBody("Uranus"),
                    new BodyBuilder().Mass(1.0244E+26).Position(4498252900000, 0, 0).Speed(5434.94396558672).Direction(0, 1, 0).Rotate(r()).ToBody("Neptune"),
                };

                var earth = result.Single(b => b.Name == "Earth");
                result.Add(new BodyBuilder().Mass(7.34767309E+22).Position(earth.Position.X + 3.844e8, earth.Position.Y, earth.Position.Z).Speed(earth.Velocity.Magnitude + 1023.16025670753).Direction(0, 1, 0).ToBody("Moon"));

                return result;
            }
        }
    }
}
