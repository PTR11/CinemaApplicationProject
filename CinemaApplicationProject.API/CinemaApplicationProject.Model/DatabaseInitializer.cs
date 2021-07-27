using CinemaApplicationProject.Model.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
		private static RoleManager<StatsAndPays> _roleManager;


		public static void Initialize(IServiceProvider serviceProvider)
		{
			_context = serviceProvider.GetRequiredService<DatabaseContext>();
			_userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			_roleManager = serviceProvider.GetRequiredService<RoleManager<StatsAndPays>>();

			// Adatbázis migrációk végrehajtása, amennyiben szükséges
			_context.Database.EnsureCreated();

			// Városok, épületek, apartmanok inicializálás
			_context.Products.Add(new Products { Name = "Sajt", Price = 100 });

			var adminUser = new Employees
			{
				UserName = "admin",
				Name = "Barnák Péter",
				Email = "barnak.peter1@gmail.com",
				Address = "faszomban"
			};

			var adminUser2 = new Guests
			{
				UserName = "admin2",
				Name = "Barnák Péter2",
				Email = "barnak.peter12@gmail.com",
				Address = "faszomban",
				CreditCardNumber = "KurvaAnyádat"
			};
			var adminPassword = "Almafa123";
			var adminRole = new StatsAndPays("administrator");
            _ = _userManager.CreateAsync(adminUser, adminPassword).Result;
            _ = _userManager.CreateAsync(adminUser2, adminPassword).Result;
            _ = _roleManager.CreateAsync(adminRole).Result;
            _ = _userManager.AddToRoleAsync(adminUser, adminRole.Name).Result;
        }
	}
}
