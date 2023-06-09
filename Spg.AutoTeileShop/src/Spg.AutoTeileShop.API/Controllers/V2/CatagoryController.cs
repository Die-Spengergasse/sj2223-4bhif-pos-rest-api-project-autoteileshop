using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Application.Helper;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Helper;
using Spg.AutoTeileShop.Domain.Interfaces.Catagory_Interfaces;
using Spg.AutoTeileShop.Domain.Models;

namespace Spg.AutoTeileShop.API.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CatagoryController : ControllerBase
    {
        private readonly IDeletableCatagoryService _deletableCatagoryService;
        private readonly IAddUpdateableCatagoryService _addUpdateableCatagoryService;
        private readonly IReadOnlyCatagoryService _readOnlyCatagoryService;
        //Hateaos
        private readonly IEnumerable<EndpointDataSource> _endpointSources;
        private List<BuildRoutePattern> _routes;


        public CatagoryController
            (IDeletableCatagoryService deletableCatagoryService, IAddUpdateableCatagoryService addUpdateableCatagoryService,
            IReadOnlyCatagoryService readOnlyCatagoryService, IEnumerable<EndpointDataSource> endpointSources, ListAllEndpoints listAllEndpoints)
        {
            _deletableCatagoryService = deletableCatagoryService;
            _addUpdateableCatagoryService = addUpdateableCatagoryService;
            _readOnlyCatagoryService = readOnlyCatagoryService;
            //Hateaos
            _endpointSources = endpointSources;

            var apiVersionAttribute = (ApiVersionAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ApiVersionAttribute));

            _routes = listAllEndpoints.ListAllEndpointsAndMethodes(GetType().Name, apiVersionAttribute?.Versions.FirstOrDefault()?.ToString(), this._endpointSources);


        }

        [HttpGet("")]
        public ActionResult<List<Catagory>> GetAll()
        {
            var result = _readOnlyCatagoryService.GetAllCatagories();
            HateoasBuild<Catagory, int> hb = new HateoasBuild<Catagory, int>();
            
            return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Catagory> GetCatagoryById(int id)
        {
            try
            {
                var result = _readOnlyCatagoryService.GetCatagoryById(id);
                HateoasBuild<Catagory, int> hb = new HateoasBuild<Catagory, int>();

                return Ok(hb.buildHateoas(result, result.Id, _routes));
            }
            catch (Exception e)
            {
                if (e.Message.Contains($"Catagory with Id: {id} not found"))
                    return NotFound(e.Message);
                return BadRequest();
            }
        }

        [HttpGet("name/{name}")] // no query form because it returns only one catagory
        [AllowAnonymous]
        public ActionResult<Catagory> GetCatagoryByName(string name)
        {
            try
            {
                var result = _readOnlyCatagoryService.GetCatagoryByName(name);
                HateoasBuild<Catagory, int> hb = new HateoasBuild<Catagory, int>();

                return Ok(hb.buildHateoas(result, result.Id, _routes));
            }
            catch (Exception e)
            {
                if (e.Message.Contains($"Catagory with Name: {name} not found"))
                    return NotFound(e.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}/Description")]
        [AllowAnonymous]
        public ActionResult<string> GetCatagoryDescriptionById(int id)
        {
            try
            {
                var result = _readOnlyCatagoryService.GetCatagoryDescriptionById(id);
                
                return Ok(_readOnlyCatagoryService.GetCatagoryDescriptionById(id));
            }
            catch (Exception e)
            {
                if (e.Message.Contains($"Catagory with Id: {id} not found"))
                    return NotFound(e.Message);
                return BadRequest();
            }
        }

        [HttpGet("filter")] //in this fromat it does not exist in the Swagger interface
        [AllowAnonymous]
        public ActionResult<List<Catagory>> GetCatagoryByTypeOrTopCatagory([FromQuery] CategoryTypes? categoryType, [FromQuery] int topCatagoryId)
        {
            if (categoryType != null)
            {
                var result = _readOnlyCatagoryService.GetCatagoriesByType((CategoryTypes)categoryType);
                HateoasBuild<Catagory, int> hb = new HateoasBuild<Catagory, int>();

                try
                {
                    List<Catagory> catagorys = (List<Catagory>)_readOnlyCatagoryService.GetCatagoriesByType((CategoryTypes)categoryType);
                    if (catagorys.Count == 0)
                        return NotFound($"No Catagorys with Type: {categoryType} found");
                    return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));

                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }
            else if (topCatagoryId != 0)
            {
                var result = _readOnlyCatagoryService.GetCatagoriesByTopCatagory(_readOnlyCatagoryService.GetCatagoryById(topCatagoryId));
                    HateoasBuild<Catagory, int> hb = new HateoasBuild<Catagory, int>();

                try
                {
                    

                    List<Catagory> catagorys = (List<Catagory>)_readOnlyCatagoryService.GetCatagoriesByTopCatagory(_readOnlyCatagoryService.GetCatagoryById(topCatagoryId));
                    if (catagorys.Count == 0)
                        return NotFound($"No Catagorys with TopCatagory: {topCatagoryId} found");
                    return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));

                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            else if (topCatagoryId != 0 && categoryType != null)
            {
                var result = _readOnlyCatagoryService.GetCatagoriesByTopCatagory(_readOnlyCatagoryService.GetCatagoryById(topCatagoryId));
                HateoasBuild<Catagory, int> hb = new HateoasBuild<Catagory, int>();

                try
                {
                    List<Catagory> catagorys = (List<Catagory>)_readOnlyCatagoryService.GetCatagoriesByTopCatagoryandByType(_readOnlyCatagoryService.GetCatagoryById(topCatagoryId), (CategoryTypes)categoryType);
                    if (catagorys.Count == 0)
                        return NotFound($"No Catagorys with TopCatagory: {topCatagoryId} found");
                    return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));

                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            else if (topCatagoryId == null && categoryType == null)
            {
                var result = _readOnlyCatagoryService.GetAllCatagories();
                HateoasBuild<Catagory, int> hb = new HateoasBuild<Catagory, int>();

                return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));
            }
            return BadRequest("No Query Parameters given");

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

        [HttpPut("{Id}")]
        [Produces("application/json")]
        public ActionResult<Catagory> UpdateCatagory(int Id, Catagory catagory)
        {
            try
            {
                var result = _addUpdateableCatagoryService.UpdateCatagory(Id, catagory);
                HateoasBuild<Catagory, int> hb = new HateoasBuild<Catagory, int>();

                return Ok(_addUpdateableCatagoryService.UpdateCatagory(Id, catagory));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
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
