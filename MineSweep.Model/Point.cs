using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MineSweep.Model
{
    [DataContract]
    public struct Point:
        IEquatable<Point>
    {
        
        [DataMember(Name = "X")]
        private readonly int _X;
        [DataMember(Name = "Y")]
        private readonly int _Y;

        [IgnoreDataMember]
        public int X
        {
            get => _X;
        }
        [IgnoreDataMember]
        public int Y
        {
            get => _Y;
        }

        public Point(int x, int y)
        {
            _X = x;
            _Y = y;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = _X;
            y = _Y;
        }

        public bool Equals(Point other)
        {
            return
                X == other.X &&
                Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Point p:
                    return this.Equals(p);
                default:
                    return false;
            }

        }

        public override int GetHashCode()
        {
            var code = 17;
            code = code * 31 + X.GetHashCode();
            code = code * 31 + Y.GetHashCode();
            return code;
        }

        public static bool operator==(Point lhs, Point rhs)
        {
            return
                lhs.Equals(rhs);
        }

        public static bool operator!=(Point lhs, Point rhs)
        {
            return
                !lhs.Equals(rhs);
        }

        public static Point operator+(Point lhs, Point rhs)
        {
            return new Point(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Point operator-(Point lhs, Point rhs)
        {
            return new Point(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public static implicit operator Point((int, int) value)
        {
            return new Point(value.Item1, value.Item2);
        }

        public static explicit operator (int, int) (Point p)
        {
            return (p.X, p.Y);
        }
    }
}
