using CinemaApplicationProject.Model.Database;
using CinemaApplicationProject.Model.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Services
{
    public class DatabaseService : IDatabaseService
    {
        private DatabaseContext context;
        private readonly UserManager<ApplicationUser> guestManager;

        public DatabaseService(DatabaseContext dc, UserManager<ApplicationUser> um)
        {
            context = dc;
            guestManager = um;
        }

        public DatabaseContext GetContext() => this.context;

        #region Actors

        public List<Actors> GetActors() => context.Actors.ToList();

        public List<Actors> GetActorsWithMovies() => context.Actors.Include(m => m.Movies).ToList();

        public Actors GetActorById(int id) => context.Actors.FirstOrDefault(m => m.Id == id);

        public Actors GetActorsByName(String name) => context.Actors.FirstOrDefault(m => m.Name.Equals(name));

        public List<Actors> GetActorsByMovie(int movieId)
        {
            Movies movie = context.Movies.FirstOrDefault(m => m.Id == movieId);
            return context.Actors.Where(a => a.Movies.Contains(movie)).ToList();
        }



        public void ConnectMovieWithActor(int movieId, int actorId) {
            if (movieId != 0)
            {
                var hasConnection = context.Movies.Include(m => m.Actors).FirstOrDefault(m => m.Id == movieId).Actors.FirstOrDefault(m => m.Id == actorId);
                if (hasConnection == null)
                {
                    context.Movies.FirstOrDefault(m => m.Id == movieId).Actors.Add(context.Actors.FirstOrDefault(m => m.Id == actorId));
                    context.SaveChanges();
                }
            }
        }
        #endregion

        #region BuffetSales

        public List<BuffetSale> GetBuffetSales() => context.BuffetSales.ToList();

        public List<BuffetSale> GetBuffetSaleByProductId(int id) => context.BuffetSales.Where(m => m.Id == id).ToList();

        public int GetAverageSaleOfProductOnAWeek(int id) => context.BuffetSales.Where(m => m.Id == id && m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).Sum(m => m.Quantity);

        public List<BuffetSale> GetBuffetSalesByEmployeeId(int id) => context.BuffetSales.Where(m => m.EmployeeId == id).ToList();

        public List<BuffetSale> GetBuffetSalesOfADay() => context.BuffetSales.Where(m => m.Date.Day.Equals(DateTime.Now.Day)).ToList();

        public List<BuffetSale> GetBuffetSalesOfADayByEmployeeId(int id) => context.BuffetSales.Where(m => m.Date.Day.Equals(DateTime.Now.Day) && m.EmployeeId == id).ToList();

        public int GetIncomeOnLastWeek()
        {
            Dictionary<Products, int> tmp = new();

            foreach (Products product in context.BuffetSales.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).Select(m => m.Product).Distinct())
            {
                int piece = context.BuffetSales.Where(m => m.Product.Equals(product) && m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).Count();
                int price = context.Products.Where(m => m.Name.Equals(product.Name)).Select(m => m.Price).Single();
                tmp.Add(product, price * price);
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

        public List<Employees> GetEmployeesFromPresenceByDate(DateTime date) => context.EmployeePresence.Where(m => m.Day.Date.Equals(date.Date)).Select(m => m.Employee).ToList();

        public List<Employees> GetEmployeesFromPresenceByDateAndStat(DateTime date, StatsAndPays stat) => context.EmployeePresence.Where(m => m.Day.Date.Equals(date.Date)).Select(m => m.Employee).Where(m => m.Stat.Contains(stat)).ToList();

        #endregion

        #region Movies

        public Movies GetMovie(int id) => context.Movies.Include(m => m.Shows).Include(m => m.Actors).Include(m => m.Categories).FirstOrDefault(m => m.Id==id);

        public List<Movies> GetMovies() => context.Movies.Include(m => m.Actors).Include(m => m.Categories).ToList();

        public List<Movies> GetTodaysMovies() {

            var shows = this.GetTodaysShows().Select(x => x.MovieId).Distinct().ToList();
            return context.Movies.Where(x => shows.Contains(x.Id)).ToList();
        }

        public Movies GetMovieById(int id) => context.Movies.Include(m => m.Actors).Include(m => m.Shows).FirstOrDefault(m => m.Id == id);

        public List<Movies> GetMoviesByNamePart(String name = null) => context.Movies.Where(m => m.Title.StartsWith(name ?? null)).ToList();

        public List<Movies> GetMoviesByCategory(String category)
        {
            Categories cat = context.Categories.FirstOrDefault(m => m.Category.Equals(category));

            return context.Movies.Include(m => m.Categories).Include(m => m.Actors).Where(m => m.Categories.Contains(cat)).ToList();
        }

        
        #endregion

        #region MoviesStatistics
        public MoviesStatistics GetBestMovieOnWeek() => context.MoviesStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).OrderBy(m => m.AverageRating).FirstOrDefault();

        public List<MoviesStatistics> GetLastWeekStatisticsOfMovies() => context.MoviesStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).ToList();

        public int GetViewrsNumberLastWeek() => context.MoviesStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).Sum(m => m.ViewersNumber);

        public List<MoviesStatistics> GetAllViewrsWithMovieLastWeek() => context.MoviesStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).ToList();

        #endregion

        #region Opinions
        public List<Opinions> GetAllOpinions() => context.Opinions.ToList();

        public List<Opinions> GetAllOpinionsByMovie(int id) => context.Opinions.Where(m => m.Movie.Id == id).Include(m => m.Guest).ToList();

        public List<Opinions> GetAllOpinionsByUser(String username = null) => context.Opinions.Where(m => m.Guest.UserName.Equals(username ?? null)).ToList();

        public Dictionary<Movies, float> GetAvarageRatingOfMovies()
        {
            Dictionary<Movies, float> dict = new();
            foreach (Movies movie in context.Opinions.Select(m => m.Movie).Distinct())
            {
                float value = context.Opinions.Where(m => m.Movie.Equals(movie)).Sum(m => m.Ranking) / context.Opinions.Where(m => m.Movie.Equals(movie)).Count();
                dict.Add(movie, value);
            }
            return dict;
        }

        public async Task<Boolean> SaveOpinionAsync(OpinionsDTO rfg)
        {
            if (rfg.GuestId == 0 || rfg.MovieId == 0)
            {
                return false;
            }
            Guests guest = context.Guests.FirstOrDefault(m => m.Id == rfg.GuestId);
            //Guests guest = (Guests)await guestManager.FindByIdAsync(rfg.UserId.ToString());

            if (guest == null)
            {
                return false;
            }

            Opinions opinion = new Opinions
            {
                Guest = guest,
                Movie = context.Movies.FirstOrDefault(m => m.Id == rfg.MovieId),
                Anonymus = rfg.Anonymus,
                Description = rfg.Description,
                DateTime = DateTime.Now,
                Ranking = rfg.Ranking,
            };

            context.Opinions.Add(opinion);
            
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

            #endregion

        #region Products
            public List<Products> GetAllProducts() => context.Products.ToList();

        public int GetProductPrice(String name = null) => context.Products.Where(m => m.Name.Equals(name)).Select(m => m.Price).Single();

        #endregion

        #region ProductsStatistics
        public List<ProductStatistics> GetAllSellsOnLastWeek() => context.ProductStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).ToList();

        public Products GetTheMostSelledItemLastWeek() => (Products)context.ProductStatistics.Where(m => m.Date >= DateTime.Now.AddDays(-7) && m.Date <= DateTime.Now).OrderBy(m => m.BuyersNumber).GroupBy(m => m.Product).Single().Select(m => m.Product);


        #endregion

        #region Rents
        public List<Rents> GetAllRents() => context.Rents.ToList();

        public List<Rents> GetAllSelledRents() => context.Rents.Where(m => m.Employee != null).ToList();

        public List<Rents> GetAllRentsByGuestId(int id) => context.Rents.Where(m => m.GuestId == id).ToList();

        public List<Rents> GetAllRentsByShowId(int id) => context.Rents.Where(m => m.ShowId == id).ToList();

        public Boolean IfReservedPlace(int showid, int x, int y) => context.Rents.Where(m => m.ShowId == showid).Where(m => m.X == x && m.Y == y).Any();

        public async Task<Boolean> SaveRentsAsync(RentFromGuestDTO rfg)
        {
            if (rfg.UserId == 0 || rfg.ShowId == 0)
            {
                return false;
            }
            Guests guest = context.Guests.FirstOrDefault(m => m.Id == rfg.UserId);
            //Guests guest = (Guests)await guestManager.FindByIdAsync(rfg.UserId.ToString());

            if (guest == null)
            {
                return false;
            }

            foreach (var place in rfg.Places)
            {
                if (this.IfReservedPlace(rfg.ShowId, place.X, place.Y))
                {
                    return false;
                }
            }
            foreach (var place in rfg.Places)
            {
                Rents rent = new Rents
                {
                    Guest = guest,
                    ShowId = rfg.ShowId,
                    X = place.X,
                    Y = place.Y,
                    Ticket = context.Tickets.FirstOrDefault(m => m.Type == place.TicketCategory)
                };

                context.Rents.Add(rent); 
            }
            try
            {
                context.SaveChanges();
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        #endregion

        #region Rooms

        public List<Rooms> GetAllRooms() => context.Rooms.ToList();

        public Rooms GetRoomById(int id) => context.Rooms.Where(m => m.Id == id).Single();

        #endregion

        #region Shows
        public List<Shows> GetAllShows() => context.Shows.Include(m => m.Movie).Include(m => m.Room).ToList();

        public List<Shows> GetTodaysShows() => context.Shows.Where(x => x.Date.Date == DateTime.Now.Date).ToList();

        public Shows GetShowById(int id) => context.Shows.Include(m => m.Room).Include(m => m.Movie).FirstOrDefault(m => m.Id == id);

        public List<Shows> GetAllShowsOnNextWeek() => context.Shows.Where(m => m.Date <= DateTime.Now.AddDays(+7) && m.Date >= DateTime.Now).ToList();

        public List<Shows> GetAllShowsByMovieId(int id) => context.Shows.Where(m => m.MovieId == id).ToList();

        public List<Shows> GetAllShowsByRoomId(int id) => context.Shows.Where(m => m.RoomId == id).ToList();

        public List<DateTime> GetAvailableDates() => new List<DateTime>() { context.Shows.Where(m => m.Date.Date >= DateTime.Now.Date && m.IsActiveShow).Select(m => m.Date).OrderBy(m => m.Date).ToList().First(), context.Shows.Where(m => m.Date.Date >= DateTime.Now.Date && m.IsActiveShow).Select(m => m.Date).OrderBy(m => m.Date).ToList().Last() };

        public String DateTimeToString(DateTime date) => date.Date.ToString("MM/dd/yyyy");

        public List<Movies> GetShowsByDate(String date)
        {
           
            var shows = context.Shows.Where(m => m.Date.Date == DateTime.Parse(date).Date).Select(m => m.Id).ToList(); //Shows ids


            List<Movies> movies = new List<Movies>();
            movies.AddRange(context.Movies.Include(m => m.Shows).AsNoTracking().ToList());
            List<Movies> tmps = new List<Movies>();
            foreach (var movie in movies)
            {
                foreach(var show in movie.Shows)
                {
                    if (!shows.Contains(show.Id))
                    {
                        movie.Shows.Remove(show);
                        
                    }
                }
                if (movie.Shows.Count != 0)
                {
                    tmps.Add(movie);
                }
            }
            return tmps;
        }
        #endregion

        #region StatsAndPays
        public List<StatsAndPays> GetStats() => context.StatsAndPays.ToList();

        public StatsAndPays GetStatById(int id) => context.StatsAndPays.FirstOrDefault(m => m.Id == id);

        public int GetSalaryOfStatById(int id) => context.StatsAndPays.Where(m => m.Id == id).Select(m => m.Salary).Single();

        #endregion

        #region Tickets
        public int GetPriceOfTicketById(int id) => context.Tickets.Where(m => m.Id == id).Select(m => m.Price).Single();

        public List<Tickets> GetTickets() => context.Tickets.ToList();

        public Tickets GetTicketById(int id) => context.Tickets.FirstOrDefault(t => t.Id == id);
        #endregion

        #region Categories

        public List<Categories> GetCategories() => context.Categories.ToList();

        public Categories GetCategoryById(int id) => context.Categories.FirstOrDefault(m => m.Id==id);

        public Categories GetCategoryByName(String cat) => context.Categories.FirstOrDefault(c => c.Category.Equals(cat));

        public void ConnectMovieWithCategory(int movieId, int catId)
        {
            if (movieId != 0)
            {
                var hasConnection = context.Movies.Include(m => m.Categories).FirstOrDefault(m => m.Id == movieId).Categories.FirstOrDefault(m => m.Id == catId);
                if (hasConnection == null)
                {
                    context.Movies.FirstOrDefault(m => m.Id == movieId).Categories.Add(context.Categories.FirstOrDefault(m => m.Id == catId));
                    context.SaveChanges();
                }
            }
        }

        #endregion

        #region Guest

        public Guests GetGuestByUserName(String username) => context.Guests.FirstOrDefault(g => g.UserName.Equals(username));

        #endregion

    }
}
