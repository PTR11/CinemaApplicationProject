using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class ShowWeekViewModel : ViewModelBase
    {
        public ObservableCollection<ShowViewModel> _monday;
        public ObservableCollection<ShowViewModel> _tuesday;
        public ObservableCollection<ShowViewModel> _wednesday;
        public ObservableCollection<ShowViewModel> _thursday;
        public ObservableCollection<ShowViewModel> _friday;
        public ObservableCollection<ShowViewModel> _saturday;
        public ObservableCollection<ShowViewModel> _sunday;

        public ObservableCollection<ShowViewModel> Monday
        {
            get { return _monday; }
            set { _monday = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ShowViewModel> Tuesday
        {
            get { return _tuesday; }
            set { _tuesday = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ShowViewModel> Wednesday
        {
            get { return _wednesday; }
            set { _wednesday = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ShowViewModel> Thursday
        {
            get { return _thursday; }
            set { _thursday = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ShowViewModel> Friday
        {
            get { return _friday; }
            set { _friday = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ShowViewModel> Saturday
        {
            get { return _saturday; }
            set { _saturday = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ShowViewModel> Sunday
        {
            get { return _sunday; }
            set { _sunday = value; OnPropertyChanged(); }
        }
    }
}
