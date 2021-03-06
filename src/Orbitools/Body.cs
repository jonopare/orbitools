﻿using System;
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
        public string Name { get; private set; }

        public Body(double mass, Triplet position, Triplet velocity)
        {
            Mass = mass;
            Position = position;
            Velocity = velocity;
        }

        public Body(double mass, Triplet position, Triplet velocity, string name)
            :this(mass, position, velocity)
        {
            Name = name;
        }

        public Triplet Gravity(Body other)
        {
            var d = other.Position - this.Position;
            if (d.Length == 0)
            {
                return new Triplet();
            }
            var f = G * (this.Mass * other.Mass) / d.LengthSquared;
            return f * d.Unit;
        }

        public static IList<Triplet> Gravity(IList<Body> bodies)
        {
            var forces = new Triplet[bodies.Count];
            for (int i = 0; i < bodies.Count; i++)
            {
                for (int j = i + 1; j < bodies.Count; j++)
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
            if (force.Length != 0)
            {
                movement += force.Unit * (force.Length / Mass * Math.Pow(duration.TotalSeconds, 2));
            }
            Position += movement;
            Velocity = movement / duration.TotalSeconds;
        }

        /// <summary>
        /// Mass ejection with conservation of momentum results in a new ejected body
        /// moving away from this body and imparts motion to this body in proportion
        /// to its new mass and the mass of the ejection.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="velocity"></param>
        /// <returns></returns>
        public Body Eject(double mass, Triplet velocity)
        {
            if (mass > Mass)
            {
                throw new ArgumentOutOfRangeException("mass", "Ejected mass cannot exceed the mass of this body");
            }

            var result = new Body(mass, Position, velocity);
            Mass -= mass;
            Velocity -= result.Momentum / Mass;
            return result;
        }

        public bool IsInvalid
        {
            get
            {
                return double.IsNaN(Position.LengthSquared) || double.IsNaN(Velocity.LengthSquared);
            }
        }
    }
}
