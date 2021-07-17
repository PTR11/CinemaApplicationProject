using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Database
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
