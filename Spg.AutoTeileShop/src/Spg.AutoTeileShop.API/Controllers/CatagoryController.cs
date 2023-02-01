﻿using Microsoft.AspNetCore.Authorization;
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

        // Add Catagory - Authorization
        // Delete Catagory - Authorization
        // Update Catagory - Authorization

        [HttpGet("")]
        [AllowAnonymous]
        public ActionResult<List<Catagory>> GetAllCatagorys()
        {
            return Ok(_readOnlyCatagoryService.GetAllCatagories());
        }

        [HttpGet("/{id}")]
        [AllowAnonymous]
        public ActionResult<Catagory> GetCatagoryById(int id)
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
        [AllowAnonymous]
        public ActionResult<Catagory> GetCatagoryByName(string name)
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
        [AllowAnonymous]
        public ActionResult<string> GetCatagoryDescriptionById(int id)
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
        [AllowAnonymous]
        public ActionResult<List<Catagory>> GetCatagoryByTypeOrTopCatagory([FromQuery] CategoryTypes? categoryType, [FromQuery] int topCatagoryId)
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
            else if (topCatagoryId != 0)
            {
                try
                {
                    List<Catagory> catagorys = (List<Catagory>)_readOnlyCatagoryService.GetCatagoriesByTopCatagory(_readOnlyCatagoryService.GetCatagoryById(topCatagoryId));
                    if (catagorys.Count == 0)
                        return NotFound($"No Catagorys with TopCatagory: {topCatagoryId} found");
                    return Ok(catagorys);

                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            else if (topCatagoryId != 0 && categoryType != null)
            {
                try
                {
                    List<Catagory> catagorys = (List<Catagory>)_readOnlyCatagoryService.GetCatagoriesByTopCatagoryandByType(_readOnlyCatagoryService.GetCatagoryById(topCatagoryId), (CategoryTypes)categoryType);
                    if (catagorys.Count == 0)
                        return NotFound($"No Catagorys with TopCatagory: {topCatagoryId} found");
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
