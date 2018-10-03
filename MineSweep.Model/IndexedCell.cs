using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweep.Model
{
    public sealed class IndexedCell
    {
        public int X { get; }
        public int Y { get; }
        public Cell Cell { get; }

        public IndexedCell(int x, int y, Cell cell)
        {
            X = x;
            Y = y;
            Cell = cell;
        }
    }
}
