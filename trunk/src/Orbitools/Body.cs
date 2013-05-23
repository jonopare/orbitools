using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class Body
    {
        public readonly static double G = 6.674e-11;

        public double Mass { get; private set; }
        public Triplet Position { get; private set; }
        public Triplet Velocity { get; private set; }

        public Body(double mass, Triplet position, Triplet velocity)
        {
            Mass = mass;
            Position = position;
            Velocity = velocity;
        }

        public Triplet Gravity(Body other)
        {
            var d = other.Position - this.Position;
            var f = G * (this.Mass * other.Mass) / Math.Pow(d.Magnitude, 2);
            return f * d.Unit;
        }

        public static Triplet[] Gravity(Body[] bodies)
        {
            var forces = new Triplet[bodies.Length];
            for (int i = 0; i < bodies.Length; i++)
            {
                for (int j = i + 1; j < bodies.Length; j++)
                {
                    var f = bodies[i].Gravity(bodies[j]);
                    forces[i] += f;
                    forces[j] -= f;
                }
            }
            return forces;
        }

        /// <summary>
        /// (kg*m)/s    or    N*s
        /// </summary>
        public Triplet Momentum
        {
            get
            {
                return Mass * Velocity;
            }
        }

        public void ApplyForce(Triplet force, TimeSpan duration)
        {
            var movement = Velocity * duration.TotalSeconds;
            if (force.Magnitude != 0)
            {
                movement += force.Unit * (force.Magnitude / Mass * Math.Pow(duration.TotalSeconds, 2));
            }
            Position += movement;
            Velocity = movement / duration.TotalSeconds;
        }
    }
}
