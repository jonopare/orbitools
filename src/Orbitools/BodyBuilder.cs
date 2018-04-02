using Extellect.Utilities.Math;
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

        public static Triplet RotateZ(Triplet triplet, Angle clockwise)
        {
            var s = Math.Sin(clockwise.Radians);
            var c = Math.Cos(clockwise.Radians);
            return new Triplet(triplet.X * c + triplet.Y * s, triplet.Y * c - triplet.X * s, triplet.Z);
        }

        public static Triplet RotateY(Triplet triplet, Angle clockwise)
        {
            var s = Math.Sin(clockwise.Radians);
            var c = Math.Cos(clockwise.Radians);
            return new Triplet(triplet.X * c + triplet.Z * s, triplet.Y, triplet.Z * c - triplet.X * s);
        }

        public static Triplet RotateX(Triplet triplet, Angle clockwise)
        {
            var s = Math.Sin(clockwise.Radians);
            var c = Math.Cos(clockwise.Radians);
            return new Triplet(triplet.X, triplet.Z * s + triplet.Y * c, triplet.Z * c - triplet.Y * s);
        }

        public BodyBuilder RotateZ(Angle clockwise)
        {
            return Position(RotateZ(position, clockwise)).Direction(RotateZ(direction, clockwise));
        }

        public BodyBuilder RotateY(Angle clockwise)
        {
            return Position(RotateY(position, clockwise)).Direction(RotateY(direction, clockwise));
        }

        public BodyBuilder RotateX(Angle clockwise)
        {
            return Position(RotateX(position, clockwise)).Direction(RotateX(direction, clockwise));
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
