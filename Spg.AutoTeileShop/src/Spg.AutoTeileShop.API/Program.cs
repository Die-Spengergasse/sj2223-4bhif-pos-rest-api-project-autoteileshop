using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


//DB
//DbContextOptions options = new DbContextOptionsBuilder()
//    .UseSqlite("Data Source=AutoTeileShop.db")
//    .Options;

//AutoTeileShopContext db = new AutoTeileShopContext(options);
//db.Database.EnsureDeleted();
//db.Database.EnsureCreated();




// Add services to the container.

builder.Services.AddControllers();
//Add Db Servic

builder.Services.AddDbContext<AutoTeileShopContext>(options =>
                options.UseSqlite("Data Source=AutoTeileShop.db"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



