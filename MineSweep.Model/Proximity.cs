using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MineSweep.Model
{
    [DataContract]
    public sealed class Proximity:
        Cell, IEquatable<Proximity>
    {
        [DataMember(Name ="Count")]
        private readonly int _Count;
        [IgnoreDataMember]
        public int Count => _Count;
        public Proximity(int x, int y, int count):
            base(x, y)
        {
            if(count < 0 || count > 8)
            {
                throw new ArgumentException();
            }
            _Count = count;
        }
        public override int GetHashCode()
        {
            return CombineHash(base.GetHashCode(), Count.GetHashCode());
        }
        public override bool Equals(object obj)
        {
            var rhs = obj as Proximity;
            if(rhs == null)
                return false;
            else
                return Equals(rhs);
        }

        public bool Equals(Proximity other)
        {
            return
                Equals((Cell)other) &&
                Count == other.Count;
        }
    }
}
