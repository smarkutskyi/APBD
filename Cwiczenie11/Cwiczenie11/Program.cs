using Cwiczenie11.Data;
using Cwiczenie11.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("Cwiczenie11");

builder.Services.AddAuthorization();
builder.Services.AddControllers();


builder.Services.AddDbContext<DataBaseContext>(options => 
    options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IDbService, DbService>();



var app = builder.Build();

app.UseHttpsRedirection();


app.MapControllers();


app.Run();

