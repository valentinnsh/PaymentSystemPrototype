using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using PaymentSystemPrototype;


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

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseMvc();
app.Run();