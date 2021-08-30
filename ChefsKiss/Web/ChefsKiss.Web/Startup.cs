namespace ChefsKiss.Web
{
    using System.Reflection;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.IO;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Infrastructure.Extensions;
    using ChefsKiss.Web.Models;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using static ChefsKiss.Common.Helpers;

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
            services.AddDbContext<RecipesDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
            .AddIdentity()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<RecipesDbContext>();

            services.AddMemoryCache();

            services.ConfigureCustomLoginRoute();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            // Application services
            services.AddTransient<IRecipesService, RecipesService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<IImagesService, ImagesService>();
            services.AddTransient<IImageOperator, ImageOperator>();
            services.AddTransient<IMeasurementUnitsService, MeasurementUnitsService>();
            services.AddTransient<IRecipeIngredientsService, RecipeIngredientsService>();
            services.AddTransient<IIngredientsService, IngredientsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAuthorsService, AuthorsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IFavoritesService, FavoritesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(RecipeFormModel).GetTypeInfo().Assembly);

            if (env.IsDevelopment())
            {
                app.ReseedDatabase();
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Errors/Index");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Errors/Index/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("areasRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultRoute", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                      name: "unmappedRoute",
                      pattern: "{*url}",
                      defaults: new
                      {
                          controller = ControllerName<HomeController>(),
                          action = nameof(ErrorsController.Index),
                      });
                endpoints.MapRazorPages();
            });
        }
    }
}
