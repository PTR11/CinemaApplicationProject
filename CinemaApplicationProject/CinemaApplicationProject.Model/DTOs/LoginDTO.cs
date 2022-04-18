using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class LoginDTO : RespondDTO
    {
        public String UserName { get; set; }

        public String Password { get; set; }
    }
}
