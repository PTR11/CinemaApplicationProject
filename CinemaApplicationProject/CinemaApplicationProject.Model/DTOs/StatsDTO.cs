using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class StatsDTO : RespondDTO
    {
        public string Name { get; set; }

        public int Salary { get; set; }

        public int UserId { get; set; }

        public static explicit operator StatsDTO(StatsAndPays m) => new StatsDTO
        {
            Id = m.Id,
            Name = m.Name,
            Salary = m.Salary
        };

        public static explicit operator StatsAndPays(StatsDTO m) => new StatsAndPays
        {
            Id = m.Id,
            Name = m.Name,
            Salary = m.Salary
        };

    }
}
