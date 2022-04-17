﻿using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Services
{
    public interface IDatabaseService
    {

        public DatabaseContext GetContext();

        #region Actors

        public List<Actors> GetActors();

        public List<Actors> GetActorsWithMovies();

        public Actors GetActorById(int id);

        public Actors GetActorsByName(String name);

        public List<Actors> GetActorsByMovie(int id);

        public void ConnectMovieWithActor(int movieId, int actorId);

        public void DeleteActorFromMovie(int movieId, int actorId);

        #endregion

        #region BuffetSales

        public List<BuffetSale> GetBuffetSales();

        public List<BuffetSale> GetBuffetSaleByProductId(int id);

        public int GetAverageSaleOfProductOnAWeek(int id);

        public List<BuffetSale> GetBuffetSalesByEmployeeId(int id);

        public List<BuffetSale> GetBuffetSalesOfADay();

        public List<BuffetSale> GetBuffetSalesOfADayByEmployeeId(int id);

        public int GetIncomeOnLastWeek();

        #endregion

        #region BuffetWarehouse
        public List<BuffetWarehouse> GetWarehouse();

        public int GetQuantityofProductById(int id);

        public int GetPriceOfQuantityOfProductById(int id);
        public BuffetWarehouse GetProductInWareHouse(int id);

        public bool SellProducts(ProductSellingDTO dto);

        public List<ProductStatDTO> ProductStatistics();

        #endregion

        #region EmployeePresence

        public EmployeePresence GetEmployeePresenceById(int id);

        public bool AddEmployeeToEmployeePresence(Employees employee, string type);

        //public List<Employees> GetEmployeesFromPresenceByDate(DateTime date);

        //public List<Employees> GetEmployeesFromPresenceByDateAndStat(DateTime date, StatsAndPays stat);

        #endregion

        #region Movies

        public List<Movies> GetMovies();

        public Movies GetMovieById(int id);

        public List<Movies> GetTodaysMovies();

        public List<Movies> GetMoviesByNamePart(String name = null);

        public List<Movies> GetMoviesByCategory(String category);

        public Task UpdateMovieActors(List<Actors> actors, int movieId);

        public List<MoviesDTO> GetStatisticsForMovies();

        #endregion

        #region MoviesStatistics
        public MoviesStatistics GetBestMovieOnWeek();

        public List<MoviesStatistics> GetLastWeekStatisticsOfMovies();

        public int GetViewrsNumberLastWeek();

        public List<MoviesStatistics> GetAllViewrsWithMovieLastWeek();

        #endregion

        #region Opinions
        public List<Opinions> GetAllOpinions();

        public List<Opinions> GetAllOpinionsByMovie(int id);

        public List<Opinions> GetAllOpinionsByUser(String username = null);

        public Dictionary<Movies, float> GetAvarageRatingOfMovies();

        public Task<Boolean> SaveOpinionAsync(OpinionsDTO rfg);

        #endregion

        #region Products
        public List<Products> GetAllProducts();

        public Products GetProductByName(String name);

        public BuffetWarehouse GetProductById(int id);

        public int GetProductPrice(String name = null);

        #endregion

        #region ProductsStatistics
        public List<ProductStatistics> GetAllSellsOnLastWeek();

        public Products GetTheMostSelledItemLastWeek();


        #endregion

        #region Rents
        public List<Rents> GetAllRents();

        public List<Rents> GetAllSelledRents();

        public List<Rents> GetAllRentsByGuestId(int id);

        public List<Rents> GetAllRentsByShowId(int id);
        public List<Guests> GetAllRentUserByShowId(int id);
        public Boolean IfReservedPlace(int showid, int x, int y);

        public Task<Boolean> SaveRents(RentFromGuestDTO rfg);

        #endregion

        #region Rooms

        public List<Rooms> GetAllRooms();

        public Rooms GetRoomById(int id);

        #endregion

        #region Shows
        public List<Shows> GetAllShows();

        public List<Shows> GetTodaysShows();

        public Shows GetShowById(int id);

        public List<Shows> GetAllShowsOnNextWeek();

        public List<Shows> GetAllShowsByMovieId(int id);

        public List<Shows> GetAllShowsByRoomId(int id);

        public List<DateTime> GetAvailableDates();

        public List<Movies> GetShowsByDate(String date);

        #endregion

        #region StatsAndPays
        public List<StatsAndPays> GetStats();

        public StatsAndPays GetStatById(int id);

        public Task<List<string>> GetStatsById(int id);

        public int GetSalaryOfStatById(int id);

        public StatsAndPays GetStatByName(String name);

        #endregion

        #region Tickets
        public int GetPriceOfTicketById(int id);

        public List<Tickets> GetTickets();

        public Tickets GetTicketById(int id);
        #endregion

        #region Categories

        public List<Categories> GetCategories();

        public Categories GetCategoryById(int id);

        public Categories GetCategoryByName(String cat);

        public void ConnectMovieWithCategory(int movieId, int actorId);

        public void DeleteCategoryFromMovie(int movieId, int catId);

        #endregion

        #region Guest

        public Guests GetGuestByUserName(String username);

        #endregion

        #region Employee
        public Task<List<Employees>> GetEmployees();

        public Task<List<Employees>> GetEmployeesByRole(String role);

        public Task<Employees> GetEmployeeById(int id);

        public Task<bool> ConnectUserWithRole(int userId, int roleId);

        public Employees GetEmployeeByUserName(String userName);
        #endregion
    }
}
