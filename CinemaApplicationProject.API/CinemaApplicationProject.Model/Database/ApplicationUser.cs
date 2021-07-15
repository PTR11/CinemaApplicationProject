using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int EmployeeId { get; set; }
        public int GuestId { get; set; }
        public virtual Employees Employees { get; set; }

        public virtual Guests Guests { get; set; }
    }
}
