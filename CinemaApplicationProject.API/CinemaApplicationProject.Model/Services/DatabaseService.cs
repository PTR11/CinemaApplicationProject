using CinemaApplicationProject.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly DatabaseContext context;
        #region Actors

        public List<Actors> GetActors() => context.Actors.ToList();

        public List<Actors> GetActorsWithMovies() => context.Actors.Include(m => m.Movies).ToList();

        public Actors GetActorById(int id) => context.Actors.FirstOrDefault(m => m.Id == id);

        public List<Actors> GetActorsByNameParts(String part) => context.Actors.Where(m => m.Name.StartsWith(part)).ToList();

        //TODO rework a little bit that
        public List<Actors> GetActorsByMovieName(String name) 
            => context.Actors.Where(m => m.Movies.Contains(new Movies { Title = name })).ToList();

        #endregion

        #region BuffetSales

        public List<BuffetSale> GetBuffetSales() => context.BuffetSales.ToList();

        public List<BuffetSale> GetBuffetSaleByProductId(int id) => context.BuffetSales.Where(m => m.Id == id).ToList();

        public int GetAverageSaleOfProductOnAWeek(int id) => context.BuffetSales.Where(m => m.Id == id && m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now ).Sum(m => m.Quantity);

        public List<BuffetSale> GetBuffetSalesByEmployeeId(int id) => context.BuffetSales.Where(m => m.EmployeeId == id).ToList();

        public List<BuffetSale> GetBuffetSalesOfADay() => context.BuffetSales.Where(m => m.Date.Day.Equals(DateTime.Now.Day)).ToList();

        public List<BuffetSale> GetBuffetSalesOfADayByEmployeeId(int id) => context.BuffetSales.Where(m => m.Date.Day.Equals(DateTime.Now.Day) && m.EmployeeId == id).ToList();
        #endregion

        #region BuffetWarehouse
        
        #endregion
    }
}
