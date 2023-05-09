using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;
using System.Runtime.CompilerServices;
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


        public CarController(IReadOnlyCarService readOnlycarService, IDeletableCarService deletableCarService, IAddUpdateableCarService addUpdateableCarService, EndpointDataSource endpointDataSource)
        {
            _readOnlycarService = readOnlycarService;
            _deletableCarService = deletableCarService;
            _addUpdateableCarService = addUpdateableCarService;
            _endpointDataSource = endpointDataSource;

            getRouteNames();
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
            var endpoints = _endpointDataSource.Endpoints;
            var routes = endpoints.Where(e => e.DisplayName.Contains("V2.CarController")); // e.DisplayName.Contains("V2") &&

            return routes;
        }
    }
}
