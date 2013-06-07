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

        /// <summary>
        /// Rotates clockwise around the Z axis
        /// </summary>
        /// <param name="clockwise"></param>
        /// <returns></returns>
        public BodyBuilder RotateZ(Angle clockwise)
        {
            var s = Math.Sin(clockwise.Radians);
            var c = Math.Cos(clockwise.Radians);
            return Position(position.X * c - position.Y * s, position.X * s + position.Y * c, position.Z)
                .Direction(direction.X * c - direction.Y * s, direction.X * s + direction.Y * c, direction.Z);
        }

        /// <summary>
        /// Rotates clockwise around the Y axis
        /// </summary>
        /// <param name="clockwise"></param>
        /// <returns></returns>
        public BodyBuilder RotateY(Angle clockwise)
        {
            var s = Math.Sin(clockwise.Radians);
            var c = Math.Cos(clockwise.Radians);
            return Position(position.X * c - position.Z * s, position.Y, position.X * s + position.Z * c)
                .Direction(direction.X * c - direction.Z * s, position.Y, direction.X * s + direction.Z * c);
        }

        //public BodyBuilder RotateY(Angle clockwise)
        //{
        //    var s = Math.Sin(clockwise.Radians);
        //    var c = Math.Cos(clockwise.Radians);
        //    return Position(position.X * s + position.Z * c, position.Y, position.X * c - position.Z * s)
        //        .Direction(direction.X * s + direction.Z * c, direction.Y, direction.X * c - direction.Z * s);
        //}

        //public BodyBuilder RotateX(Angle clockwise)
        //{
        //    var s = Math.Sin(clockwise.Radians);
        //    var c = Math.Cos(clockwise.Radians);
        //    return Position(position.X, position.Y * c - position.Z * s, position.Y * s + position.Z * c)
        //        .Direction(direction.X, direction.Y * c - direction.Z * s, direction.Y * s + direction.Z * c);
        //}

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
