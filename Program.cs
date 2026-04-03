using MormorDagnys.Data;
using Microsoft.EntityFrameworkCore;
using MormorDagnys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MormorDagnysContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlitedev"));
});

builder.Services.AddControllers();

var app = builder.Build();


var seeder = new SeedData();
await seeder.InitDb(app);

app.MapControllers();
app.Run();
