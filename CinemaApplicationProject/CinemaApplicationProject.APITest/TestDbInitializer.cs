﻿using CinemaApplicationProject.Model;
using CinemaApplicationProject.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.APITest
{
    public class TestDbInitializer
    {
		public static void Initialize(DatabaseContext _context)
		{
			_context.Database.EnsureCreated();
			//_userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			//_roleManager = serviceProvider.GetRequiredService<RoleManager<StatsAndPays>>();

			// Adatbázis migrációk végrehajtása, amennyiben szükséges
			//_context.Database.Migrate();
			// Városok, épületek, apartmanok inicializálás
			Products p = new Products { Name = "Sajt", Price = 100 };
			Products t = new Products { Name = "Perec", Price = 100 };
			Products f = new Products { Name = "kukac", Price = 100 };
			_context.Products.Add(p);
			_context.BuffetWarehouse.Add(new BuffetWarehouse { Product = p, Quantity = 12 });
			_context.Products.Add(t);
			_context.BuffetWarehouse.Add(new BuffetWarehouse { Product = t, Quantity = 12 });
			_context.Products.Add(f);
			_context.BuffetWarehouse.Add(new BuffetWarehouse { Product = f, Quantity = 12 });

			//_context.ChangeTracker.LazyLoadingEnabled = false;




			List<Actors> actors = new List<Actors>
			{
				new Actors
				{
					Name = "Mark Hamill"
				},
				new Actors
				{
					Name = "Harrison Ford"
				},
				new Actors
				{
					Name = "Carrie Fisher"
				},
				new Actors
				{
					Name = "Leonardo DiCaprio"
				},
				new Actors
				{
					Name = "Kate Winslet"
				},
				new Actors
				{
					Name = "Billy Zane"
				},
				new Actors
				{
					Name = "Chris Evans"
				},
				new Actors
				{
					Name = "Hugo Weaving"
				},
				new Actors
				{
					Name = "Dominic Cooper"
				},
				new Actors
				{
					Name = "Vivianne Bánovits"
				},
				new Actors
				{
					Name = "András Mózes"
				},
				new Actors
				{
					Name = "Barna Bokor"
				},
				new Actors
				{
					Name = "Arnold Schwarzenegger"
				},
				new Actors
				{
					Name = "Nick Stahl"
				},
				new Actors
				{
					Name = "Claire Danes"
				},
				new Actors
				{
					Name = "Chris Pratt"
				},
				new Actors
				{
					Name = "Channing Tatum"
				},
				new Actors
				{
					Name = "Will Arnett"
				}
			};

			_context.Actors.AddRange(actors);

			List<Categories> categories = new List<Categories>
			{
				new Categories{
					Category = "Animation"
				},
				new Categories{
					Category = "Action"
				},
				new Categories{
					Category = "Adventure"
				},
				new Categories{
					Category = "Sci-Fi"
				},
				new Categories{
					Category = "Crime"
				},
				new Categories{
					Category = "Drama"
				},
				new Categories{
					Category = "Romance"
				},
				new Categories{
					Category = "Fanatasy"
				},
			};

			_context.AddRange(categories);

			List<Movies> movies = new List<Movies>
			{
				new Movies
				{
					Title = "Star Wars - A birodalom visszavág",
					Length = 124,
					Description = "A Halálcsillag elpusztítása után Luke Skywalker, Han Solo, Leia Organa hercegnő és a Lázadó Szövetség menekülni kényszerülnek a Galaktikus Birodalom Darth Vader által vezetett erői elől. Luke elszakad barátaitól és egy félreeső bolygón Yoda jedi mestertől megtanulja használni az Erőt.",
					Trailer = "",
					Actors = actors.GetRange(0,3),
					Categories = new List<Categories>
					{
						categories[1],categories[2],categories[7]
					}
				},
				new Movies
				{
					Title = "Titanic",
					Length = 194,
					Description = "1912-ben indul útjára a világ legnagyobb, legelegánsabb, legbiztonságosabbnak vélt óceánjárója, a Titanic. A csodálatos hajón sokféle ember utazik, köztük Jack (Leonardo DiCaprio) és Rose (Kate Winslet), akik két teljesen különböző világból érkeztek, de mégis összehozza őket a sors. Amikor a vesztébe száguldó hajó jéghegynek ütközik, már nem csak szerelmükért, hanem az életben maradásukért is meg kell küzdeniük.",
					Trailer = "",
					Actors = actors.GetRange(3,3),
					Categories = new List<Categories>
					{
						categories[5],categories[6]
					}
				},
				new Movies
				{
					Title = "Amerika kapitány - Az első bosszúálló",
					Length = 124,
					Description = "Javában dúl a második világháború 1941-ben. A lelkes és mindenre elszánt ifjú, Steve Rogers is jelentkezik a hadseregbe, ám a gyenge fizikuma miatt kiszuperálják a sorozáson. Ezért örömmel jelentkezik a titkos katonai kísérleti programba, az Újjászületés nevet viselő projektbe, melynek keretében szuperkatonát csinálnak belőle. Az izomkolosszussá fejlődött Amerika kapitány feladata megállítani a nácik titkos tudománmyos részlegét, a Hydrát, melyet a világuralmi törekvéseket dédelgető, rettegett Vörös Koponya vezet.",
					Trailer = "",
					Actors = actors.GetRange(6,3),
					Categories = new List<Categories>
					{
						categories[1],categories[2],categories[3]
					}
				},
				new Movies
				{
					Title = "Elk*rtuk",
					Length = 125,
					Description = "2006-ban Budapesten valami összetört. Az akkor tizenhat éves Magyar Köztársaság vezetőjének kiszivárgott “őszödi beszéde” alapjaiban rengette meg az emberek demokráciába és a kommunizmus utáni rendszerváltozásba vetett hitét.",
					Trailer = "",
					Actors = actors.GetRange(9,3),
					Categories = new List<Categories>
					{
						categories[4],categories[5]
					}
				},
				new Movies
				{
					Title = "Terminátor 3 - A gépek lázadása",
					Length = 109,
					Description = "Egy évtized telt el azóta, hogy John Connornak sikerült megakadályoznia azt a napot, amikor a Skynet csúcsfejlesztésű gépei öntudatra ébredtek, és a világ elpusztítására törtek. Connornak mára, 22 évesen sikerült tökéletesen köddé válnia, lehetetlen a nyomára bukkanni. Mindaddig, amíg egy napon a jövőből elő nem bukkan a T-X, a Skynet eddigi legtökéletesebb, nőnek álcázott cyborg gyilkológépe, akit azzal a feladattal küldtek vissza az időben, hogy fejezze be elődje, a T-1000 félbehagyott munkáját. Connornak egyetlen esélye korábbi ellenfele, akit ezúttal egy új feladattal küldtek vissza.",
					Trailer = "",
					Actors = actors.GetRange(12,3),
					Categories = new List<Categories>
					{
						categories[1],categories[3]
					}
				},
				new Movies
				{
					Title = "Lego Kaland",
					Length = 100,
					Description = "Emmet átlagos fickó. A keze sárga, két ujja van, a feje tetején pedig bütyök tartja a sapkát. A Lego-világ átlagembere, aki egy építkezésen dolgozik, és semmi pénzért se késné le kedvenc napi tévésorozatát. Egy félreértés folytán azonban mindenki más azt hiszi: ő a Kiválasztott, a világ megmentésének kulcsfigurája. Különleges alakok egy kis csapatával kell nekivágnia a nagy feladatnak, ami meghökkentő kalandokon és váratlan fordulatokon keresztül eljuttathatja hőseinket... majd meglátjuk, hova. Annyi biztos: a kettős életet élő Lord Biznisz a vesztükre tör, Emmet pedig Vitruvius, a bölcs öreg, egy vad Lego-lány, Batman és még néhány furcsa fickó oldalán, gyorsan alakuló járgányok és könnyen átépíthető világok között harcol – de alig érti, mi zajlik körülötte.",
					Trailer = "",
					Actors = actors.GetRange(15,3),
					Categories = new List<Categories>
					{
						categories[0],categories[1],categories[2]
					}
				},
				new Movies
				{
					Title = "Lego Kaland2",
					Length = 100,
					Description = "Emmet átlagos fickó. A keze sárga, két ujja van, a feje tetején pedig bütyök tartja a sapkát. A Lego-világ átlagembere, aki egy építkezésen dolgozik, és semmi pénzért se késné le kedvenc napi tévésorozatát. Egy félreértés folytán azonban mindenki más azt hiszi: ő a Kiválasztott, a világ megmentésének kulcsfigurája. Különleges alakok egy kis csapatával kell nekivágnia a nagy feladatnak, ami meghökkentő kalandokon és váratlan fordulatokon keresztül eljuttathatja hőseinket... majd meglátjuk, hova. Annyi biztos: a kettős életet élő Lord Biznisz a vesztükre tör, Emmet pedig Vitruvius, a bölcs öreg, egy vad Lego-lány, Batman és még néhány furcsa fickó oldalán, gyorsan alakuló járgányok és könnyen átépíthető világok között harcol – de alig érti, mi zajlik körülötte.",
					Trailer = "",
					Actors = actors.GetRange(15,3),
					Categories = new List<Categories>
					{
						categories[0],categories[1],categories[2]
					}
				}
			};

			_context.Movies.AddRange(movies);

			List<Rooms> rooms = new List<Rooms>
			{
				new Rooms
				{
					Name = "Koponya",
					Width = 12,
					Heigth = 10
				},
				new Rooms
				{
					Name = "Kristály",
					Width = 20,
					Heigth = 20
				},
				new Rooms
				{
					Name = "Kopár",
					Width = 6,
					Heigth = 6
				},
			};

			_context.Rooms.AddRange(rooms);

			List<Shows> shows = new List<Shows>
			{
				new Shows
				{
					Room = rooms[0],
					Movie = movies[0],
					Date = DateTime.Now.AddHours(-3),
					IsActiveShow = true
				},
				new Shows
				{
					Room = rooms[1],
					Movie = movies[0],
					Date = DateTime.Now.AddDays(-7).AddHours(-1),
					IsActiveShow = true
				},
				new Shows
				{
					Room = rooms[1],
					Movie = movies[0],
					Date = DateTime.Now.AddDays(2).AddHours(-2).AddMinutes(15),
					IsActiveShow = true
				},

			};

			_context.Shows.AddRange(shows);

			List<Tickets> tickets = new List<Tickets>
			{
				new Tickets
				{
					Price = 1200,
					Type = "normal"
				},
				new Tickets
				{
					Price = 900,
					Type = "student"
				},
				new Tickets
				{
					Price = 700,
					Type = "retired"
				},
			};

			_context.AddRange(tickets);



			var guest = new Guests
			{
				UserName = "ptr1",
				Name = "Sanyi",
				Email = "s.p@asd.com",
				Address = "anyadban",
				CreditCardNumber = "123asd321"
			};




			var adminRole = new StatsAndPays("administrator");
			adminRole.Salary = 2500;
			var ticketerRole = new StatsAndPays("ticket seller");
			ticketerRole.Salary = 1250;
			var buffetSellerRole = new StatsAndPays("buffet seller");
			var buffetSWRole = new StatsAndPays("buffet sw");
			var ticketerSWRole = new StatsAndPays("ticket sw");
			buffetSellerRole.Salary = 1600;

			var adminUser = new Employees
			{
				UserName = "admin",
				Name = "Barnák Péter",
				Email = "barnak.peter1@gmail.com",
				Address = "Nyíregyháza, Honvéd utca 61",
				Birthday = "2000/06/15",

			};

			var adminUser2 = new Guests
			{
				UserName = "admin2",
				Name = "Barn",
				Email = "barnak.peter12@gmail.com",
				Address = "faszomban",
				CreditCardNumber = "KurvaAnyádat"

			};
			var adminPassword = "Almafa123";



			//adminUser.Stat = new List<StatsAndPays>();
			//adminUser.Stat.Add(adminRole);
			//_ = _userManager.CreateAsync(adminUser, adminPassword).Result;
			//_ = _userManager.CreateAsync(adminUser2, adminPassword).Result;
			//_ = _userManager.CreateAsync(guest, adminPassword).Result;
			//_ = _roleManager.CreateAsync(adminRole).Result;
			//_ = _userManager.AddToRoleAsync(adminUser, adminRole.Name).Result;
			//_ = _roleManager.CreateAsync(ticketerRole).Result;
			//_ = _roleManager.CreateAsync(buffetSellerRole).Result;
			//_ = _roleManager.CreateAsync(ticketerSWRole).Result;
			//_ = _roleManager.CreateAsync(buffetSWRole).Result;

		}
	}
}