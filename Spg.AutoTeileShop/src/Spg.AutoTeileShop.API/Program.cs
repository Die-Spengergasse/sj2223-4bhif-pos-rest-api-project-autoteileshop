using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.DbExtentions;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;
using Spg.AutoTeileShop.ServiceExtentions;

var builder = WebApplication.CreateBuilder(args);



string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add Transient for Services and Repos
builder.Services.AddAllTransient();




//DB
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

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        //new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
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



