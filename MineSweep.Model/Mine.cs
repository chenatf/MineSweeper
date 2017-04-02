using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Linq;

namespace MineSweep.Model
{
    [DataContract]
    public class Mine :
        Cell
    {
        public Mine(Point p) :
            base(p)
        {
        }

        public Mine(int x, int y) :
            base(x, y)
        {
        }

        public static IList<Mine> GenerateMines(int count, int maxX, int maxY)
        {
            var pairs = new List<(int, int)>();
            foreach (var x in Enumerable.Range(0, maxX))
            {
                foreach (var y in Enumerable.Range(0, maxY))
                {
                    pairs.Add((x, y));
                }
            }
            CellGenerator.Shuffle(pairs);
            var result = new List<Mine>(count);
            for (int i = 0; i < count; ++i)
            {
                result.Add(
                    new Mine(pairs[i]));
            }
            return result;
        }
    }


}
