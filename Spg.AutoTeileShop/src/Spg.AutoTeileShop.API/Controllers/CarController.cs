using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private AutoTeileShopContext createDB()  // You have to run the 2 Unit Test classes separately (successively) otherwise the database accesses get in the way
        {
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=AutoTeileShopTest.db")
                .Options;

            AutoTeileShopContext db = new AutoTeileShopContext(options);
            return db;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<string[]> GetMethod3(int id)
        {
            AutoTeileShopContext db = createDB();
            Car car = db.Cars.SingleOrDefault(c => c.Id == id);
            if (car == null) { return NotFound(); }
            return Ok(car);
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

        [HttpGet]                     // Darf nur 1x ohne Angabe von Parametern vorkommen!
        public async Task<ActionResult<string[]>> GetMethod3Async()
        {
            return await Task.FromResult(Ok(new string[] { "value3", "value4" }));
        }
    }
}
