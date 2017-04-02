using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweep.Model;
using System.Runtime.Serialization;
using System.IO;

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
        static void Test1()
        {
            var p = new Point(3, 5);
            var (x, y) = p;
            var serializer = new DataContractSerializer(typeof(Point));
            using (var stream = new MemoryStream(3000))
            {
                serializer.WriteObject(stream, p);
                serializer.WriteObject(Console.OpenStandardOutput(), p);
                Console.WriteLine();
                stream.Seek(0, SeekOrigin.Begin);
                var p1 = (Point)serializer.ReadObject(stream);

            }
            Console.WriteLine();
        }
        static void Test2()
        {
            var mine = new Mine(5, 8);

            var prox = new Proximity(5, 8, 2);

            var s1 = new DataContractSerializer(typeof(Mine));
            var s2 = new DataContractSerializer(typeof(Proximity));

            using (var stream = Console.OpenStandardOutput())
            {
                s1.WriteObject(stream, mine);
                Console.WriteLine();
                s2.WriteObject(stream, prox);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void Test3()
        {
            var mines = Mine.GenerateMines(10, 8, 8);
            var se = new DataContractSerializer(typeof(Mine));
            using (var stream = Console.OpenStandardOutput())
            {
                foreach (var mine in mines)
                {
                    se.WriteObject(stream, mine);
                    Console.WriteLine();
                }
            }
        }
        static string Render(Cell cell)
        {
            switch (cell)
            {
                case Mine m:
                    return "*";
                case Proximity p:
                    return p.Count.ToString();
                default:
                    return " ";
            }
        }
        static int ComparePoint(Point x, Point y)
        {
            return
                x.X == y.X ?
                x.Y - y.Y :
                x.X - y.X;
        }
        static void Test4()
        {
            var mineSet = new SortedDictionary<Point, Cell>(new FuncComparer<Point>(ComparePoint));
            foreach (var mine in Mine.GenerateMines(10, 8, 8))
            {
                mineSet.Add(mine.Location, mine);
            }
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (mineSet.ContainsKey((i, j)))
                    {
                        Console.Write(Render(mineSet[(i, j)]));
                    }
                    else
                    {
                        Console.Write(Render(null));
                    }
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            //Test1();
            //Test2();
            //Test3();
            //Test4();
            var mines = Mine.GenerateMines(10, 9, 9);
            var proxs = Proximity.GenerateProximities(mines);
            var cellSet = new SortedSet<Cell>(
                new FuncComparer<Cell>((x, y) => ComparePoint(x.Location, y.Location))
                );
            
            foreach (var mine in mines)
            {
                cellSet.Add(mine);
            }

            foreach (var prox in proxs)
            {
                if(!cellSet.Contains(prox))
                    cellSet.Add(prox);
            }
            var cells = cellSet.ToDictionary(cell => cell.Location);
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (cells.ContainsKey((i, j)))
                    {
                        Console.Write(Render(cells[(i, j)]));
                    }
                    else
                    {
                        Console.Write(Render(null));
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
