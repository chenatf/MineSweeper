using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MineSweep.Model
{
    public sealed class BitArray2D
    {
        private readonly BitArray array;
        public int Length1 { get; }
        public int Length2 { get; }

        public BitArray2D(int length1, int length2)
        {
            array = new BitArray(length1 * length2);
            Length1 = length1;
            Length2 = length2;
        }

        public BitArray2D(int length1, int length2, bool defaultValue)
        {
            array = new BitArray(length1 * length2, defaultValue);
            Length1 = length1;
            Length2 = length2;
        }

        public BitArray2D(bool[,] values)
            :this(values.GetLength(0), values.GetLength(1))
        {
            for(int i = 0; i < Length1; ++i)
            {
                for(int j = 0; j < Length2; ++j)
                {
                    this[i, j] = values[i, j];
                }
            }
        }

        public bool this[int x, int y]
        {
            get
            {
                return array[x * Length2 + y];
            }
            set
            {
                array[x * Length2 + y] = value;
            }
        }
    }
}
