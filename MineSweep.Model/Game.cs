using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using static MineSweep.Model.CellState;

namespace MineSweep.Model
{
    public class Game:
        INotifyPropertyChanged
    {
        #pragma warning disable CS0067
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore CS0067

        public event EventHandler<ExplosionEventArgs> Exploded;

        public const double MAX_MINE_RATIO = 0.6;

        protected static readonly CellCollection EmptyMineField = new CellCollection();

        protected CellCollection _Cells { get; set; }

        protected int Version { get; set; }

        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public IEnumerable<Cell> Cells => _Cells;

        public int MineCount { get; protected set; }

        public int MarkedMineCount { get; protected set; }

        public Game()
        {
            Version = default(int);
            _Cells = EmptyMineField;
        }

        public static int CalculateMaxNumberOfMine(int width, int height) =>
            Convert.ToInt32(Math.Floor(width * height * MAX_MINE_RATIO));

        public void Initialize(int width, int height, int mineCount, int firstClickX, int firstClickY)
        {
            const int isMine = -1;
            if(mineCount < 0 || mineCount > CalculateMaxNumberOfMine(width, height))
            {
                throw new ArgumentException("Too many mines");
            }

            bool CheckIndex(int x, int y) =>
                x > 0 && x < width &&
                y > 0 && y < height;

            var cellCounting = new int[width, height];
            var rand = new Random();
            for(var k = 0; k != mineCount; ++k)
            {
                int x, y;
                bool notToBeMine;
                do
                {
                    x = rand.Next(width);
                    y = rand.Next(height);
                    notToBeMine = 
                        cellCounting[x, y] == isMine ||
                        (x == firstClickX && y == firstClickY);
                } while (notToBeMine);

                cellCounting[x, y] = isMine;
                for(int i = -1; i <= 1; ++i)
                {
                    var x1 = x + i;
                    for (int j = -1; j <= 1; ++j)
                    {
                        var y1 = y + j;
                        if(CheckIndex(x1, y1) && cellCounting[x1, y1] != isMine)
                        {
                            ++cellCounting[x1, y1];
                        }
                    }
                }
            }

            var cells = new CellCollection();

            for(var x = 0; x != width; ++x)
            {
                for(var y = 0; y != height; ++y)
                {
                    var proximalCount = cellCounting[x, y];
                    var cellIsMine = proximalCount == isMine;
                    if(cellIsMine)
                    {
                        cells.Add(Cell.CreateMine(x, y));
                    }
                    else
                    {
                        cells.Add(Cell.CreateRegularCell(x, y, Convert.ToByte(proximalCount)));
                    }
                }
            }

            _Cells = cells;
            MineCount = mineCount;
            Width = width;
            Height = height;
            ++Version;
        }

        public void Explore(int x, int y)
        {
            var cell = _Cells[(x, y)];
            cell.State = Explored;
            if(cell.IsMine)
            {
                OnExploded(x, y);
            }
        }

        public void MarkAsMine(int x, int y)
        {
            var cell = _Cells[(x, y)];
            if(cell.State == Explored)
            {
                var ex = new InvalidOperationException("Cell is already explored");
                ex.Data.Add("x", x);
                ex.Data.Add("y", y);
                ex.Data.Add("cell", cell);
                throw ex;
            }
            cell.State = MarkedAsMine;
            ++MarkedMineCount;
        }

        protected virtual void OnExploded(int x, int y)
        {
            Exploded?.Invoke(this, new ExplosionEventArgs(x, y));
        }
    }
}
