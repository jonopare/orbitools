using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Orbitools
{
    class Program
    {
        //http://visibleearth.nasa.gov/view_cat.php?categoryID=1484
        static void Main(string[] args)
        {
            var origin = new Triplet(0, 0, 0);
            var earthRadius = 6.371e6;
            var surfacePosition = new Triplet(earthRadius, 0, 0);
            var orbitPosition = new Triplet(earthRadius + (4.1e5 + 4.17e5) / 2, 0, 0);

            var orbitalInclination = 51.6 * Math.PI / 180;
            var orbitalDirection = new Triplet(0, 1, Math.Tan(orbitalInclination)).Unit;
                        
            var earth = new BodyBuilder().Mass(5.972e24).Position(origin).ToBody();
            var human = new BodyBuilder().Mass(95).Position(surfacePosition).ToBody();
            var iss = new BodyBuilder().Mass(4.5e5).Position(orbitPosition).Speed(7.7066e3).Direction(orbitalDirection).ToBody();

            var surface = human.Gravity(earth);
            
            var orbit = iss.Gravity(earth);
                        
            var surfaceAcceleration = surface.Magnitude / human.Mass;
            var orbitAcceleration = orbit.Magnitude / iss.Mass;

            var maxSamples = 32000;
            var period = 92 * 60 + 50;
            var sampleRate = (int)(maxSamples / period);

            using (var bitmap = new Bitmap(@"world_200412_3x5400x2700.jpg"))
            //using (var bitmap = new Bitmap(@"world_200412_3x1280x640.jpg"))
            //using (var graphics = Graphics.FromImage(bitmap))
            {
                //Pen p = new Pen(Brushes.Yellow, 3);

                var px = bitmap.Width / Math.PI / 2;
                for (int s = 0; s < sampleRate * period * 3; s++)
                {
                    iss.ApplyForce(iss.Gravity(earth), TimeSpan.FromSeconds(1d / sampleRate));

                    var sp = Coordinates.ToSpherical(iss.Position);

                    // factor in the rotating earth
                    var xd = 2 * Math.PI * s / sampleRate / TimeSpan.FromHours(24).TotalSeconds;

                    var x = (sp.Item3 + xd) * px;
                    var y = sp.Item2 * px;

                    x = (x + bitmap.Width) % bitmap.Width;
                    y = (bitmap.Height - y) % bitmap.Height;

                    bitmap.SetPixel((int)x, (int)y, Color.Yellow);

                    //graphics.DrawLine(p, (int)x, (int)y, (int)x + 1, (int)y + 1);
                }
                bitmap.Save(@"groundtrack.jpg");
            }

            //Console.WriteLine("Period,{0},SampleRate,{1}", period, sampleRate);
            //Console.WriteLine("Sam,X,Y,Z,Alt,Vel");
            //for (int s = 0; s < sampleRate * period; s++)
            //{
            //    iss.ApplyForce(iss.Gravity(earth), TimeSpan.FromSeconds(1d / sampleRate));

            //    Console.Write(s);
            //    Console.Write(",");
            //    Console.Write(iss.Position.X);
            //    Console.Write(",");
            //    Console.Write(iss.Position.Y);
            //    Console.Write(",");
            //    Console.Write(iss.Position.Z);
            //    Console.Write(",");
            //    Console.Write(iss.Position.Magnitude);
            //    Console.Write(",");
            //    Console.Write(iss.Velocity.Magnitude);
                
            //    var sph = Coordinates.ToSpherical(iss.Position);
            //    //Console.Write(",");
            //    //Console.Write(sph.Item1);
            //    Console.Write(",");
            //    Console.Write(sph.Item2);
            //    Console.Write(",");
            //    Console.Write(sph.Item3);

            //    Console.WriteLine();
            //}

            //var issSpeed = 7.7066e3;


            ////1 N = 1 kg·m/s2

            //// F1 = F2 = G(m1m2)/r2

            //var r = 6371000d; //m
            //var m1 = 1; //kg
            //var m2 = 5.972e24; // kg

            ////
            //// F = 9.8m/s2

            ////F = G(m1m2)/r2
            ////F(r2)/m1m2 = G



            //var G = 9.8 * (r * r) / (m1 * m2);

            ////-0.000 000 000 000 003 4377364468854659
            //// 0.000 000 000 066 607 247454789016
            ////var Gx = -3.4377364468854659e-15;
            //var Gx = 6.6607247454789016e-11;


            //var Gw = 6.674e-11; //N(m/kg)2
            //var F = Gw * (m1 * m2) / (r * r);

            ////1 N = 1 kg·m/s2
        }
    }
}
