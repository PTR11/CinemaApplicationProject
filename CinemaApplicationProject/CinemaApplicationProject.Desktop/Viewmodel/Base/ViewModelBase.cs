using CinemaApplicationProject.Desktop.Model.Errors;
using CinemaApplicationProject.Desktop.Model.Services;
using CinemaApplicationProject.Desktop.Viewmodel.EventArguments;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace CinemaApplicationProject.Desktop.Viewmodel.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase _mainModel;

        public ViewModelBase MainModel
        {
            get { return _mainModel; }
            set { _mainModel = value; OnPropertyChanged(); }
        }

        public int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<MessageEventArgs> MessageApplication;

        protected static MainApiService _service = new MainApiService(ConfigurationManager.AppSettings["baseAddress"]);

        protected ViewModelBase() { }

        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnMessageApplication(String message)
        {
            if (MessageApplication != null)
                MessageApplication(this, new MessageEventArgs(message));
        }


        
    }
}
