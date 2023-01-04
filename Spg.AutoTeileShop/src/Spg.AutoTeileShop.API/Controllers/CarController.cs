using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Application.Services;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;


namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase    {

        private readonly IReadOnlyCarService _readOnlycarService;
        private readonly IDeletableCarService _deletableCarService;
        private readonly IAddUpdateableCarService _addUpdateableCarService;

        public CarController(IReadOnlyCarService readOnlycarService, IDeletableCarService deletableCarService, IAddUpdateableCarService addUpdateableCarService)
        {
            _readOnlycarService = readOnlycarService;
            _deletableCarService = deletableCarService;
            _addUpdateableCarService = addUpdateableCarService;
        }

        [HttpGet("")]
        public ActionResult<List<Car>> GetAll()
        {
            return Ok(_readOnlycarService.GetAll());
        }

               
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetByBaujahr/{year}")]
        public ActionResult<List<Car>> GetByBaujahr(int year)
        {
            try
            {
                return Ok(_readOnlycarService.GetByBauJahr(new DateTime(year, 1, 1)));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetByMarke/{marke}")]
        public ActionResult<List<Car>> GetByMarke(string marke)
        {
            try
            {
                return Ok(_readOnlycarService.GetByMarke(marke));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetByModell/{model}")]
        public ActionResult<List<Car>> GetByModell(string model)
        {
            try
            {
                return Ok(_readOnlycarService.GetByModell(model));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetByMarkeAndModell/{marke}/{model}")]
        public ActionResult<List<Car>> GetByMarkeAndModell(string marke, string model)
        {
            try
            {
                return Ok(_readOnlycarService.GetByMarkeAndModell(marke, model));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetByMarkeAndModellAndBaujahr/{merke}/{model}/{baujahr}")]
        public ActionResult<List<Car>> GetByMarkeAndModellAndBaujahr(string merke, string model, int baujahr)
        {
            try
            {
                return Ok(_readOnlycarService.GetByMarkeAndModellAndBaujahr(merke, model, new DateTime(baujahr, 1, 1)));
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
                return BadRequest(e);
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
                return Created("/api/Car/" + car.Id , car);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut()]
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
                return BadRequest(e);
            }
        }
    }
}
