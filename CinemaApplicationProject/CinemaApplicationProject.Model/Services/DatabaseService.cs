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
                    var actor = context.Actors.FirstOrDefault(m => m.Id == actorId);
                    if(actor != null)
                    {
                        context.Movies.FirstOrDefault(m => m.Id == movieId).Actors.Add(actor);
                        context.SaveChanges();
                    }
                }
            }
        }

        public void DeleteActorFromMovie(int movieId, int actorId)
        {
            if (movieId != 0)
            {
                var hasConnection = context.Movies.Include(m => m.Actors).FirstOrDefault(m => m.Id == movieId).Actors.FirstOrDefault(m => m.Id == actorId);
                if (hasConnection != null)
                {
                    context.Movies.FirstOrDefault(m => m.Id == movieId).Actors.Remove(context.Actors.FirstOrDefault(m => m.Id == actorId));
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
        public List<BuffetWarehouse> GetWarehouse() => context.BuffetWarehouse.Include(m => m.Product).ToList();

        public BuffetWarehouse GetProductInWareHouse(int id) => context.BuffetWarehouse.Include(m => m.Product).FirstOrDefault(m => m.Id == id);

        public int GetQuantityofProductById(int id) => context.BuffetWarehouse.FirstOrDefault(m => m.ProductId == id).Quantity;

        public int GetPriceOfQuantityOfProductById(int id) => context.BuffetWarehouse.FirstOrDefault(m => m.ProductId == id).Quantity * context.Products.FirstOrDefault(m => m.Id == id).Price;


        public bool SellProducts(ProductSellingDTO dto)
        {
            foreach(var product in dto.Products)
            {
                var item = context.BuffetWarehouse.Include(m => m.Product).FirstOrDefault(m => m.Id == product.ProductId);
                if(item != null)
                {
                    int quantity = item.Quantity;
                    if(quantity-product.Count < 0)
                    {
                        throw new Exception("There is not enough product ("+item.Product.Name+")");
                    }
                    else
                    {
                        item.Quantity -= product.Count;
                    }
                }
                else
                {
                    throw new Exception("Current product is not found");
                }
            }


            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                return false;
            }
            return true;
        }
        #endregion

        #region EmployeePresence

        public EmployeePresence GetEmployeePresenceById(int id) => context.EmployeePresence.FirstOrDefault(m => m.Id == id);

        public bool AddEmployeeToEmployeePresence(Employees employee, string type)
        {
            if (type.Equals("login"))
            {
                context.EmployeePresence.Add(new EmployeePresence
                {
                    DutyTime = 0,
                    Employee = employee,
                    Login = DateTime.Now.Date
                });
            }
            else
            {
                var find = context.EmployeePresence.Where(e => e.Id == employee.Id).OrderByDescending(e => e.Login).First();
                find.Logout = DateTime.Now.Date;
                find.DutyTime = (int)(find.Logout - find.Login).TotalHours;
            }

            try
            {
                context.SaveChanges();
                return true;
            }catch(DbUpdateException)
            {
                return false;
            }
        }

        //public List<Employees> GetEmployeesFromPresenceByDate(DateTime date) => context.EmployeePresence.Where(m => m.Day.Date.Equals(date.Date)).Select(m => m.Employee).ToList();

        //public List<Employees> GetEmployeesFromPresenceByDateAndStat(DateTime date, StatsAndPays stat) => context.EmployeePresence.Where(m => m.Day.Date.Equals(date.Date)).Select(m => m.Employee).Where(m => m.Stat.Contains(stat)).ToList();

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

        public async Task UpdateMovieActors(List<Actors> actors, int movieId)
        {

            var movie = context.Movies.FirstOrDefault(m => m.Id == movieId);
            movie.Actors.Clear();
            var movieActors = movie.Actors.ToList();
            foreach(var actor in movie.Actors)
            {
                var updated = actors.Contains(actor);
                if (!updated)
                {
                    movieActors.Remove(actor);
                }
                actors.Remove(actor);
            }
            foreach (var actor in movie.Actors)
            {
                movie.Actors.Add(actor);
            }
            await context.SaveChangesAsync();
        }

        public List<MoviesDTO> GetStatisticsForMovies()
        {
            var listOfMovies = context.Movies.Include(m => m.Opinions).ToList();
            List<MoviesDTO> result = new List<MoviesDTO>(listOfMovies.Select(m => (MoviesDTO)m).ToList());
            foreach(var movie in result)
            {
                movie.TicketsCount = context.Rents.Include(m => m.Show).ThenInclude(m => m.Movie).Where(r => r.EmployeeId != 0 && r.Show.MovieId == movie.Id).ToList().Count;
                int count = listOfMovies.FirstOrDefault(m => m.Id == movie.Id).Opinions.Count;  
                if(count != 0)
                    movie.Average = listOfMovies.FirstOrDefault(m => m.Id == movie.Id).Opinions.Sum(m => m.Ranking) / (double)count;
            }
            return result;
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

        public Products GetProductByName(String name) => context.Products.FirstOrDefault(m => m.Name.Equals(name));

        public BuffetWarehouse GetProductById(int id) => context.BuffetWarehouse.Include(m => m.Product).FirstOrDefault(m => m.Id == id);

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

        public List<Rents> GetAllRentsByShowId(int id) => context.Rents.Where(m => m.ShowId == id).Include(m => m.Guest).ToList();

        public List<Guests> GetAllRentUserByShowId(int id) => context.Rents.Where(m => m.ShowId == id).Include(m => m.Guest).Select(m => m.Guest).Distinct().ToList();
        public Boolean IfReservedPlace(int showid, int x, int y) => context.Rents.Where(m => m.ShowId == showid).Where(m => m.X == x && m.Y == y).Any();

        public async Task<bool> SaveRents(RentFromGuestDTO rfg)
        {
            if (rfg.ShowId == 0)
            {
                return false;
            }

            if(rfg.IsEmployee && rfg.UserId != 0 && rfg.EmployeeId != 0)
            {
                foreach(var place in rfg.Places)
                {
                    Rents find = context.Rents.FirstOrDefault(r => r.GuestId == rfg.UserId && r.ShowId == rfg.ShowId && r.X == place.X && r.Y ==place.Y && r.Ticket == context.Tickets.FirstOrDefault(t => t.Type == place.TicketCategory));
                    if (find != null)
                    {
                        find.Employee = context.Employees.FirstOrDefault(m => m.Id == rfg.EmployeeId);

                        context.Update(find);
                    }
                }
            }
            else
            {
                Guests guest = context.Guests.FirstOrDefault(m => m.Id == rfg.UserId);
                //Guests guest = (Guests)await guestManager.FindByIdAsync(rfg.UserId.ToString());
                Employees employees = context.Employees.FirstOrDefault(m => m.Id == rfg.EmployeeId);

                if (guest == null && rfg.IsEmployee == false)
                {
                    return false;
                }
                else if (guest != null)
                {
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
                }


                if (employees == null && rfg.IsEmployee == true)
                {
                    return false;
                }
                else if (employees != null)
                {
                    foreach (var place in rfg.Places)
                    {
                        if (this.IfReservedPlace(rfg.ShowId, place.X, place.Y))
                        {
                            return false;
                        }
                    }
                    foreach (var place in rfg.Places)
                    {
                        Place tmp = place;
                        Rents rent = new Rents
                        {
                            Employee = employees,
                            ShowId = rfg.ShowId,
                            X = tmp.X,
                            Y = tmp.Y,
                            Ticket = context.Tickets.FirstOrDefault(m => m.Type == place.TicketCategory)
                        };

                        context.Rents.Add(rent);
                    }
                }
            }


            

            
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

        #region Rooms

        public List<Rooms> GetAllRooms()
        {
            var list = context.Rooms.Include(m => m.Shows).ToList();
            list.ForEach(r =>
            {
                foreach(var show in r.Shows)
                {
                    show.Movie = context.Movies.FirstOrDefault(m => m.Id == show.MovieId);
                }
            });
            return list;
        }

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

        public StatsAndPays GetStatByName(String name) => context.StatsAndPays.FirstOrDefault(m => m.Name.Equals(name));

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

        public void DeleteCategoryFromMovie(int movieId, int catId)
        {
            if (movieId != 0)
            {

                var hasConnection = context.Movies.Include(m => m.Categories).FirstOrDefault(m => m.Id == movieId).Categories.FirstOrDefault(m => m.Id == catId);
                if (hasConnection != null)
                {
                    context.Movies.FirstOrDefault(m => m.Id == movieId).Categories.Remove(context.Categories.FirstOrDefault(m => m.Id == catId));
                    context.SaveChanges();
                }
            }
        }
        #endregion

        #region Guest

        public Guests GetGuestByUserName(String username) => context.Guests.FirstOrDefault(g => g.UserName.Equals(username));

        #endregion

        #region Employee

        public async Task<List<Employees>> GetEmployees()
        {
            var emps = context.Employees.Include(m => m.Presence).ToList();
            foreach(var employee in emps)
            {
                var rolesString = await guestManager.GetRolesAsync(employee);
                var roles = context.StatsAndPays.Where(m => rolesString.Contains(m.Name));
                employee.Stat = new List<StatsAndPays>(roles);
            }
            return emps;
        }

        public async Task<Employees> GetEmployeeById(int id)
        {
            var user = context.Employees.FirstOrDefault(m => m.Id == id);
            var rolesString =await guestManager.GetRolesAsync(user);
            var roles = context.StatsAndPays.Where(m => rolesString.Contains(m.Name));
            user.Stat = new List<StatsAndPays>(roles);
            
            return user;
        }


        public async Task<bool> ConnectUserWithRole(int userId, int roleId)
        {
            if (userId != 0)
            {
                var user = context.Employees.FirstOrDefault(m => m.Id == userId);
                var rolesString = await guestManager.GetRolesAsync(user);
                var role = context.StatsAndPays.FirstOrDefault(m => m.Id == roleId);

                var hasConnection = rolesString.Contains(role.Name);
                if (hasConnection == false)
                {
                    var result = await guestManager.AddToRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Employees GetEmployeeByUserName(String userName) => context.Employees.FirstOrDefault(e => e.UserName.Equals(userName));

        #endregion

    }
}
