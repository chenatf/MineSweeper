using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweep.Model
{
    public class MineHitEventArgs:
        EventArgs
    {
        public int X { get; }

        public int Y { get; }

        public MineHitEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
