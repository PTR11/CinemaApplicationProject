using CinemaApplicationProject.Model.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
        
        public int GetIncomeOnLastWeek()
        {
            Dictionary<Products, int> tmp = new Dictionary<Products, int>();

            foreach(Products product in context.BuffetSales.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).Select(m => m.Product).Distinct())
            {
                int piece = context.BuffetSales.Where(m => m.Product.Equals(product) && m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).Count();
                int price = context.Products.Where(m => m.Name.Equals(product.Name)).Select(m => m.Price).Single();
                tmp.Add(product,price*price);
            }

            return tmp.Values.Sum();
        }

        #endregion

        #region BuffetWarehouse
        public List<BuffetWarehouse> GetWarehouse() => context.BuffetWarehouse.ToList();

        public int GetQuantityofProductById(int id) => context.BuffetWarehouse.FirstOrDefault(m => m.ProductId == id).Quantity;

        public int GetPriceOfQuantityOfProductById(int id) => context.BuffetWarehouse.FirstOrDefault(m => m.ProductId == id).Quantity * context.Products.FirstOrDefault(m => m.Id == id).Price;

        #endregion

        #region EmployeePresence

        public EmployeePresence GetEmployeePresenceById(int id) => context.EmployeePresence.FirstOrDefault(m => m.Id == id);

        public List<Employees> GetEmployeesFromPresenceByDate(DateTime date) => context.EmployeePresence.Where(m => m.Day.Date.Equals(date.Date)).Select(m =>m.Employee).ToList();

        public List<Employees> GetEmployeesFromPresenceByDateAndStat(DateTime date, StatsAndPays stat) => context.EmployeePresence.Where(m => m.Day.Date.Equals(date.Date)).Select(m => m.Employee).Where(m => m.Stat.Contains(stat)).ToList();

        #endregion

        #region Movies

        public List<Movies> GetMovies() => context.Movies.ToList();

        public Movies GetMovieById(int id) => context.Movies.FirstOrDefault(m => m.Id == id);

        public List<Movies> GetMoviesByNamePart(String name = null) => context.Movies.Where(m => m.Title.StartsWith(name ?? null)).ToList();

        #endregion

        #region MoviesStatistics
        public MoviesStatistics GetBestMovieOnWeek() => context.MoviesStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).OrderBy(m => m.AverageRating).FirstOrDefault();

        public List<MoviesStatistics> GetLastWeekStatisticsOfMovies() => context.MoviesStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).ToList();

        public int GetViewrsNumberLastWeek() => context.MoviesStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).Sum(m => m.ViewersNumber);

        public List<MoviesStatistics> GetAllViewrsWithMovieLastWeek() => context.MoviesStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).ToList();

        #endregion

        #region
        public List<Opinions> GetAllOpinions() => context.Opinions.ToList();

        public List<Opinions> GetAllOpinionsByMovie(String name = null) => context.Opinions.Where(m => m.Movie.Title.Equals(name ?? null)).ToList();

        public List<Opinions> GetAllOpinionsByUser(String username = null) => context.Opinions.Where(m => m.Guest.UserName.Equals(username ?? null)).ToList();

        public Dictionary<Movies, float> GetAvarageRatingOfMovies()
        {
            Dictionary<Movies, float> dict = new Dictionary<Movies, float>();
            foreach (Movies movie in context.Opinions.Select(m => m.Movie).Distinct())
            {
                float value = context.Opinions.Where(m => m.Movie.Equals(movie)).Sum(m => m.Ranking) / context.Opinions.Where(m => m.Movie.Equals(movie)).Count();
                dict.Add(movie, value);
            }
            return dict;
        }

        #endregion

        #region Products
        public List<Products> GetAllProducts() => context.Products.ToList();

        public int GetProductPrice(String name = null) => context.Products.Where(m => m.Name.Equals(name)).Select(m => m.Price).Single();

        #endregion

        #region ProductsStatistics
        public List<ProductStatistics> GetAllSellsOnLastWeek() => context.ProductStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).ToList();

        public Products GetTheMostSelledItemLastWeek() => (Products) context.ProductStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).OrderBy(m => m.BuyersNumber).GroupBy(m => m.Product).Single().Select(m => m.Product);


        #endregion
    }
}
