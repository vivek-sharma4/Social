using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

public class Program
{
    private readonly IConfiguration _configuration;

    // Constructor to inject IConfiguration
    public Program(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

        // Add UserService for dependency injection
        services.AddScoped<UserService>();

        // Add controllers
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
