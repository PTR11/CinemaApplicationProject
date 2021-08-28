using CinemaApplicationProject.Desktop.Viewmodel.Base;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models
{
    public class MainViewModel : ViewModelBase
    {

        private ObservableCollection<PeopleViewModel> _people = new ObservableCollection<PeopleViewModel>();
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

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

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "2015",
                    Values = new ChartValues<double> { 10, 50, 39, 50 }
                }
            };

            //adding series will update and animate the chart automatically
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "2016",
                Values = new ChartValues<double> { 11, 56, 42 }
            });

            //also adding values updates and animates the chart automatically
            SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            Formatter = value => value.ToString("N");

        }
    }
}
