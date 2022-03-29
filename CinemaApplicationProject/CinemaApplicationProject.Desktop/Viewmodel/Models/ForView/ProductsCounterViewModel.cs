using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class ProductsCounterViewModel : ViewModelBase
    {
        private String _type;
        private int _count;
        private int _price;


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

        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

    }
}
