﻿using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Infrastructure;

namespace Spg.AutoTeileShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class SeedController : ControllerBase
    {
        private AutoTeileShopContext _context;

        public SeedController(AutoTeileShopContext context)
        {
            _context = context;
        }

        [HttpGet("seed")]
        public IActionResult Seed()
        {
            //_context.Database.Migrate();
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.Seed();
            _context.SaveChanges();
            return Ok();
        }

        //[HttpGet()]
        //public IActionResult Get()
        //{
        //    try 
        //    {
        //        _context.Cars.Add(null);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex);
        //    }
        //    return Ok(_context.Database.GetDbConnection().ConnectionString);
        //}
    }
}