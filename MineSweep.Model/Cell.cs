using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MineSweep.Model
{
    public class Cell :
        INotifyPropertyChanged
    {
        #pragma warning disable CS0067
        public event PropertyChangedEventHandler PropertyChanged;
        #pragma warning restore CS0067

        public CellState State { get; set; }

        public bool IsMine { get; }

        public byte ProximalMineCount { get; }

        public Cell(byte proximalMineCount, bool isMine = false)
        {
            State = CellState.Unexplored;
            ProximalMineCount = proximalMineCount;
            IsMine = isMine;
        }
    }
}
