using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gravity
{
    public class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Length { get { return Math.Sqrt(LengthSquared); } }
        public double LengthSquared { get { return X * X + Y * Y + Z * Z; } }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3 operator /(Vector3 vector, double scalar)
        {
            return new Vector3(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
        }

        public static Vector3 operator *(Vector3 vector, double scalar)
        {
            return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        public static Vector3 operator *(double scalar, Vector3 vector)
        {
            return vector * scalar;
        }

        public readonly static Vector3 Zero = new Vector3(0, 0, 0);


        public Vector3 Unit
        {
            get { return this / Length; }
        }

        public override string ToString()
        {
            return new StringBuilder("X=").Append(X).Append(", Y=").Append(Y).Append(", Z=").Append(Z).ToString();
        }
    }


}
