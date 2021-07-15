using CinemaApplicationProject.Model.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model
{
    public static class DatabaseInitializer
    {
		private static DatabaseContext _context;
		private static UserManager<ApplicationUser> _userManager;
		private static RoleManager<IdentityRole<int>> _roleManager;


		public static void Initialize(IServiceProvider serviceProvider)
		{
			_context = serviceProvider.GetRequiredService<DatabaseContext>();
			_userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			_roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

			// Adatbázis migrációk végrehajtása, amennyiben szükséges
			_context.Database.EnsureCreated();

			// Városok, épületek, apartmanok inicializálás
			_context.Products.Add(new Products { Name = "Sajt", Price = 100 });
		}
	}
}
