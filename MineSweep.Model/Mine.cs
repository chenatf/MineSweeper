﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MineSweep.Model
{
    [DataContract]
    public sealed class Mine:
        Cell
    {
        public Mine(int x, int y) :
            base(x, y)
        {
        }
    }
}