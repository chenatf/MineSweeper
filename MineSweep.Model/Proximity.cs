using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MineSweep.Model
{
    [DataContract]
    public sealed class Proximity:
        Cell
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
        }
    }
}
