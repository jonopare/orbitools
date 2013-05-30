using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class BodyBuilder
    {
        private Triplet position;
        private Triplet direction;
        private double speed;
        private double mass;

        public BodyBuilder Mass(double mass)
        {
            this.mass = mass;
            return this;
        }

        public BodyBuilder Speed(double speed)
        {
            this.speed = speed;
            return this;
        }

        public BodyBuilder Direction(Triplet direction)
        {
            this.direction = direction.Unit;
            return this;
        }

        public BodyBuilder Direction(double x, double y, double z)
        {
            return Direction(new Triplet(x, y, z));
        }

        public BodyBuilder Position(Triplet position)
        {
            this.position = position;
            return this;
        }

        public BodyBuilder Position(double x, double y, double z)
        {
            return Position(new Triplet(x, y, z));
        }

        public BodyBuilder Rotate(double clockwise)
        {
            var s = Math.Sin(clockwise);
            var c = Math.Cos(clockwise);
            return Position(position.X * c - position.Y * s, position.X * s + position.Y * c, position.Z)
                .Direction(direction.X * c - direction.Y * s, direction.X * s + direction.Y * c, direction.Z);
        }

        public Body ToBody()
        {
            return new Body(mass, position, direction * speed);
        }

        public Body ToBody(string name)
        {
            return new Body(mass, position, direction * speed, name);
        }
    }
}
