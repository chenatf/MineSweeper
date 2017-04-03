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
    class Program
    {
        private GameBoard _board;

        public Program(int width, int height, int count)
        {
            _board = new GameBoard(width, height, count);
        }

        public static string PrintCell(int cellValue)
        {
            switch(cellValue)
            {
            case GameBoard.CELL_IS_COVERED:
                return "██";
            case GameBoard.CELL_IS_MINE:
                return "**";
            case GameBoard.CELL_IS_EMPTY:
                return "  ";
            case int count:
                return $"{count:D2}";
            }
        }

        public void Draw(TextWriter writer)
        {
            for(int i = 0; i < _board.Height; ++i)
            {
                for(int j = 0; j < _board.Width; ++j)
                {
                    writer.Write(PrintCell(_board[i, j]));
                }
                writer.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            var program = new Program(15, 9, 10);
            program.Draw(Console.Out);
            Console.WriteLine();
            program._board.Open(2, 4);
            program.Draw(Console.Out);
            Console.WriteLine();
            program._board.Open(1, 3);
            program.Draw(Console.Out);
            Console.WriteLine();
            program._board.Open(5, 7);
            program.Draw(Console.Out);
            Console.WriteLine();
            program._board.Open(2, 1);
            program.Draw(Console.Out);
            Console.WriteLine();
            program._board.Open(8, 0);
            program.Draw(Console.Out);
            Console.WriteLine();
        }
    }
}
