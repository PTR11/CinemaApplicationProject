using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models
{
    public class MainViewModel : ViewModelBase
    {

        private ObservableCollection<PeopleViewModel> _people = new ObservableCollection<PeopleViewModel>();
        public ObservableCollection<PeopleViewModel> People
        {
            get { return _people; }
            set { _people = value; OnPropertyChanged(); }
        }
        private String _name;

        public String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            List<PeopleViewModel> tmpPrograms = new List<PeopleViewModel>();

            tmpPrograms.Add(new PeopleViewModel
            {
                Name = "Sajt",
                Age = "12",
                Address1 = "fasz",
                Address2 = "fasza"
            });
            tmpPrograms.Add(new PeopleViewModel
            {
                Name = "Sajt2",
                Age = "123",
                Address1 = "fasz",
                Address2 = "fasza"
            });
            People = new ObservableCollection<PeopleViewModel>(tmpPrograms);
            Name = "Sajt";
        }
    }
}
