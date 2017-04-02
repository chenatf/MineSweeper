using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using MineSweep.Model;
using MineSweep.Model.CSharp;

namespace Test
{
    sealed class FuncComparer<T> :
        IComparer<T>
    {
        private readonly Comparison<T> comp;
        public FuncComparer(Comparison<T> comp)
        {
            this.comp = comp;
        }

        public int Compare(T x, T y)
        {
            return comp(x, y);
        }
    }

    class Program
    {
        static string Render(Cell cell)
        {
            switch (cell)
            {
                case Cell.Proximity p:
                    return p.Count.ToString();
                default:
                    switch (cell.Tag)
                    {
                        case Cell.Tags.Empty:
                            return " ";
                        case Cell.Tags.Mine:
                            return "*";
                        default:
                            throw new InvalidOperationException();
                    }

            }
        }

        static void Main(string[] args)
        {
            var mineField = CellGenerator.CreateMineField(10, 9, 9);
            for(int i = 0; i < 9; ++i)
            {
                for(int j = 0; j < 9; ++j)
                {
                    Console.Write(Render(mineField[(i, j)]));
                }
                Console.WriteLine();
            }
        }
    }
}
