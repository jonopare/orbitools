using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class BodyEventArgs : EventArgs
    {
        public Body Body { get; private set; }

        public BodyEventArgs(Body body)
        {
            Body = body;
        }
    }
}
