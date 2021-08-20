using CinemaApplicationProject.Desktop.Viewmodel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models
{
    public class PeopleViewModel : ViewModelBase
    {
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
        private String _age;

        public String Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged();
                }
            }
        }
        private String _address1;

        public String Address1
        {
            get { return _address1; }
            set
            {
                if (_address1 != value)
                {
                    _address1 = value;
                    OnPropertyChanged();
                }
            }
        }
        private String _address2;

        public String Address2
        {
            get { return _address2; }
            set
            {
                if (_address2 != value)
                {
                    _address2 = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
