using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Spg.AutoTeileShop.API.Helper;
using Org.BouncyCastle.Asn1.Cmp;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.DbExtentions;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.ServiceExtentions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add Transient for Services and Repos
builder.Services.AddAllTransient();

builder.Services.AddFluentValidationAutoValidation();

//DB
builder.Services.ConfigureSQLite(connectionString);
// Add services to the container.

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
    s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First())
    );


// NuGet: Microsoft.AspNetCore.Mvc.Versioning
builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(2, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddSwaggerGen(s =>
{

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
    });

    s.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "AutoTeile Shop - v2",
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
        Version = "v2"
    });

});




builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:7058");
    });
});

string jwtSecret = builder.Configuration["AppSettings:Secret"] ?? AuthService.GenerateRandom(1024);

//Authorizatio
builder.Services.AddJwtAuthentication(jwtSecret, setDefault: false);
builder.Services.AddCookieAuthentication(setDefault: true);

//Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOrAdmin", policy =>
    {
        policy.Requirements.Add(new CustomAuthorizationRequirement("User", "Admin"));
    });

    options.AddPolicy("SalesmanOrAdmin", policy =>
    {
        policy.Requirements.Add(new CustomAuthorizationRequirement("Salesman", "Admin"));
    });

    options.AddPolicy("UserOrSalesmanOrAdmin", policy =>
    {
        policy.Requirements.Add(new CustomAuthorizationRequirement("User", "Salesman", "Admin"));
    });
});

builder.Services.AddTransient<IAuthorizationHandler, CustomAuthorizationHandler>();



//AuthService

builder.Services.AddTransient<AuthService>(services =>
{
    var userRepository = services.GetRequiredService<IUserRepository>();
    return new AuthService(jwtSecret, userRepository);
}
);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        x.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.UseCors("myAllowSpecificOrigins");
app.MapControllers();

//HATEOAS
var linksv1 = new List<string>();
// Get Controller names
var assembly = typeof(Program).Assembly;
var controllerTypes = assembly.GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract);

var controllerNames = controllerTypes.Select(x => x.Name.Replace("Controller", "")).ToList();

// Build Links
for (int i = 0; i <= (controllerNames.Count / 2) - 1; i++)
{
    linksv1.Add("https://localhost:7083/api/v1/" + controllerNames[i]);
}

var linksv2 = new List<string>();

for (int i = 0; i <= (controllerNames.Count / 2) - 1; i++)
{
    linksv2.Add("https://localhost:7083/api/v2/" + controllerNames[i]);
}

// links to Json
var Controllerv1 = new { Controller = linksv1 };
var Controllerv2 = new { Controller = linksv2 };


var jsonsConv1 = JsonConvert.SerializeObject(Controllerv1, Formatting.Indented);
var jsonsConv2 = JsonConvert.SerializeObject(Controllerv2, Formatting.Indented);


// Version Links
var versionList = new List<string>
{
    "https://localhost:7083/api/v1",
    "https://localhost:7083/api/v2"
};

// Version to Json
var jsonsVersion = JsonConvert.SerializeObject(versionList, Formatting.Indented);

// Minimal API


app.MapControllers();

app.MapGet("/api/", () => jsonsVersion);
app.MapGet("/api/v1", () => jsonsConv1);
app.MapGet("/api/v2", () => jsonsConv2);
app.Run();

