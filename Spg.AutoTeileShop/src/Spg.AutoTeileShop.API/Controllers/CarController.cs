using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;


namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly AutoTeileShopContext _autoTeileShopContext;


        public CarController(AutoTeileShopContext autoTeileShopContext)
        {
            _autoTeileShopContext = autoTeileShopContext;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string[]> CarbyId(int id)
        {
            Car car = _autoTeileShopContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null) { return NotFound(); }
            CarDTO carReqeustBodyModel = new CarDTO(car);
            return Ok(carReqeustBodyModel);
        }


    }
}
