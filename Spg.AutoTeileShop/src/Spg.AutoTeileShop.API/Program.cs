using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.DbExtentions;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;

var builder = WebApplication.CreateBuilder(args);



string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//DB
builder.Services.AddTransient<IAddUpdateableProductService, ProductService>();
builder.Services.AddTransient<IReadOnlyProductService, ProductService>();
builder.Services.AddTransient<IDeletableProductService, ProductService>();

builder.Services.AddTransient<IProductRepositroy, ProductRepository>();
builder.Services.ConfigureSQLite(connectionString);



// Add services to the container.

builder.Services.AddControllers();
//Add Db Servic

builder.Services.AddDbContext<AutoTeileShopContext>(options =>
                options.UseSqlite("Data Source = AutoTeileShop.db"));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "AutoTeile Shop - v1",
        Description = "Description about AutoTeileShop",
        Contact = new OpenApiContact()
        {
            Name = "David Ankenbrand and Johannes Scholz",
            Email = "ank19415@spengergasse.at",
            Url = new Uri("http://www.spengergasse.at")
        },
        
        License = new OpenApiLicense()
        {
            Name = "Spenger-Licence",
            Url = new Uri("http://www.spengergasse.at/licence")
        },
        Version = "v1"
    }));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:7058");
    });
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

app.UseCors("myAllowSpecificOrigins");

app.MapControllers();

app.Run();



