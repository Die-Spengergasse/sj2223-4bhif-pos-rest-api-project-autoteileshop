using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.DbExtentions;
using Spg.AutoTeileShop.Repository2.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepositroy, ProductRepository>();
builder.Services.ConfigureSQLite(connectionString);
//builder.Services.AddDbContext<AutoTeileShopContext>(options =>
//              options.UseSqlite("Data Source = AutoTeileShop.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
