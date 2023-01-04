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


        public ActionResult<List<Car>> GetByModell(string model)
        public ActionResult<List<Car>> GetByMarkeAndModellAndBaujahr(string merke, string model, int baujahr)
    }
}
