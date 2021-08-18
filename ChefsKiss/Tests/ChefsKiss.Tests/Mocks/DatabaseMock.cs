namespace ChefsKiss.Tests.Mocks
{
    using System;

    using ChefsKiss.Data;

    using Microsoft.EntityFrameworkCore;

    public class DatabaseMock
    {
        public static RecipesDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<RecipesDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new RecipesDbContext(dbContextOptions);
            }
        }
    }
}
