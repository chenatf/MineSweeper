using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MineSweep.Model
{
    [DataContract]
    public class Proximity:
        Cell
    {
        [DataMember(Name = "Count")]
        private readonly int _Count;

        public Proximity(Point p, int count) :
            base(p)
        {
            _Count = count;
        }

        public Proximity(int x, int y, int count) :
            base(x, y)
        {
            _Count = count;
        }

        [IgnoreDataMember]
        public int Count => _Count;

        public override string ToString()
        {
            return 
                base.ToString() + 
                $"\nCount:{Count}";
        }

        public static IList<Proximity> GenerateProximities(IEnumerable<Mine> mines)
        {
            var counter = new Dictionary<(int, int), int>();
            foreach (var mine in mines)
            {
                for (var i = -1; i <= 1; ++i)
                {
                    for (var j = -1; j <= 1; ++j)
                    {
                        var loc = (mine.X + i, mine.Y + j);
                        if (loc != mine.Location)
                        {
                            if (counter.ContainsKey(loc))
                            {
                                counter[loc]++;
                            }
                            else
                            {
                                counter[loc] = 1;
                            }
                        }
                    }
                }
            }
            var result = new List<Proximity>();
            foreach (var (x, y, v) in counter)
            {
                result.Add(new Proximity(x, y, v));
            }
            return result;
        }
    }
}
