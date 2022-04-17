using CinemaApplicationProject.Desktop.Viewmodel.Base;
using CinemaApplicationProject.Desktop.Viewmodel.Models.ForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Desktop.Model.Services
{
    public class ValidationService
    {
        private string _validationString = "";

        public bool IsValid()
        {
            return _validationString == "";
        }

        public String GetValidationString()
        {
            return _validationString;
        }

        public (bool, String) Validate<T>(T obj) where T : ViewModelBase
        {
            switch (obj.GetType().Name)
            {
                case "MovieViewModel": IsMovieValid(obj as MovieViewModel); break;
                case "RoomViewModel": IsRoomValid(obj as RoomViewModel); break;
                case "TicketViewModel": IsTicketValid(obj as TicketViewModel); break;
                case "EmployeeViewModel": IsUserValid(obj as EmployeeViewModel); break;
                case "ProductViewModel": IsProductValid(obj as ProductViewModel); break;
                case "StatsViewModel": IsRoleValid(obj as StatsViewModel); break;
                case "ShowViewModel": IsShowValid(obj as ShowViewModel); break;
                default: _validationString = "Can not validate this class"; break;
            }
            return ResultValidation();
        }

        private (bool,String) IsMovieValid(MovieViewModel movie)
        {
            _validationString = "";
            if (movie.Title.Length == 0) _validationString += "Missing Title\n";
            if (movie.Director.Length == 0) _validationString += "Missing Director\n";
            if (movie.Trailer.Length == 0) _validationString += "Missing Trailer\n";
            if (movie.Actors.Count == 0) _validationString += "Missing Actors\n";
            if (movie.Categories.Count == 0) _validationString += "Missing Categories\n";
            if (movie.Length == 0) _validationString += "Missing Length \n";
            return ResultValidation();
        }

        private (bool, String) IsRoomValid(RoomViewModel room)
        {
            _validationString = "";
            if (room.Name.Length == 0) _validationString += "Missing Name\n";
            if (room.Width == 0) _validationString += "Missing Width\n";
            if (room.Heigth == 0) _validationString += "Missing Heigth\n";
            

            return ResultValidation();
        }

        private (bool, String) IsTicketValid(TicketViewModel ticket)
        {
            _validationString = "";
            if (ticket.Type.Length == 0) _validationString += "Missing Type\n";
            if (ticket.Price == 0) _validationString += "Missing Price\n";


            return ResultValidation();
        }

        private (bool, String) IsUserValid(EmployeeViewModel employee)
        {
            _validationString = "";
            if (employee.Name.Length == 0) _validationString += "Missing Name\n";
            if (employee.UserName.Length == 0) _validationString += "Missing UserName\n";
            if (employee.Address.Length == 0) _validationString += "Missing Address\n";
            if (employee.Email.Length == 0) _validationString += "Missing Email\n";
            if (!employee.Email.Contains("@")) _validationString += "Missing '@' in Email\n";
            if (employee.Birthday.Split("/").Length != 3) _validationString += "Wrong Birthday format (yyyy/mm/dd)\n";
            if (employee.Password != null)
            {
                if (!employee.Password.Any(char.IsDigit)) _validationString += "Wrong Password format (must contain at least one number)\n";
                if (employee.Password.Length < 8) _validationString += "Wrong Password format (must be at least 8 characters)\n";
                if (!employee.Password.Any(char.IsUpper)) _validationString += "Wrong Password format (must contain at least 1 upper characters)\n";
                if (employee.Password.Length == 0) _validationString += "Missing Password\n";
            }
            
            if (employee.Stats.Count == 0) _validationString += "Missing Roles\n";

            return ResultValidation();
        }
        
        private (bool, String) IsProductValid(ProductViewModel product)
        {
            _validationString = "";

            if (product.Name.Length == 0) _validationString += "Missing Type\n";
            if (product.Price == 0) _validationString += "Missing Price\n";
            if (product.Quantity == 0) _validationString += "Missing Quantity\n";

            return ResultValidation();
        }

        private (bool, String) IsRoleValid(StatsViewModel role)
        {
            _validationString = "";

            if (role.Name.Length == 0) _validationString += "Missing Type\n";
            if (role.Salary == 0) _validationString += "Missing Salary\n";

            return ResultValidation();
        }

        private (bool, String) IsShowValid(ShowViewModel show)
        {
            _validationString = "";

            if (show.Date != DateTime.MinValue && show.Date.Date.Year < 2022) _validationString += "Wrong Date (should be later than 2022)\n";
            if (show.MovieTitle.Length == 0) _validationString += "Missing Movie\n";
            if (show.RoomName.Length == 0) _validationString += "Missing Room\n";

            return ResultValidation();
        }

        private (bool, String) ResultValidation() {
            if (_validationString.Equals(""))
            {
                return (true, "No validation needed");
            }
            else
            {
                return (false, _validationString);
            }
        }

    }
}
