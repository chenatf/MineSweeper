using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MineSweep.Model
{
    [DataContract]
    [KnownType(typeof(Empty)), KnownType(typeof(Mine))]
    [KnownType(typeof(Proximity)), KnownType(typeof(Covered))]
    public abstract class Cell:
        IEquatable<Cell>
    {
        #region Field
        [DataMember(Name = "X")]
        private readonly int _X;
        [DataMember(Name = "Y")]
        private readonly int _Y;
        #endregion

        #region Properties
        [IgnoreDataMember]
        public int X => _X;
        [IgnoreDataMember]
        public int Y => _Y;
        #endregion

        #region Ctor
        public Cell(int x, int y)
        {
            _X = x;
            _Y = y;
        }
        #endregion
        public override bool Equals(object obj)
        {
            var rhs = obj as Cell;
            if(rhs == null)
            {
                return false;
            }
            else
            {
                return Equals(rhs);
            }
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = CombineHash(hash, X.GetHashCode());
            hash = CombineHash(hash, Y.GetHashCode());
            return hash;
        }

        public bool Equals(Cell other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        protected static int CombineHash(int hash1, int hash2)
        {
            unchecked
            {
                return hash1 * 23 + hash2;
            }
        }
    }
}
