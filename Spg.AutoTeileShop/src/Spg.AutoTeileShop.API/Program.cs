using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Asn1.Cmp;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Application.Validators;
using Spg.AutoTeileShop.DbExtentions;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserInterfaces;
using Spg.AutoTeileShop.Domain.Interfaces.UserMailConfirmInterface;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using Spg.AutoTeileShop.Repository2.Repositories;
using Spg.AutoTeileShop.ServiceExtentions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


//product get del -id
//ShoppingCart del put guid
//ShoppingCartItem get ShoppingCar
var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add Transient for Services and Repos
builder.Services.AddAllTransient();

builder.Services.AddFluentValidationAutoValidation();

//DB
builder.Services.ConfigureSQLite(connectionString);
// Add services to the container.

builder.Services.AddControllers();

// Link Generator
//builder.Services.AddSingleton<LinkGenerator>(new LinkGenerator(
//    new ActionContext 
//    {
//        HttpContext = new DefaultHttpContext(),
//        RouteData = new RouteData(),
//        ActionDescriptor = new ActionDescriptor(),

//    },
//    new List<IEndpointRouteBuilder>(),
//    new OptionsManager<RouteOptions>(Enumerable.Empty<IConfigureOptions<RouteOptions>>())
//    ));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
    s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First())
    );


builder.Services.AddSwaggerGen(s =>
{

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

    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token here"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



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
//LinkGenerator linkGenerator = app.Services.GetRequiredService<LinkGenerator>();
//string? url = linkGenerator.GetUriByAction(HttpContext, action: nameof(GetDetails), values: new { name = newId });
// Json json = new Json();


var linksv1 = new List<string>();
//var controllerNameList = new List<string>();

//var endpoints = app.Services.GetService<EndpointDataSource>();
//foreach (var endpoint in endpoints.Endpoints)
//{
//    var controllerEndpoint = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
//    if (controllerEndpoint != null)
//    {
//        var controllerName = controllerEndpoint.ControllerName;
//        controllerNameList.Add(controllerName);
//    }
//}
//links.Add(builder.Services.);


//var asm = Assembly.GetAssembly(typeof(Program));
//var controlleractionlist = asm.GetTypes()
//    .Where(type => typeof(Controllerv1).IsAssignableFrom(type))
//    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
//    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
//    .Select(x => new
//    {
//        Controllerv1 = x.DeclaringType.Name,
//        Action = x.Name,
//        ReturnType = x.ReturnType.Name,
//        Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")))
//    })
//    .OrderBy(x => x.Controllerv1).ThenBy(x => x.Action).ToList();

//var list = new List<String>();
//foreach (var item in controlleractionlist)
//{
//    list.Add(item.Controllerv1);
//}


//var asm = Assembly.GetAssembly(typeof(Program));
//var controllerTypes = asm.GetTypes().Where(type => typeof(Controllerv1).IsAssignableFrom(type));
//var controllerNames = controllerTypes.Select(type => type.Name);
var assembly = typeof(Program).Assembly;
var controllerTypes = assembly.GetTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract);

var controllerNames = controllerTypes.Select(x => x.Name.Replace("Controller", "")).ToList();

for (int i = 0; i <= (controllerNames.Count / 2) - 1; i++)
{
    linksv1.Add("https://localhost:7083/api/v1/" + controllerNames[i]);
}

var linksv2 = new List<string>();

for (int i = 0; i <= (controllerNames.Count / 2) - 1; i++)
{
    linksv2.Add("https://localhost:7083/api/v2/" + controllerNames[i]);
}
//links.Add();
//foreach (string item in controllerNames)
//{
//    links.Add("https://localhost:7083/api/" + item);
//}

var Controllerv1 = new { Controller = linksv1 };
var Controllerv2 = new { Controller = linksv2 };


var jsonsConv1 = JsonConvert.SerializeObject(Controllerv1, Formatting.Indented);
var jsonsConv2 = JsonConvert.SerializeObject(Controllerv2, Formatting.Indented);



var versionList = new List<string>
{
    "https://localhost:7083/api/v1",
    "https://localhost:7083/api/v2"
};


var jsonsVersion = JsonConvert.SerializeObject(versionList, Formatting.Indented);


app.MapGet("/api/", () => jsonsVersion);
app.MapGet("/api/v1", () => jsonsConv1);
app.MapGet("/api/v2", () => jsonsConv2);
app.Run();



