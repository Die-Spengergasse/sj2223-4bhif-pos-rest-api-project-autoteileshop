using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;

using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Helper;
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
        private List<BuildRoutePattern> _routes;


        public CarController
            (IReadOnlyCarService readOnlycarService, IDeletableCarService deletableCarService,
            IAddUpdateableCarService addUpdateableCarService, EndpointDataSource endpointDataSource,
            IEnumerable<EndpointDataSource> endpointSources, ListAllEndpoints listAllEndpoints )
        {
            _readOnlycarService = readOnlycarService;
            _deletableCarService = deletableCarService;
            _addUpdateableCarService = addUpdateableCarService;
            _endpointDataSource = endpointDataSource;
            _endpointSources = endpointSources;

            //requert for HATEOAS, List of Routes and Methodes
            var apiVersionAttribute = (ApiVersionAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ApiVersionAttribute));
            _routes = listAllEndpoints.ListAllEndpointsAndMethodes(GetType().Name, apiVersionAttribute?.Versions.FirstOrDefault()?.ToString(), this._endpointSources);

        }


        // AddCar - Authorization
        // DeleteCar - Authorization
        // UpdateCar - Authorization

        [HttpGet("Old")]
        public ActionResult<List<Car>> GetAllCars() // Auslaufend
        {
            var cars = _readOnlycarService.GetAll();
            HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();
            
            return Ok(hb.buildHateoas(cars.ToList(), cars.Select(c => c.Id).ToList(), _routes));
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

        [HttpGet("")] // new
        public ActionResult<List<Car>> GetByMarkeAndModellAndBaujahrFilter([FromQuery] string? marke, [FromQuery] string? model, [FromQuery] string? baujahrS)
        {
            try
            {
                int baujahr = -1;
                if (baujahrS is not null) baujahr = int.Parse(baujahrS);
                if((marke.IsEmpty() || marke == null) && (model.IsEmpty() || model == null) && baujahr != -1) return Ok(_readOnlycarService.GetByBauJahr(new DateTime(baujahr, 1, 1)));
                else if((marke.IsEmpty() || marke == null) && (baujahr == -1|| baujahr <= 0) && model is not null) return Ok(_readOnlycarService.GetByModell(model));
                else if ((model.IsEmpty() || model == null) && (baujahr == -1|| baujahr <= 0) && marke is not null) return Ok(_readOnlycarService.GetByMarke(marke));
                else if ((baujahr == -1|| baujahr <= 0) &&  marke is not null && model is not null) return Ok(_readOnlycarService.GetByMarkeAndModell(marke, model));
                else if ((marke.IsEmpty() || marke == null) && (baujahr == -1|| baujahr <= 0) && (model.IsEmpty() || model == null)) return Ok(_readOnlycarService.GetAll());
                else
                return Ok(_readOnlycarService.GetByMarkeAndModellAndBaujahr(marke, model, new DateTime(baujahr, 1, 1)));
            }
            catch (Exception e)
            {
                return BadRequest(e);
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
        public ActionResult<Car> AddCar([FromQuery] CarDTOPost carDTO)
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

        [HttpPut("{id}")]
        [Produces("application/json")]
        public ActionResult<Car> UpdateCar(int id, [FromQuery] CarDTOUpdate carDTO)
        {
            if (id <= 0) return BadRequest("Id must be greater than 0");
            if (_readOnlycarService.GetById(id) == null) return NotFound("Car not found");
            try
            {
                Car car = new Car(id, carDTO);
                _addUpdateableCarService.Update(car);
                return Ok(car);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }

}
