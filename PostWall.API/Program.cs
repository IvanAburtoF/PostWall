
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostWall.Data;
namespace PostWall.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

        builder.Services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<PostWallDbContext>()
            .AddApiEndpoints();

        builder.Services.AddDbContext<PostWallDbContext>(options =>
        {
            options.UseSqlite("Data Source=PostWall.db");
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapIdentityApi<ApplicationUser>();

        app.MapControllers();

        app.Run();
    }
}
