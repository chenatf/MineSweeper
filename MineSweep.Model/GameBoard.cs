using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweep.Model
{
    public class GameBoard:
        MineField
    {
        public const int CELL_IS_COVERED = -2;
        private BitArray2D _mask;

        public override int this[int x, int y]
        {
            get
            {
                if(_mask[x, y])
                {
                    return base[x, y];
                }
                else
                {
                    return CELL_IS_COVERED;
                }
            }
        }

        public GameBoard(int height, int width, int count)
            :base(height, width, count)
        {
            _mask = new BitArray2D(height, width);
        }

        public HashSet<Cell> Open(int x, int y)
        {
            if(IsValidIndex(x, y) && !_mask[x, y])
            {
                _mask[x, y] = true;
                var result = new HashSet<Cell>();
                result.Add(GetCell(x, y));
                if(base[x, y] == CELL_IS_EMPTY)
                {
                    for(int i = -1; i <= 1; ++i)
                    {
                        for(int j = -1; j <= 1; ++j)
                        {
                            result.UnionWith(Open(x + i, y + j));
                        }
                    }
                }
                return result;
            }
            else
            {
                return new HashSet<Cell>();
            }
        }

        protected override Cell GetCell(int x, int y)
        {
            if(!_mask[x, y])
            {
                return new Covered(x, y);
            }
            else
            {
                return base.GetCell(x, y);
            }
        }

    }
}
