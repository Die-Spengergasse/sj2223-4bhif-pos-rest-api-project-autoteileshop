using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spg.AutoTeileShop.API.RequestBodyModels;
using Spg.AutoTeileShop.Domain.Models;
using Spg.AutoTeileShop.Infrastructure;


namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        //[HttpGet]                     // Darf nur 1x ohne Angabe von Parametern vorkommen!
        //public IEnumerable<string> GetMethod1()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //[HttpGet]                     // Darf nur 1x ohne Angabe von Parametern vorkommen!
        //public IActionResult GetMethod2()
        //{
        //    return Ok(new string[] { "value1", "value2" });
        //}
        private readonly AutoTeileShopContext _autoTeileShopContext;


       public CarController()
        {
            //_autoTeileShopContext = autoTeileShopContext;

            ////AutoTeileShopContext db = new AutoTeileShopContext(builder);
            //_autoTeileShopContext.Database.EnsureDeleted();
            //_autoTeileShopContext.Database.EnsureCreated();
            //_autoTeileShopContext.Seed();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string[]> GetMethod3(int id)
        {
            Car car = _autoTeileShopContext.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null) { return NotFound(); }
            CarDTO carReqeustBodyModel = new CarDTO(car);
            return Ok(carReqeustBodyModel);
        }

        // *************************************************************************************************

        //[HttpGet]                     // Darf nur 1x ohne Angabe von Parametern vorkommen!
        //public async Task<IEnumerable<string>> GetMethod1Async()
        //{
        //    return await Task.FromResult(new string[] { "value1", "value2" });
        //}

        //[HttpGet]                     // Darf nur 1x ohne Angabe von Parametern vorkommen!
        //public async Task<IActionResult> GetMethod2Async()
        //{
        //    // await something
        //    var result = await ....
        //    return Ok(result);
        //}

        //[HttpGet]                     // Darf nur 1x ohne Angabe von Parametern vorkommen!
        //public async Task<ActionResult<string[]>> GetMethod3Async()
        //{
        //    return await Task.FromResult(Ok(new string[] { "value3", "value4" }));
        //}
    }
}
