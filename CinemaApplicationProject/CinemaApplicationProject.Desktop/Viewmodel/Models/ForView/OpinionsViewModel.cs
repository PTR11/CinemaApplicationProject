using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class OpinionsViewModel : ViewModelBase
    {
        private String _userName;
        private int _stars;
        private String _comment;

        public OpinionsViewModel()
        {
            this.UserName = "";
            this.Stars = 0;
            this.Comment = "";
            
        }
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }

        public int Stars
        {
            get { return _stars; }
            set { _stars = value; OnPropertyChanged(); }
        }

        public String Comment
        {
            get { return _comment; }
            set { _comment = value; OnPropertyChanged(); }
        }

        public static explicit operator OpinionsViewModel(OpinionsDTO dto) => new OpinionsViewModel
        {
            Id = dto.Id,
            UserName = dto.GuestName,
            Stars = dto.Ranking,
            Comment = dto.Description,

        };
    }
}
