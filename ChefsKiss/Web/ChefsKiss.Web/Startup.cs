namespace ChefsKiss.Web
{
    using System.Reflection;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Data.Seeding;
    using ChefsKiss.Services.IO;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;
    using ChefsKiss.Web.Areas.Recipes.Services;
    using ChefsKiss.Web.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RecipesDbContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<RecipesDbContext>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            // services.Configure<IdentityOptions>(options => { });

            // Application services
            services.AddTransient<IRecipesService, RecipesService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<IImagesService, ImagesService>();
            services.AddTransient<IImageOperator, ImageOperator>();
            services.AddTransient<IMeasurementUnitsService, MeasurementUnitsService>();
            services.AddTransient<IRecipeIngredientsService, RecipeIngredientsService>();
            services.AddTransient<IIngredientsService, IngredientsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IWritersService, WritersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(RecipeFormModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<RecipesDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.EnsureDeleted();
                }

                dbContext.Database.Migrate();

                new ApplicationDbContextSeeder()
                    .SeedAsync(dbContext, serviceScope.ServiceProvider)
                    .GetAwaiter()
                    .GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
               endpoints =>
                   {
                       endpoints.MapControllerRoute("areasRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                       endpoints.MapControllerRoute("defaultRoute", "{controller=Home}/{action=Index}/{id?}");
                       endpoints.MapRazorPages();
                   });
        }
    }
}
