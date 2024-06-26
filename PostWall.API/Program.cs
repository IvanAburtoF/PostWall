using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostWall.API.Models.EF;
using PostWall.API.Repositories;
using PostWall.API.Services;
using PostWall.Data;
namespace PostWall.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        

        
        builder.Services.AddDbContext<PostWallDbContext>(options =>
        {
            options.UseSqlite("Data Source=PostWall.db");
        });

        builder.Services.AddIdentityCore<ApplicationUser>(options =>
        {
            
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<PostWallDbContext>()
        .AddSignInManager<SignInManager<ApplicationUser>>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
        })
        .AddCookie(IdentityConstants.ApplicationScheme, options =>
        {
            options.Cookie.Name = "PostWall.Cookie";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = 403;
                return Task.CompletedTask;
            };
        }
        );


        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<IMediaRepository, MediaRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
