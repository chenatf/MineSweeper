using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweep.Model
{
    public class ExplosionEventArgs:
        EventArgs
    {
        public int X { get; }

        public int Y { get; }

        public ExplosionEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
