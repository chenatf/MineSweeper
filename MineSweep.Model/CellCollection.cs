using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MineSweep.Model
{
    public class CellCollection :
        KeyedCollection<(int, int), Cell>
    {
        protected override (int, int) GetKeyForItem(Cell item)
        {
            return (item.X, item.Y);
        }
    }
}
