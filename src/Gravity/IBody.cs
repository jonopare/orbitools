using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gravity
{
    public interface IBody
    {
        double StandardGravitationalParameter { get; }
        Vector3 Position { get; }
        Vector3 Velocity { get; }
        double Mass { get; }
        Vector3 Momentum { get; }
        Vector3 F(IBody other);
        void Apply(Vector3 force, double seconds);
    }
}
