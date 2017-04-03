using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweep.Model;
using System.Collections;
using System.IO;

namespace MineSweepConsole
{
    sealed class BitArray2D
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
    class Program
    {
        public const int CELL_IS_COVERED = -2;
        private MineField _field;
        private BitArray2D _mask;

        public Program(int width, int height, int count)
        {
            _field = new MineField(width, height, count);
            _mask = new BitArray2D(width, height);
        }

        public int this[int x, int y]
        {
            get
            {
                if(_mask[x, y])
                {
                    return _field[x, y];
                }
                else
                {
                    return CELL_IS_COVERED;
                }
            }
        }

        public static string PrintCell(int cellValue)
        {
            switch(cellValue)
            {
            case CELL_IS_COVERED:
                return "██";
            case MineField.CELL_IS_MINE:
                return "**";
            case MineField.CELL_IS_EMPTY:
                return "  ";
            case int count:
                return $"{count:D2}";
            }
        }

        public void Draw(TextWriter writer)
        {
            for(int i = 0; i < _field.Width; ++i)
            {
                for(int j = 0; j < _field.Height; ++j)
                {
                    writer.Write(PrintCell(this[i, j]));
                }
                writer.WriteLine();
            }
        }

        public void Open(int x, int y)
        {
            if(_field.IsValidIndex(x, y))
            {
                if(!_mask[x, y])
                {
                    _mask[x, y] = true;
                    if(_field[x, y] == MineField.CELL_IS_EMPTY)
                    {
                        for(int i = -1; i <= 1; ++i)
                        {
                            for(int j = -1; j <= 1; ++j)
                            {
                                Open(x + i, y + j);
                            }
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            var program = new Program(9, 9, 10);
            program.Draw(Console.Out);
            Console.WriteLine();
            program.Open(2, 4);
            program.Open(1, 3);
            program.Open(5, 7);
            program.Open(2, 1);
            program.Open(8, 0);
            program.Draw(Console.Out);
            //var field = new MineField(9, 9, 10);
            //Console.WriteLine(field);
        }
    }
}
