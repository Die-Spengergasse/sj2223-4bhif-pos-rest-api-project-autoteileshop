using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;

using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Helper;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System.Linq;
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

        //requert for HATEOAS, List of Routes and Methodes
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

            //requert for HATEOAS, List of Routes and Methodes
            var apiVersionAttribute = (ApiVersionAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ApiVersionAttribute));
            this.apiVersion = apiVersionAttribute?.Versions.FirstOrDefault()?.ToString();

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

                    return new
                    {
                        Method = e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods?[0],
                        Route = $"/{e.RoutePattern.RawText.TrimStart('/')}",
                        Action = action,
                    };
                }
            );
            var routen2 = endpoints
                .Where(e => e.DisplayName.Contains($"{apiVersion.Split(".")[0]}.{controllerName}"))
                .Select(e => e.RoutePattern);
            
            List<BuildRoutePattern> buildRoutePatterns = new List<BuildRoutePattern>();
            foreach (Microsoft.AspNetCore.Routing.Patterns.RoutePattern r in routen2)
            {
                Microsoft.AspNetCore.Routing.Patterns.RoutePattern routePattern = r;

                var methode = output.Where(a => a.Route.Equals("/" + r.RawText)).Select(o => o.Method);

                //Filter if more than 1 Methode
                if (methode.Count() == 2)
                {
                    BuildRoutePattern brP = new(r.RawText.Replace("{version:apiVersion}", apiVersion.Split(".")[0]), methode.FirstOrDefault());
                    if(!buildRoutePatterns.Contains(brP)) buildRoutePatterns.Add(brP);
                }
                else
                {
                    List<string> methodeHalved = new();
                    for (int i = 0; i <= (methode.Count() / 2) - 1; i++)
                    {
                        BuildRoutePattern brP = new(r.RawText.Replace("{version:apiVersion}", apiVersion.Split(".")[0]), methode.ElementAtOrDefault(i));
                        if (!buildRoutePatterns.Contains(brP)) buildRoutePatterns.Add(brP);
                    }
                }                    
            }

            return JsonSerializer.Serialize(buildRoutePatterns);
        }

    


    }
}
