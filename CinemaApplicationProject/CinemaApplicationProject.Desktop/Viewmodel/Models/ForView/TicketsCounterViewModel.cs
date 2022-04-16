using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public  class TicketsCounterViewModel : ViewModelBase
    {
        private String _type;
        private int _count;

        public String Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }
    }
}
