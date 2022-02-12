using CinemaApplicationProject.Model.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplicationProject.Model
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, StatsAndPays, int>
    {
        public DatabaseContext(){}

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<ApplicationUser>().ToTable("ApplicationUser");
			builder.Entity<Employees>().ToTable("Employees");
			builder.Entity<Guests>().ToTable("Guests");
			builder.Entity<StatsAndPays>().ToTable("Stats");
			builder.Entity<Categories>().HasIndex(u => u.Category).IsUnique();
		}

		public virtual DbSet<Actors> Actors { get; set; }
		public virtual DbSet<BuffetSale> BuffetSales { get; set; }
		public virtual DbSet<BuffetWarehouse> BuffetWarehouse { get; set; }
		public virtual DbSet<EmployeePresence> EmployeePresence { get; set; }
		public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
		public virtual DbSet<Employees> Employees { get; set; }
		public virtual DbSet<Guests> Guests { get; set; }
		public virtual DbSet<Movies> Movies { get; set; }
		public virtual DbSet<Opinions> Opinions { get; set; }
		public virtual DbSet<Products> Products { get; set; }
		public virtual DbSet<Rents> Rents { get; set; }
		public virtual DbSet<Rooms> Rooms { get; set; }
		public virtual DbSet<Shows> Shows { get; set; }
		public virtual DbSet<ProductStatistics> ProductStatistics { get; set; }
		public virtual DbSet<MoviesStatistics> MoviesStatistics { get; set; }
		public virtual DbSet<Tickets> Tickets { get; set; }
		public virtual DbSet<StatsAndPays> StatsAndPays { get; set; }

		public virtual DbSet<Categories> Categories { get; set; }


	}
}
