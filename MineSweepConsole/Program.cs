using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweep.Model;
using System.Collections;

namespace MineSweepConsole
{
    class Program
    {
        private MineField field;

        private BitArray mask;

        public Program()
        {
            var difficulty = DifficultySettings.Intermediate;
            field = new MineField(difficulty);
            mask = new BitArray(difficulty.Rows * difficulty.Columns, false);
        }

        public void Draw()
        {
            for(int x = 0; x < field.Columns; ++x)
            {
                for(int y = 0; y < field.Rows; ++y)
                {
                    if(!mask[x*field.Columns+y])
                    {
                        Console.Write("██");
                    }
                    else if(field[x, y].IsEmpty)
                    {
                        Console.Write("  ");
                    }
                    else if(field[x, y].IsMine)
                    {
                        Console.Write("►◄");
                    }
                    else
                    {
                        Console.Write($"{field[x, y].MinesInProximity:2}");
                    }
                }
                Console.WriteLine();
            }
        }

        public bool Open(int x, int y)
        {
            mask[x * field.Columns + y] = true;
            if(field[x, y].IsMine)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static void Main(string[] args)
        {
            var program = new Program();
            program.Draw();
            Console.WriteLine();
            program.field.Initialize(2, 4);
            program.Open(2, 4);
            program.Open(1, 3);
            program.Open(5, 7);
            program.Draw();
        }
    }
}
