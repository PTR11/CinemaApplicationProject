using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class Field : ViewModelBase
    {
        private String _background;
        private String _image;

        public Field()
        {

        }

        public string Background
        {
            get { return _background; }
            set { _background = value; OnPropertyChanged(); }
        }

        public String Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        public Int32 X { get; set; }

        public Int32 Y { get; set; }

        public Int32 Number { get; set; }

        public DelegateCommand OnClick { get; set; }

    }
}
