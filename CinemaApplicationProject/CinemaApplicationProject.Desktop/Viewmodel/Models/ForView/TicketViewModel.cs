using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Viewmodel.Models.ForView
{
    public class TicketViewModel : ViewModelBase
    {
        private string _type;
        private int _price;

        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        public TicketViewModel ShallowClone()
        {
            return (TicketViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(TicketViewModel rhs)
        {
            Id = rhs.Id;
            Type = rhs.Type;
            Price = rhs.Price;
        }

        public static explicit operator TicketViewModel(TicketsDTO dto) => new TicketViewModel
        {
            Id = dto.Id,
            Type = dto.Type,
            Price = dto.Price,
        };

        public static explicit operator TicketsDTO(TicketViewModel dto) => new TicketsDTO
        {
            Id = dto.Id,
            Type = dto.Type,
            Price = dto.Price,
        };
    }
}
