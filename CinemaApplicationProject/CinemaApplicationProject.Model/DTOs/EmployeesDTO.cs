﻿using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.DTOs
{
    public class EmployeesDTO : RespondDTO
    {
        public String UserName { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public String Address { get; set; }

        public String Password { get; set; }

        public String Birthday { get; set; }

        public List<StatsDTO> Stats { get; set; }

        private static List<StatsDTO> ConvertStatsToDTO(ICollection<StatsAndPays> m) => new(m.ToList().Select(x => new StatsDTO
        {
            Id = x.Id,
            Name = x.Name,
            Salary = x.Salary
        }));

        public static explicit operator EmployeesDTO(Employees m) => new EmployeesDTO
        {
            Id = m.Id,
            Name = m.Name,
            UserName = m.UserName,
            Email = m.Email,
            Address = m.Address,
            Birthday = m.Birthday,
            Stats = ConvertStatsToDTO(m.Stat)
        };

        public static explicit operator Employees(EmployeesDTO m) => new Employees
        {
            Id = m.Id,
            Name = m.Name,
            UserName = m.UserName,
            Email = m.Email,
            Address = m.Address,
            Birthday = m.Birthday,
            Stat = new List<StatsAndPays>(m.Stats.ToList().Select(a => new StatsAndPays { Name = a.Name, Salary = a.Salary })),
        };

    }
}