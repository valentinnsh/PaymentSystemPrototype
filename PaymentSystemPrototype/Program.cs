using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PaymentSystemPrototype;
using PaymentSystemPrototype.Services;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new Exception("Couldn't get connection string");
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(connectionString));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IKycService, KycService>();
builder.Services.AddScoped<ITransferOperationsService, TransferOperationsService>();
builder.Services.AddScoped<IUserOperationsService, UserOperationsService>();
builder.Services.AddRazorPages();


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

builder.Services.AddMvc().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/Auth/LogIn", "");
});
var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }