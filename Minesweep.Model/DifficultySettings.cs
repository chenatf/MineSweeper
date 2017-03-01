using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweep.Model
{
    public struct Difficulty
    {
        public int Columns { get; }
        public int Rows { get; }
        public int Mines { get; }

        public static int MIN_DIM => 10;

        public Difficulty(int columns, int rows, int mines)
        {
            if(columns < MIN_DIM)
            {
                throw new ArgumentException("Not enough space.", "columns");
            }
            if(rows < MIN_DIM)
            {
                throw new ArgumentException("Not enough space.", "rows");
            }
            if(mines > rows * columns - 1)
            {
                throw new ArgumentException("Too many mines");
            }
            if(mines < 1)
            {
                throw new ArgumentException("Too few mines");
            }
            Columns = columns;
            Rows = rows;
            Mines = mines;
        }
    }
    internal static class DifficultySettings
    {
        private static readonly Difficulty _Beginner =
            new Difficulty(9, 9, 10);
        public static Difficulty Beginner => _Beginner;
        private static readonly Difficulty _Intermediate =
            new Difficulty(16, 16, 40);
        public static Difficulty Intermediate => _Intermediate;
        private static readonly Difficulty _Expert =
            new Difficulty(30, 16, 99);
    }
}
