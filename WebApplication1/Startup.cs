using AutoServiceApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceApi;

public class Startup(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AutoServiceContext>(options =>
            options.UseSqlite("Data Source=AutoService.db"));

        services.AddControllers();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}