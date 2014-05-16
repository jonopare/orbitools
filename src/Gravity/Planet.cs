using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gravity
{
    public class Planet : IBody
    {
        public double StandardGravitationalParameter { get; private set; }

        public Vector3 Position { get; private set; }
        public Vector3 Velocity { get; private set; }

        public double OrbitalPeriod { get; private set; }

        public Planet(double standardGravitationalParameter, Vector3 position, double orbitalPeriod = 0)
        {
            StandardGravitationalParameter = standardGravitationalParameter;
            Position = position;
            OrbitalPeriod = orbitalPeriod;
            Velocity = Position.Length * Math.Sin(AngularVelocity) * new Vector3(0, 1, 0);
        }

        public double Mass
        {
            get { return 1e9 * StandardGravitationalParameter / Constants.GravitationalConstant; }
        }

        public Vector3 F(IBody other)
        {
            var r = other.Position - this.Position;
            return 1e9 * this.StandardGravitationalParameter * 1e9 * other.StandardGravitationalParameter / Constants.GravitationalConstant / r.LengthSquared * r.Unit;
        }

        public void Apply(Vector3 force, double seconds)
        {
            // v = u + at
            // s = ut + 0.5at^2
            // f = ma
            var a = force / Mass / 1e9; // m/s/s

            var s = Velocity * seconds + 0.5 * seconds * seconds * a;
            Position += s;
            var deltaV = a * seconds;
            Velocity += deltaV;
        }

        public double AngularVelocity
        {
            get { return Math.PI * 2 / OrbitalPeriod / 24 / 60 / 60; }
        }

        public Vector3 Momentum
        {
            get { return Mass * Velocity; }
        }
    }
}
