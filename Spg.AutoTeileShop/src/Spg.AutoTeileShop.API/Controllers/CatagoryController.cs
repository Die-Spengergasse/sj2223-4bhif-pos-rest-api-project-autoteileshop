using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private readonly IDeletableCatagoryService _deletableCatagoryService;
        private readonly IAddUpdateableCatagoryService _addUpdateableCatagoryService;
        private readonly IReadOnlyCatagoryService _readOnlyCatagoryService;

        public CatagoryController(IDeletableCatagoryService deletableCatagoryService, IAddUpdateableCatagoryService addUpdateableCatagoryService, IReadOnlyCatagoryService readOnlyCatagoryService)
        {
            _deletableCatagoryService = deletableCatagoryService;
            _addUpdateableCatagoryService = addUpdateableCatagoryService;
            _readOnlyCatagoryService = readOnlyCatagoryService;
        }

        [HttpGet("")]
        public ActionResult<List<Catagory>> GetAll()
        {
            return Ok(_readOnlyCatagoryService.GetAllCatagories());
        }

        [HttpGet("/{id}")]
        public ActionResult<Catagory> GetById(int id)
        {
            try
            {
                return Ok(_readOnlyCatagoryService.GetCatagoryById(id));
            }
            catch (Exception e)
            {
                if (e.Message.Contains($"Catagory with Id: {id} not found"))
                    return NotFound(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/{name}")]
        public ActionResult<Catagory> GetByName(string name)
        {
            try
            {
                return Ok(_readOnlyCatagoryService.GetCatagoryByName(name));
            }
            catch (Exception e)
            {
                if (e.Message.Contains($"Catagory with Name: {name} not found"))
                    return NotFound(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/Description/{id}")]
        public ActionResult<string> GetDescriptionById(int id)
        {
            try
            {
                return Ok(_readOnlyCatagoryService.GetCatagoryDescriptionById(id));
            }
            catch (Exception e)
            {
                if (e.Message.Contains($"Catagory with Id: {id} not found"))
                    return NotFound(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/ByType/{categoryType}")]
        public ActionResult<List<Catagory>> GetByType(CategoryTypes categoryType)
        {
            try
            {
                List<Catagory> catagorys = (List<Catagory>)_readOnlyCatagoryService.GetCatagoriesByType(categoryType);
                if (catagorys.Count == 0)
                    return NotFound($"No Catagorys with Type: {categoryType} found");
                return Ok(catagorys);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/ByTopCatagory/{topCatagory}")]
        public ActionResult<List<Catagory>> GetByTopCatagory(Catagory topCatagory)
        {
            try
            {
                List<Catagory> catagorys = (List<Catagory>)_readOnlyCatagoryService.GetCatagoriesByTopCatagory(topCatagory);
                if (catagorys.Count == 0)
                    return NotFound($"No Catagorys with TopCatagory: {topCatagory} found");
                return Ok(catagorys);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost()]
        [Produces("application/json")]
        public ActionResult<Catagory> AddCatagory(Catagory catagory)
        {
            try
            {
                Catagory c = _addUpdateableCatagoryService.AddCatagory(catagory);
                return Created("/api/Catagory/" + c.Id, c);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut()]
        [Produces("application/json")]
        public ActionResult<Catagory> UpdateCatagory(Catagory catagory)
        {
            try
            {
                return Ok(_addUpdateableCatagoryService.UpdateCatagory(catagory));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("/{id}")]
        public ActionResult DeleteCatagory(int id)
        {
            try
            {
                Catagory catagory = _readOnlyCatagoryService.GetCatagoryById(id);
                _deletableCatagoryService.DeleteCatagory(catagory);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
