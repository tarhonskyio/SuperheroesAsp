using Microsoft.EntityFrameworkCore;
using SuperheroesAspNet.Models.Superheroes;

namespace SuperheroesAspNet;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddDbContext<SuperheroesContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("SuperheroesDatabase")));

        var app = builder.Build();


        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}