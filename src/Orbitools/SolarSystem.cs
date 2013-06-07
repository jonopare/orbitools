using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class SolarSystem
    {
        public static NBodyModel v0_2
        {
            get
            {
                var result = new NBodyModel()
                {
                    new BodyBuilder().Mass(1.9891E+30).ToBody("Sun"),
                    new BodyBuilder().Mass(5.9736E+24).Position(147098291000,0,0).Speed(30286.6658479136).Direction(0,-1,0).RotateZ(Angle.FromDegrees(90)).RotateY(Angle.FromDegrees(23.4)).ToBody("Earth"),
                };
                return result;
            }
        }

        public static NBodyModel v_1
        {
            get
            {
                var random = new Random();
                Func<Angle> r = () => Angle.TwoPi * random.NextDouble();
                var result = new NBodyModel()
                {
                    new BodyBuilder().Mass(1.9891E+30).ToBody("Sun"),
                    new BodyBuilder().Mass(3.302E+23).Position(46001009000,0,0).Speed(58976.6725489771).Direction(0,1,0).RotateZ(r()).ToBody("Mercury"),
                    new BodyBuilder().Mass(4.8685E+24).Position(107476170000,0,0).Speed(35258.7441501087).Direction(0,1,0).RotateZ(r()).ToBody("Venus"),
                    new BodyBuilder().Mass(5.9736E+24).Position(147098291000,0,0).Speed(30286.6658479136).Direction(0,1,0).RotateZ(r()).ToBody("Earth"),
                    new BodyBuilder().Mass(6.4185E+23).Position(206655215000,0,0).Speed(26498.4862837966).Direction(0,1,0).RotateZ(r()).ToBody("Mars"),
                    new BodyBuilder().Mass(1.8986E+27).Position(740679835000,0,0).Speed(13712.2398529094).Direction(0,1,0).RotateZ(r()).ToBody("Jupiter"),
                    new BodyBuilder().Mass(5.6846E+26).Position(1349823615000,0,0).Speed(10180.5488090601).Direction(0,1,0).RotateZ(r()).ToBody("Saturn"),
                    new BodyBuilder().Mass(8.6832E+25).Position(2734998229000,0,0).Speed(7128.75188586689).Direction(0,1,0).RotateZ(r()).ToBody("Uranus"),
                    new BodyBuilder().Mass(1.0243E+26).Position(4459753056000,0,0).Speed(5478.59288641191).Direction(0,1,0).RotateZ(r()).ToBody("Neptune"),
                    new BodyBuilder().Mass(9.5E+20).Position(380951528000,0,0).Speed(19389.1825873945).Direction(0,1,0).RotateZ(r()).ToBody("Ceres"),
                    new BodyBuilder().Mass(1.3105E+22).Position(4436756954000,0,0).Speed(6111.87116762944).Direction(0,1,0).RotateZ(r()).ToBody("Pluto"),
                    new BodyBuilder().Mass(3E+21).Position(5671928586000,0,0).Speed(5218.40769509739).Direction(0,1,0).RotateZ(r()).ToBody("Makemake"),
                    new BodyBuilder().Mass(4.006E+21).Position(5157623774000,0,0).Speed(5552.43307194022).Direction(0,1,0).RotateZ(r()).ToBody("Haumea"),
                    new BodyBuilder().Mass(1.67E+22).Position(5765732799000,0,0).Speed(5744.42886442385).Direction(0,1,0).RotateZ(r()).ToBody("Eris"),
                    new BodyBuilder().Mass(220000000000000).Position(87875040000.0001,0,0).Speed(54503.6050789003).Direction(0,1,0).RotateZ(r()).ToBody("Halleys"),
                    //new BodyBuilder().Mass(7.35E+22).Position(362600000,0,0).Speed(1083.89165904698).Direction(0,1,0).ToBody("Moon"),

                };
                return result;
            }
        }

        public static NBodyModel v_0
        {
            get
            {
                var random = new Random();
                Func<Angle> r = () => Angle.TwoPi * random.NextDouble();
                var result = new NBodyModel()
                {
                    new BodyBuilder().Mass(1.989E+30).Position(0, 0, 0).ToBody("Sun"),
                    new BodyBuilder().Mass(3.302E+23).Position(57909175000, 0, 0).Speed(47873.1729201086).Direction(0, 1, 0).RotateZ(r()).ToBody("Mercury"),
                    new BodyBuilder().Mass(4.869E+24).Position(108208930000, 0, 0).Speed(35021.4598038072).Direction(0, 1, 0).RotateZ(r()).ToBody("Venus"),
                    new BodyBuilder().Mass(5.9742E+24).Position(149597890000, 0, 0).Speed(29785.4006434612).Direction(0, 1, 0).RotateZ(r()).ToBody("Earth"),
                    new BodyBuilder().Mass(6.4191E+23).Position(227936640000, 0, 0).Speed(24129.3748122509).Direction(0, 1, 0).RotateZ(r()).ToBody("Mars"),
                    new BodyBuilder().Mass(1.8987E+27).Position(778412010000, 0, 0).Speed(13065.1568256159).Direction(0, 1, 0).RotateZ(r()).ToBody("Jupiter"),
                    new BodyBuilder().Mass(5.6851E+26).Position(1426725400000, 0, 0).Speed(9646.67232611922).Direction(0, 1, 0).RotateZ(r()).ToBody("Saturn"),
                    new BodyBuilder().Mass(8.6849E+25).Position(2870972200000, 0, 0).Speed(6803.74687553784).Direction(0, 1, 0).RotateZ(r()).ToBody("Uranus"),
                    new BodyBuilder().Mass(1.0244E+26).Position(4498252900000, 0, 0).Speed(5434.94396558672).Direction(0, 1, 0).RotateZ(r()).ToBody("Neptune"),
                };

                var earth = result.Single(b => b.Name == "Earth");
                result.Add(new BodyBuilder().Mass(7.34767309E+22).Position(earth.Position.X + 3.844e8, earth.Position.Y, earth.Position.Z).Speed(earth.Velocity.Magnitude + 1023.16025670753).Direction(0, 1, 0).ToBody("Moon"));

                return result;
            }
        }
    }
}
