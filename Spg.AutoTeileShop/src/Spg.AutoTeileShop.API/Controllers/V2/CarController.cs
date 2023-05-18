using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;

using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Web.Helpers;
using System.Web.WebPages;

namespace Spg.AutoTeileShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CarController : ControllerBase
    {

        private readonly IReadOnlyCarService _readOnlycarService;
        private readonly IDeletableCarService _deletableCarService;
        private readonly IAddUpdateableCarService _addUpdateableCarService;
        private readonly EndpointDataSource _endpointDataSource;
        private readonly IEnumerable<EndpointDataSource> _endpointSources;

        private string controllerName;
        private string apiVersion;


        public CarController
            (IReadOnlyCarService readOnlycarService, IDeletableCarService deletableCarService,
            IAddUpdateableCarService addUpdateableCarService, EndpointDataSource endpointDataSource,
            IEnumerable<EndpointDataSource> endpointSources)
        {
            _readOnlycarService = readOnlycarService;
            _deletableCarService = deletableCarService;
            _addUpdateableCarService = addUpdateableCarService;
            _endpointDataSource = endpointDataSource;
            _endpointSources = endpointSources;

            this.controllerName = GetType().Name;

            var apiVersionAttribute = (ApiVersionAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ApiVersionAttribute));
            this.apiVersion = apiVersionAttribute?.Versions.FirstOrDefault()?.ToString();

            //.OfType<ApiVersionAttribute>()
            //.FirstOrDefault()
            //?.ApiVersion.ToString();

            getRouteNames();
        }


        private string GetApiVersion()
        {
            var routeAttribute = GetType()
                .GetCustomAttributes(typeof(RouteAttribute), true)
                .OfType<RouteAttribute>()
                .FirstOrDefault();

            if (routeAttribute != null && routeAttribute.Template.Contains("{version:apiVersion}"))
            {
                var templateParts = routeAttribute.Template.Split('/');
                if (templateParts.Length >= 3)
                {
                    return templateParts[1].Substring(1); // Ignoriere das 'v'-Präfix
                }
            }

            return null; // Fallback-Wert, wenn die API-Version nicht gefunden werden kann
        }

        // AddCar - Authorization
        // DeleteCar - Authorization
        // UpdateCar - Authorization

        [HttpGet("")]
        public ActionResult<List<Car>> GetAllCars() // Auslaufend
        {
            return Ok(_readOnlycarService.GetAll());
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<Car> GetCarbyId(int id) 
        {
            try
            {
                Car? car = _readOnlycarService.GetById(id);
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains($"No Car found with Id: {id}")) { return BadRequest(ex.Message); }
                return BadRequest();
            }
        }

        [HttpGet("byBaujahr")]
        public ActionResult<List<Car>> GetByBaujahr([FromQuery] int year)  // Auslaufend
        {
            try
            {
                return Ok(_readOnlycarService.GetByBauJahr(new DateTime(year, 1, 1)));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByMarke")]
        [AllowAnonymous]
        public ActionResult<List<Car>> GetByMarke([FromQuery] string marke)  // Auslaufend
        {
            try
            {
                return Ok(_readOnlycarService.GetByMarke(marke));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByModel")]
        public ActionResult<List<Car>> GetByModell([FromQuery] string model)  // Auslaufend
        {
            try
            {
                return Ok(_readOnlycarService.GetByModell(model));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByMarkeAndModell")]
        public ActionResult<List<Car>> GetByMarkeAndModell([FromQuery] string marke, [FromQuery] string model) // Auslaufend
        {
            try
            {
                return Ok(_readOnlycarService.GetByMarkeAndModell(marke, model));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByMarkeAndModellAndBaujahr")]
        public ActionResult<List<Car>> GetByMarkeAndModellAndBaujahr([FromQuery] string merke, [FromQuery] string model, [FromQuery] int baujahr) // Auslaufend
        {
            try
            {
                return Ok(_readOnlycarService.GetByMarkeAndModellAndBaujahr(merke, model, new DateTime(baujahr, 1, 1)));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByMarkeAndModellAndBaujahrFilter")] // new
        public ActionResult<List<Car>> GetByMarkeAndModellAndBaujahrFilter([FromQuery] string? marke, [FromQuery] string? model, [FromQuery] int baujahr)
        {
            try
            {
                if((marke.IsEmpty() || marke == null) && (model.IsEmpty() || model == null)) return Ok(_readOnlycarService.GetByBauJahr(new DateTime(baujahr, 1, 1)));
                else if((marke.IsEmpty() || marke == null) && (baujahr == null || baujahr <= 0)) return Ok(_readOnlycarService.GetByModell(model));
                else if ((model.IsEmpty() || model == null) && (baujahr == null || baujahr <= 0)) return Ok(_readOnlycarService.GetByMarke(marke));
                else if (baujahr == null || baujahr <= 0) return Ok(_readOnlycarService.GetByMarkeAndModell(marke, model));
                else if ((marke.IsEmpty() || marke == null) && (baujahr == null || baujahr <= 0) && (model.IsEmpty() || model == null)) return Ok(_readOnlycarService.GetAll());
                else
                return Ok(_readOnlycarService.GetByMarkeAndModellAndBaujahr(marke, model, new DateTime(baujahr, 1, 1)));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Car> DeleteCar(int id)
        {
            try
            {
                _deletableCarService.Delete(_readOnlycarService.GetById(id));
                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Car is null")) { return BadRequest(e.Message); }

                return BadRequest();
            }
        }

        [HttpPost("")]
        [Produces("application/json")]
        public ActionResult<Car> AddCar(CarDTO carDTO)
        {
            try
            {
                Car car = new Car(carDTO);
                _addUpdateableCarService.Add(car);
                return Created("/api/Car/" + car.Id, car);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Car is null")) { return BadRequest(e.Message); }
                return BadRequest();
            }
        }

        [HttpPut("")]
        [Produces("application/json")]
        public ActionResult<Car> UpdateCar(CarDTO carDTO)
        {
            try
            {
                Car car = new Car(carDTO);
                _addUpdateableCarService.Update(car);
                return Ok(car);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //[HttpGet("test")]
        //public IActionResult GetAllEndpoints()
        //{
        //    var endpoints = _endpointDataSource.Endpoints;
        //    var routes = endpoints.Where(e => e.DisplayName.Contains("V2.CarController")); // e.DisplayName.Contains("V2") &&
            
        //    return Ok(routes);
        //}

        private IEnumerable<Endpoint> getRouteNames()
        {
            IEnumerable<Endpoint> endpoints = _endpointDataSource.Endpoints;
            EndpointMetadataCollection c = endpoints.ElementAt(0).Metadata;

            var routes = endpoints.Where(e => e.DisplayName.Contains("V2.CarController")); // e.DisplayName.Contains("V2") &&
            List<string> routeNames = new List<string>();
            //routeNames.Add(routes.ElementAtOrDefault(1).RoutePattern);

            return routes;
        }

        //private List<string> getRoutes(IEnumerable<Endpoint> routes)
        //{
        //    List<string> routeNames = new List<string>();
        //    foreach (var e in routes)
        //    {
        //        string routeName = e.RoutePattern.RawText;
        //    }
        //    return null;
        //}
        [HttpGet("endpoints")]
        public string ListAllEndpoints()
        {
            var endpoints = _endpointSources
                .SelectMany(es => es.Endpoints)
                .OfType<RouteEndpoint>();
            var output = endpoints.Select(
                e =>
                {
                    var controller = e.Metadata
                        .OfType<ControllerActionDescriptor>()
                        .FirstOrDefault();
                    var action = controller != null
                        ? $"{controller.ControllerName}.{controller.ActionName}"
                        : null;
                    //var controllerMethod = controller != null
                    //    ? $"{controller.ControllerTypeInfo.FullName}:{controller.MethodInfo.Name}"
                    //    : null;
                    return new
                    {
                        Method = e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods?[0],
                        Route = $"/{e.RoutePattern.RawText.TrimStart('/')}",
                        Action = action,
                        //ControllerMethod = controllerMethod
                    };
                }
            );
            var routen2 = endpoints
                .Where(e => e.DisplayName.Contains($"{apiVersion.Split(".")[0]}.{controllerName}"))
                .Select(e => e.RoutePattern);

            List<Microsoft.AspNetCore.Routing.Patterns.RoutePattern> routenForThisController = new List<Microsoft.AspNetCore.Routing.Patterns.RoutePattern>(); ;
            List<string> routenOutput = new List<string>();
            foreach (Microsoft.AspNetCore.Routing.Patterns.RoutePattern r in routen2)
            {
                routenOutput.Add(r.RawText.Replace("{version:apiVersion}", apiVersion.Split(".")[0]));
                routenForThisController.Add(r);
            }

            return JsonSerializer.Serialize(routenOutput);
        }

    }
}
