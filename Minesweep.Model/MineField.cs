using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweep.Model
{
    public class MineField
    {
        #region Fields
        private Tile[,] _Field;
        #endregion

        #region Properties
        public Tile this[int x, int y] => _Field[x, y];
        public int MineCount { get; }
        public int Columns => _Field.GetLength(0);
        public int Rows => _Field.GetLength(1);
        #endregion

        public MineField(Difficulty setting)
        {
            _Field = new Tile[setting.Columns, setting.Rows];
            MineCount = setting.Mines;
        }

        public void Initialize(int saveX, int saveY)
        {
            var gen = new Random();
            for(int i = 0; i < MineCount; ++i)
            {
                var x = saveX;
                var y = saveY;
                while(x == saveX || y == saveY ||
                      _Field[x, y].IsMine)
                {
                    x = gen.Next(Columns - 1);
                    y = gen.Next(Rows - 1);
                }

                _Field[x, y] = Tile.Mine;
                for(int dx = -1; dx < 2 && (x + dx) > 0; ++dx)
                {
                    for(int dy = -1; dy < 2 && (y + dy) > 0 && (dx != x || dy != y); ++dy)
                    {
                        var newX = x + dx;
                        var newY = y + dy;
                        _Field[newX, newY] = Tile.Create(_Field[newX, newY].MinesInProximity + 1);
                    }
                }
            }
        }
    }
}
