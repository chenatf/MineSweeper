using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweep.Model
{
    public struct Tile
    {
        #region Constants
        private const int EMPTY_STATUS = 0;
        private const int MINE_STATUS = -1;
        private const int MAX_PROXIMITY_COUNT = 8;
        #endregion

        #region Fields
        private int _Status;
        #endregion

        #region Properties
        public int MinesInProximity => 
            _Status > 0 ? _Status : 0;
        public bool IsEmpty => 
            _Status == EMPTY_STATUS;
        public bool IsMine =>
            _Status == MINE_STATUS;
        #endregion

        #region Constructors
        private Tile(int status)
        {
            _Status = status;
        }
        #endregion

        #region Factories
        public static Tile Empty => new Tile(EMPTY_STATUS);
        public static Tile Mine => new Tile(MINE_STATUS);
        public static Tile Create(int minesInProximity)
        {
            if(minesInProximity > MAX_PROXIMITY_COUNT || minesInProximity < 0)
            {
                throw new ArgumentException();
            }
            return new Tile(minesInProximity);
        }
        #endregion
    }
}
