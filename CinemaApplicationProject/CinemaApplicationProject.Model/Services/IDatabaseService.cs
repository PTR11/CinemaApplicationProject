using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Services
{
    public interface IDatabaseService
    {
        #region Actors

        public List<Actors> GetActors();

        public List<Actors> GetActorsWithMovies();

        public Actors GetActorById(int id);

        public List<Actors> GetActorsByNameParts(String part);

        public List<Actors> GetActorsByMovieName(String name);

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

        #endregion

        #region EmployeePresence

        public EmployeePresence GetEmployeePresenceById(int id);

        public List<Employees> GetEmployeesFromPresenceByDate(DateTime date);

        public List<Employees> GetEmployeesFromPresenceByDateAndStat(DateTime date, StatsAndPays stat);

        #endregion

        #region Movies

        public List<Movies> GetMovies();

        public Movies GetMovieById(int id);

        public List<Movies> GetTodaysMovies();

        public List<Movies> GetMoviesByNamePart(String name = null);

        public List<Movies> GetMoviesByCategory(String category);

        #endregion

        #region MoviesStatistics
        public MoviesStatistics GetBestMovieOnWeek();

        public List<MoviesStatistics> GetLastWeekStatisticsOfMovies();

        public int GetViewrsNumberLastWeek();

        public List<MoviesStatistics> GetAllViewrsWithMovieLastWeek();

        #endregion

        #region Opinions
        public List<Opinions> GetAllOpinions();

        public List<Opinions> GetAllOpinionsByMovie(String name = null);

        public List<Opinions> GetAllOpinionsByUser(String username = null);

        public Dictionary<Movies, float> GetAvarageRatingOfMovies();

        #endregion

        #region Products
        public List<Products> GetAllProducts();

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

        public int GetSalaryOfStatById(int id);

        #endregion

        #region Tickets
        public int GetPriceOfTicketById(int id);
        #endregion

        #region Categories

        public List<Categories> GetCategories();

        #endregion
    }
}
