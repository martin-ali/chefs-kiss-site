namespace ChefsKiss.Data.Seeding
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using ChefsKiss.Data.Models;

	using Microsoft.AspNetCore.Identity;

	using static ChefsKiss.Common.WebConstants;

	public class AuthorsSeeder : IDataSeeder
	{
		private static readonly string[] FirstNames = new[]
		{
			"Liam",
			"Noah",
			"Oliver",
			"Elijah",
			"William",
			"James",
			"Benjamin",
			"Lucas",
			"Henry",
			"Alexander",
			"Mason",
			"Michael",
			"Ethan",
			"Daniel",
			"Jacob",
			"Logan",
			"Jackson",
			"Levi",
			"Sebastian",
			"Jack",
			"Owen",
			"Theodore",
			"Aiden",
			"Samuel",
			"Joseph",
			"John",
			"David",
			"Wyatt",
			"Matthew",
			"Luke",
			"Asher",
			"Carter",
			"Julian",
			"Grayson",
			"Leo",
			"Jayden",
			"Gabriel",
			"Isaac",
			"Lincoln",
			"Anthony",
			"Hudson",
			"Dylan",
			"Ezra",
			"Thomas",
			"Charles",
			"Christopher",
			"Maverick",
			"Josiah",
			"Isaiah",
			"Andrew",
			"Elias",
			"Joshua",
			"Nathan",
			"Caleb",
			"Ryan",
			"Adrian",
			"Miles",
			"Eli",
			"Nolan",
			"Christian",
			"Aaron",
			"Cameron",
			"Ezekiel",
			"Colton",
			"Luca",
			"Landon",
			"Hunter",
			"Jonathan",
			"Santiago",
			"Axel",
			"Easton",
			"Cooper",
			"Jeremiah",
			"Angel",
			"Roman",
			"Connor",
			"Jameson",
			"Robert",
			"Greyson",
			"Jordan",
			"Ian",
			"Carson",
			"Leonardo",
			"Nicholas",
			"Dominic",
			"Austin",
			"Everett",
			"Brooks",
			"Xavier",
			"Kai",
			"Jose",
			"Parker",
			"Adam",
			"Wesley",
			"Kayden",
			"Silas",
			"Bennett",
			"Declan",
			"Waylon",
			"Weston",
			"Evan",
			"Emmett",
			"Micah",
			"Ryder",
			"Beau",
			"Damian",
			"Brayden",
			"Gael",
			"Rowan",
			"Harrison",
			"Sawyer",
			"Amir",
			"Kingston",
			"Jason",
			"Giovanni",
			"Vincent",
			"Chase",
			"Myles",
			"Diego",
			"Nathaniel",
			"Legend",
			"Jonah",
			"River",
			"Tyler",
			"Cole",
			"Braxton",
			"George",
			"Milo",
			"Zachary",
			"Ashton",
			"Luis",
			"Jasper",
			"Gavin",
			"Bentley",
			"Calvin",
			"Zion",
			"Juan",
			"Maxwell",
			"Max",
			"Carlos",
			"Emmanuel",
			"Lorenzo",
			"Ivan",
			"Jude",
			"August",
			"Kevin",
			"Malachi",
			"Elliott",
			"Archer",
			"Arthur",
			"Elliot",
			"Brandon",
			"Camden",
			"Justin",
			"Jesus",
			"Maddox",
			"King",
			"Theo",
			"Enzo",
			"Matteo",
			"Emiliano",
			"Dean",
			"Hayden",
			"Finn",
			"Brody",
			"Antonio",
			"Abel",
			"Alex",
			"Tristan",
			"Graham",
			"Judah",
			"Xander",
			"Miguel",
			"Atlas",
			"Messiah",
			"Barrett",
			"Tucker",
			"Timothy",
			"Alan",
			"Edward",
			"Leon",
			"Dawson",
			"Eric",
			"Ace",
			"Victor",
			"Abraham",
			"Nicolas",
			"Jesse",
			"Charlie",
			"Patrick",
			"Walker",
			"Joel",
			"Richard",
			"Beckett",
			"Blake",
			"Alejandro",
			"Avery",
			"Grant",
			"Peter",
			"Oscar",
			"Matias",
			"Andres",
			"Arlo",
			"Colt",
			"Adonis",
			"Kyrie",
			"Steven",
			"Felix",
			"Preston",
			"Marcus",
			"Holden",
			"Emilio",
			"Remington",
			"Jeremy",
			"Brantley",
			"Bryce",
			"Mark",
			"Knox",
			"Phoenix",
			"Kobe",
			"Nash",
			"Griffin",
			"Kenneth",
			"Hayes",
			"Jax",
			"Rafael",
			"Beckham",
			"Javier",
			"Maximus",
			"Simon",
			"Paul",
		};

		private readonly string[] LastNames = new[]
		{
			"Smith",
			"Johnson",
			"Williams",
			"Brown",
			"Jones",
			"Garcia",
			"Miller",
			"Davis",
			"Rodriguez",
			"Martinez",
			"Hernandez",
			"Lopez",
			"Gonzalez",
			"Wilson",
			"Anderson",
			"Thomas",
			"Taylor",
			"Moore",
			"Jackson",
			"Martin",
			"Lee",
			"Perez",
			"Thompson",
			"White",
			"Harris",
			"Sanchez",
			"Clark",
			"Ramirez",
			"Lewis",
			"Robinson",
			"Walker",
			"Young",
			"Allen",
			"King",
			"Wright",
			"Scott",
			"Torres",
			"Nguyen",
			"Hill",
			"Flores",
			"Green",
			"Adams",
			"Nelson",
			"Baker",
			"Hall",
			"Rivera",
			"Campbell",
			"Mitchell",
			"Carter",
			"Roberts",
		};

		public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
		{
			var random = new Random();
			var users = dbContext
				.Users
				.Where(x => x.UserName.Contains("admin") == false)
				.Where(x => x.UserName.Contains("testuser") == false)
				.ToList();
			var userManager = (UserManager<ApplicationUser>)serviceProvider.GetService(typeof(UserManager<ApplicationUser>));

			int authorsCount = users.Count / 2;
			for (int i = 0; i < authorsCount; i++)
			{
				var firstName = FirstNames[random.Next(0, FirstNames.Length)];
				var lastName = LastNames[random.Next(0, LastNames.Length)];
				var user = users[random.Next(0, users.Count)];
				var isApproved = i % 2 == 0;

				var author = new Author
				{
					FirstName = firstName,
					LastName = lastName,
					User = user,
					IsApproved = isApproved,
				};

				dbContext.Authors.Add(author);
				await userManager.AddToRoleAsync(user, AuthorRoleName);
			}

			await dbContext.SaveChangesAsync();
		}
	}
}
