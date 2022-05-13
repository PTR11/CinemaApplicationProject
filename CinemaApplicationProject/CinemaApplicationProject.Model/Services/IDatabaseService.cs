using CinemaApplicationProject.Model.Database;
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

        public Actors GetActorById(int id);

        public Actors GetActorsByName(String name);

        public List<Actors> GetActorsByMovie(int id);

        public void ConnectMovieWithActor(int movieId, int actorId);

        public void DeleteActorFromMovie(int movieId, int actorId);

        #endregion

        #region BuffetWarehouse

        public List<BuffetWarehouse> GetWarehouse();

        public BuffetWarehouse GetProductInWareHouse(int id);

        public bool SellProducts(ProductSellingDTO dto);

        public List<ProductStatDTO> ProductStatistics();

        #endregion

        #region EmployeePresence

        public bool AddEmployeeToEmployeePresence(Employees employee, string type);

        #endregion

        #region Movies

        public List<Movies> GetMovies();

        public Movies GetMovieById(int id);

        public List<Movies> GetTodaysMovies();

        public List<Movies> GetMoviesByNamePart(String name = null);

        public List<Movies> GetMoviesByCategory(String category);

        public List<MoviesDTO> GetStatisticsForMovies();

        #endregion

        #region Opinions

        public List<Opinions> GetAllOpinionsByMovie(int id);

        public Task<Boolean> SaveOpinionAsync(OpinionsDTO rfg);

        #endregion

        #region Products

        public Products GetProductByName(String name);

        public BuffetWarehouse GetProductById(int id);

        #endregion

        #region Rents

        public List<Rents> GetAllRentsByShowId(int id);
        public List<Guests> GetAllRentUserByShowId(int id);
        public Boolean IfReservedPlace(int showid, int x, int y);

        public Task<Boolean> SaveRents(RentFromGuestDTO rfg);

        #endregion

        #region Rooms

        public List<Rooms> GetAllRooms();

        public Rooms GetRoomById(int id);

        public bool IsRoomFree(int id, DateTime date);

        #endregion

        #region Shows
        public List<Shows> GetAllShows();

        public List<Shows> GetTodaysShows();

        public Shows GetShowById(int id);

        public List<Shows> GetAllShowsByMovieId(int id);

        public List<DateTime> GetAvailableDates();

        public List<Movies> GetShowsByDate(String date);

        #endregion

        #region StatsAndPays
        public List<StatsAndPays> GetStats();

        public StatsAndPays GetStatById(int id);

        public Task<List<string>> GetStatsById(int id);

        public StatsAndPays GetStatByName(String name);

        #endregion

        #region Tickets

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
