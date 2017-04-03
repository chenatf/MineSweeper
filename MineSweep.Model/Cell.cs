using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MineSweep.Model
{
    [DataContract]
    [KnownType(typeof(EmptyCell)), KnownType(typeof(Mine)), KnownType(typeof(Proximity))]
    public abstract class Cell
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
    }
}
