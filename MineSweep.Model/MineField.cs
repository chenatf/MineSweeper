using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace MineSweep.Model
{
    public class MineField:
        IReadOnlyDictionary<(int, int), Cell>
    {
        #region Constants
        public const int CELL_IS_MINE = -1;
        public const int CELL_IS_EMPTY = 0;
        #endregion
        #region Fields
        private int[,] _Cells;
        #endregion 
        #region Properties
        public int this[int x, int y] => _Cells[x, y];
        public int Width => _Cells.GetLength(0);
        public int Height => _Cells.GetLength(1);
        public int Count { get; }

        public IEnumerable<(int, int)> Keys
        {
            get
            {
                for(int i = 0; i < Width; ++i)
                    for(int j = 0; j < Height; ++j)
                        yield return (i, j);
            }
        }

        public IEnumerable<Cell> Values
        {
            get
            {
                for(int i = 0; i < Width; ++i)
                {
                    for(int j = 0; j < Height; ++j)
                    {
                        var value = _Cells[i, j];
                        switch(value)
                        {
                        case CELL_IS_EMPTY:
                            yield return new EmptyCell(i, j);
                            break;
                        case CELL_IS_MINE:
                            yield return new Mine(i, j);
                            break;
                        case int count:
                            yield return new Proximity(i, j, count);
                            break;
                        }
                    }
                }
            }
        }

        Cell IReadOnlyDictionary<(int, int), Cell>.this[(int, int) key]
        {
            get
            {
                var (x, y) = key;
                if(!IsValidIndex(x, y))
                    throw new KeyNotFoundException();
                var value = _Cells[x, y];
                switch(value)
                {
                case CELL_IS_EMPTY:
                    return new EmptyCell(x, y);
                case CELL_IS_MINE:
                    return new Mine(x, y);
                case int count:
                    return new Proximity(x, y, count);
                }
            }
        }
        #endregion
        #region Ctor
        public MineField(int width, int height, int count)
        {
            if(!IsValidBoundry(width, height)||
               !IsValidMineCount(count))
            {
                throw new ArgumentException();
            }
            else
            {
                _Cells = new int[width, height];
                Count = count;
                GenerateMines();
            }
        }
        #endregion
        #region Implements
        private void GenerateMines()
        {
            var idxs = new List<(int, int)>(Width * Height);
            var rng = new Random();
            for(int i = 0, x = 0; i < Width; ++i)
            {
                for(int j = 0; j < Height; ++j, ++x)
                {
                    var idx = (i, j);
                    var y = rng.Next(x);
                    if(y == idxs.Count)
                    {
                        idxs.Add(idx);
                    }
                    else
                    {
                        idxs.Add(idxs[y]);
                        idxs[y] = idx;
                    }
                }
            }
            foreach(var (x, y) in idxs.Take(Count))
            {
                _Cells[x, y] = CELL_IS_MINE;
                for(int i = -1; i <= 1; ++i)
                {
                    for(int j = -1; j <= 1; ++j)
                    {
                        IncCount(x + i, y + j);
                    }
                }
            }
        }
        private void IncCount(int x, int y)
        {
            if(IsValidIndex(x, y) && _Cells[x, y] != CELL_IS_MINE)
            {
                ++_Cells[x, y];
            }
        }
        #endregion
        #region Validaters
        private static bool IsValidBoundry(int width, int height)
        {
            return width > 0 && height > 0;
        }
        private static bool IsValidMineCount(int count)
        {
            return count > 0;
        }
        private bool IsValidIndex(int x, int y)
        {
            return x > 0 && y > 0 &&
                   x < Width && y < Height;
        }
        #endregion
        #region Methods
        public bool ContainsKey((int, int) key)
        {
            return IsValidIndex(key.Item1, key.Item2);
        }

        public bool TryGetValue((int, int) key, out Cell value)
        {
            var (x, y) = key;
            if(!IsValidIndex(x, y))
            {
                value = null;
                return false;
            }
            var val = _Cells[x, y];
            switch(val)
            {
            case CELL_IS_EMPTY:
                value = new EmptyCell(x, y);
                return true;
            case CELL_IS_MINE:
                value = new Mine(x, y);
                return true;
            case int count:
                value = new Proximity(x, y, count);
                return true;
            }
        }

        public IEnumerator<KeyValuePair<(int, int), Cell>> GetEnumerator()
        {
            for(int i = 0; i < Width; ++i)
            {
                for(int j = 0; j < Height; ++j)
                {
                    var value = _Cells[i, j];
                    switch(value)
                    {
                    case CELL_IS_EMPTY:
                        yield return new KeyValuePair<(int, int), Cell>(
                            (i, j), new EmptyCell(i, j)
                            );
                        break;
                    case CELL_IS_MINE:
                        yield return new KeyValuePair<(int, int), Cell>(
                        (i, j), new Mine(i, j)
                        );
                        break;
                    case int count:
                        yield return new KeyValuePair<(int, int), Cell>(
                        (i, j), new Proximity(i, j, count)
                        );
                        break;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
