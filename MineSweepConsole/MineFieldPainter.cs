using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweepConsole
{
    public static class MineFieldPainter
    {
        public static char PrintCell(int cellValue)
        {
            switch(cellValue)
            {
            case -1:
                return '*';
            case 0:
                return ' ';
            case int count:
                return count.ToString()[0];
            }
    }
}
