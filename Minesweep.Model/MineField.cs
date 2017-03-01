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
        #endregion

        public MineField(Difficulty setting)
        {
            _Field = new Tile[setting.Columns, setting.Rows];
            MineCount = setting.Mines;
        }

        public void Initialize(int x, int y)
        {

        }
    }
}
