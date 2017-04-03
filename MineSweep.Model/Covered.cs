using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MineSweep.Model
{
    [DataContract]
    public class Covered:
        Cell, IEquatable<Covered>
    {
        public Covered(int x, int y):
            base(x, y)
        {
        }

        public override bool Equals(object obj)
        {
            switch(obj)
            {
            case Covered rhs:
                return Equals(rhs);
            default:
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Covered other)
        {
            return base.Equals(other);
        }
    }
}
