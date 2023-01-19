using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Domain.DTO;
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
                return BadRequest();
            }
        }

        [HttpGet("/name/{name}")] // no query form because it returns only one catagory
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
                return BadRequest();
            }
        }

        [HttpGet("/{id}/Description")]
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
                return BadRequest();
            }
        }

        [HttpGet("/filter")] //in this fromat it donst shine of in the Swagger interface
        public ActionResult<List<Catagory>> GetByTypeOrTopCatagory([FromQuery] CategoryTypes? categoryType, [FromQuery] Catagory? topCatagory)
        {
            if (categoryType != null)
            {
                try
                {
                    List<Catagory> catagorys = (List<Catagory>)_readOnlyCatagoryService.GetCatagoriesByType((CategoryTypes)categoryType);
                    if (catagorys.Count == 0)
                        return NotFound($"No Catagorys with Type: {categoryType} found");
                    return Ok(catagorys);

                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            else if (topCatagory != null)
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
                    return BadRequest();
                }
            }

            else if (topCatagory != null && categoryType != null)
            {
                try
                {
                    List<Catagory> catagorys = (List<Catagory>)_readOnlyCatagoryService.GetCatagoriesByTopCatagoryandByType(topCatagory, (CategoryTypes)categoryType);
                    if (catagorys.Count == 0)
                        return NotFound($"No Catagorys with TopCatagory: {topCatagory} found");
                    return Ok(catagorys);

                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
                
        }

      

        [HttpPost("")]
        [Produces("application/json")]
        public ActionResult<Catagory> AddCatagory(CatagoryPostDTO catagoryDTO)
        {
            try
            {
                Catagory c = new Catagory(catagoryDTO, _readOnlyCatagoryService.GetCatagoryById(catagoryDTO.TopCatagoryId));
                    _addUpdateableCatagoryService.AddCatagory(c);
                return Created("/api/Catagory/" + c.Id, c);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("/{Id}")]
        [Produces("application/json")]
        public ActionResult<Catagory> UpdateCatagory(int Id,Catagory catagory)
        {
            try
            {
                return Ok(_addUpdateableCatagoryService.UpdateCatagory(Id ,catagory));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("/{id}")]
        public ActionResult DeleteCatagory(int id)
        {
            Catagory catagory = null;
            try
            {
                catagory = _readOnlyCatagoryService.GetCatagoryById(id);
                _deletableCatagoryService.DeleteCatagory(catagory);
                return Ok(catagory);
            }
            catch (Exception e)
            {
                if (e.Message.Contains($"Catagory with Id: {id} not found")) return Ok(catagory);
                return BadRequest();
            }
        }
    }
}
