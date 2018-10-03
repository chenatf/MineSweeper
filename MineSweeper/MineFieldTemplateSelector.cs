using MineSweep.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static MineSweep.Model.CellState;

namespace MineSweeper
{
    public class MineFieldTemplateSelector :
        DataTemplateSelector
    {
        public DataTemplate UnexploredCell { get; set; }

        public DataTemplate ExploredCell { get; set; }

        public DataTemplate MarkedAsMineCell { get; set; }

        public DataTemplate ExplodedMineCell { get; set; }

        public DataTemplate MissedMineCell { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //TODO: switch to end game template base on datacontext of container
            if(!(item is IndexedCell))
            {
                return null;
            }
            var cell = ((IndexedCell)item).Cell;
            switch(cell.State)
            {
                case Unexplored:
                    return UnexploredCell;
                case MarkedAsMine:
                    return MarkedAsMineCell;
                case Explored:
                    if(cell.IsMine)
                    {
                        return ExplodedMineCell;
                    }
                    else
                    {
                        return ExploredCell;
                    }
                default:
                    return null;
            }
        }
    }
}
