using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MineSweep.Model
{
    [DataContract]
    public sealed class Empty:
        Cell, IEquatable<Empty>
    {
        public Empty(int x, int y) : base(x, y)
        {
        }

        public override bool Equals(object obj)
        {
            switch(obj)
            {
            case Empty rhs:
                return Equals(rhs);
            default:
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Empty other)
        {
            return base.Equals(other);
        }


    }
}
