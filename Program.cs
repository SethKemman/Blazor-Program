using BlazorProject.Components;
using BlazorProject.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();


        // Add database context
        builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(
            "Server=localhost;User ID=root;Password=root;Database=application;",
            ServerVersion.AutoDetect("Server=localhost;User ID=root;Password=root;Database=application")
        )

    );


        var app = builder.Build();

        // Initialize database with seed data
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}