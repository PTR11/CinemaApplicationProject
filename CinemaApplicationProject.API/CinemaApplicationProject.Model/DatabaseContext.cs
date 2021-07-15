using CinemaApplicationProject.Model.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaApplicationProject.Model
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DatabaseContext(){}

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<ApplicationUser>().ToTable("ApplicationUser");

			builder.Entity<Guests>(b =>
			{
				b.HasKey(u => u.Id);
				b.Property(u => u.Name).HasColumnName("Name").IsRequired();
				b.Property(u => u.Address).HasColumnName("Address").IsRequired();
				b.ToTable("Guests");
			});

			builder.Entity<Employees>(b =>
			{
				b.HasKey(u => u.Id);
				b.Property(u => u.Name).HasColumnName("Name").IsRequired();
				b.Property(u => u.Address).HasColumnName("Address").IsRequired();
				b.Property(u => u.Birthday).HasColumnName("BirthDay").IsRequired();
				b.ToTable("Employees");
			});
			builder.Entity<Employees>()
			.HasOne(a => a.User)
			.WithOne(a => a.Employees)
			.HasForeignKey<ApplicationUser>(c => c.EmployeeId);

			builder.Entity<Guests>()
			.HasOne(a => a.User)
			.WithOne(a => a.Guests)
			.HasForeignKey<ApplicationUser>(c => c.EmployeeId);
		}

		public virtual DbSet<Actors> Actors { get; set; }
		public virtual DbSet<BuffetSale> BuffetSales { get; set; }
		public virtual DbSet<BuffetWarehouse> BuffetWarehouse { get; set; }
		public virtual DbSet<EmployeePresence> EmployeePresence { get; set; }
		public virtual DbSet<Employees> Employees { get; set; }
		public virtual DbSet<Guests> Guests { get; set; }
		public virtual DbSet<Movies> Movies { get; set; }
		public virtual DbSet<Opinions> Opinions { get; set; }
		public virtual DbSet<Products> Products { get; set; }
		public virtual DbSet<Rents> Rents { get; set; }
		public virtual DbSet<Rooms> Rooms { get; set; }
		public virtual DbSet<Shows> Shows { get; set; }
		public virtual DbSet<StatsAndPays> StatsAndPays { get; set; }
	}
}
