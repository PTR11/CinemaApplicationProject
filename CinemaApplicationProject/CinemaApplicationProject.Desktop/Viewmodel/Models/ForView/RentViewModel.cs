using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class RentViewModel : ViewModelBase
    {
        private int _x;
        private int _y;
        private int _ticketId;
        private int? _employeeId;
        private int? _guestId;
        

        
        public int X {
            get { return _x; }
            set { _x = value; OnPropertyChanged(); }
        }

        public int Y {
            get { return _y; }
            set { _y = value; OnPropertyChanged(); }
        }

        public int TicketId {
            get { return _ticketId; }
            set { _ticketId = value; OnPropertyChanged(); }
        }

        public int? EmployeeId {
            get { return _employeeId; }
            set { _employeeId = value; OnPropertyChanged(); }
        }

        public int? GuestId {
            get { return _guestId; }
            set { _guestId = value; OnPropertyChanged(); }
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                RentViewModel rent = (RentViewModel)obj;
                return (X == rent.X) && (Y == rent.Y);
            }
        }

        public static explicit operator RentViewModel(RentsDTO dto) => new RentViewModel
        {
            Id = dto.Id,
            X = dto.X,
            Y = dto.Y,
            TicketId = dto.TicketId,
            EmployeeId = dto.EmployeeId,
            GuestId = dto.GuestId,
        };
    }
}
