using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MineSweep.Model
{
    [DataContract]
    public sealed class EmptyCell:
        Cell
    {
        public EmptyCell(int x, int y) : base(x, y)
        {
        }
    }
}
