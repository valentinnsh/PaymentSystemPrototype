using System.Collections.Immutable;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PaymentSystemPrototype;
using PaymentSystemPrototype.Services;


var builder = WebApplication.CreateBuilder(args);

// var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
// builder.Services.AddDbContext<AppDbContext>(o =>
//     o.UseNpgsql(
//         connectionString
//     )
// );
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(connectionString));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserOperationsService, UserOperationsService>();
//builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => //CookieAuthenticationOptions
    {
        options.Cookie.Name = "IamACookie";
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

builder.Services.AddMvc();
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapDefaultControllerRoute();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }