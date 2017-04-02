using System;
using System.Runtime.Serialization;
namespace MineSweep.Model
{
    [DataContract]
    [KnownType(typeof(Mine)), KnownType(typeof(Proximity))]
    public abstract class Cell
    {
        [DataMember(Name = "Location")]
        private readonly Point _Location;
        [IgnoreDataMember]
        public Point Location => _Location;
        [IgnoreDataMember]
        public int X => Location.X;
        [IgnoreDataMember]
        public int Y => Location.Y;

        public override string ToString()
        {
            return
                base.ToString() +
                $"\nX: {X}, Y: {Y}";
        }

        public Cell(int x, int y)
        {
            _Location = new Point(x, y);
        }

        public Cell(Point p)
        {
            _Location = p;
        }
    }
}
