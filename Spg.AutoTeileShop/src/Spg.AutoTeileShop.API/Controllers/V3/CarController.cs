using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.AutoTeileShop.Application.Helper;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Commands;
using Spg.AutoTeileShop.Application.Services.CQS.Car.Queries;
using Spg.AutoTeileShop.Domain;
using Spg.AutoTeileShop.Domain.DTO;
using Spg.AutoTeileShop.Domain.Helper;
using Spg.AutoTeileShop.Domain.Interfaces;
using Spg.AutoTeileShop.Domain.Interfaces.Car_Interfaces;
using Spg.AutoTeileShop.Domain.Models;
using System.Web.WebPages;

namespace Spg.AutoTeileShop.API.Controllers.V3
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class CarController : ControllerBase
    {


        //requert for HATEOAS, List of Routes and Methodes

        private readonly IEnumerable<EndpointDataSource> _endpointSources;

        private List<BuildRoutePattern> _routes;

        // CQS
        private readonly IMediator _mediator;
        private readonly IQueryHandler<GetCarByIdQuery, Car> _getCarByIdQueryHandler;
        private readonly ICommandHandler<CreateCarCommand, Car> _createCarCommandHandler;
        private readonly IQueryHandler<GetAllCarsQuery, IQueryable<Car>> _getAllCarsQueryHandler;
        private readonly IQueryHandler<GetCarsByBaujahrQuery, IEnumerable<Car>> _getCarsByBaujahrQueryHandler;
        private readonly IQueryHandler<GetCarsByMarkeQuery, IEnumerable<Car>> _getCarsByMarkeQueryHandler;
        private readonly IQueryHandler<GetCarsByModellQuery, IEnumerable<Car>> _getCarsByModellQueryHandler;
        private readonly IQueryHandler<GetCarsByMarkeAndModellQuery, IEnumerable<Car>> _getCarsByMarkeAndModellQueryHandler;
        private readonly IQueryHandler<GetCarsByMarkeAndModellAndBaujahrQuery, IEnumerable<Car>> _getCarsByMarkeAndModellAndBaujahrQueryHandler;
        private readonly IQueryHandler<GetCarsByFitProductQuery, IEnumerable<Car>> _getCarsByFitProductQueryHandler;

        // AutoMapper
        private readonly IMapper _mapper;

        public CarController
            (
            IReadOnlyCarService readOnlycarService, IDeletableCarService deletableCarService,
            IAddUpdateableCarService addUpdateableCarService,
            IEnumerable<EndpointDataSource> endpointSources, ListAllEndpoints listAllEndpoints,
            IMediator mediator,
            IQueryHandler<GetAllCarsQuery, IQueryable<Car>> getAllCarsQueryHandler,
            IQueryHandler<GetCarByIdQuery, Car> getCarByIdQueryHandler,
            ICommandHandler<CreateCarCommand, Car> createCarCommandHandler,
            IQueryHandler<GetCarsByBaujahrQuery, IEnumerable<Car>> getCarsByBaujahrQueryHandler,
            IQueryHandler<GetCarsByMarkeQuery, IEnumerable<Car>> getCarsByMarkeQueryHandler,
            IQueryHandler<GetCarsByModellQuery, IEnumerable<Car>> getCarsByModellQueryHandler,
            IQueryHandler<GetCarsByMarkeAndModellQuery, IEnumerable<Car>> getCarsByMarkeAndModellQueryHandler,
            IQueryHandler<GetCarsByMarkeAndModellAndBaujahrQuery, IEnumerable<Car>> getCarsByMarkeAndModellAndBaujahrQueryHandler,
            IQueryHandler<GetCarsByFitProductQuery, IEnumerable<Car>> getCarsByFitProductQueryHandler,
            IMapper mapper
            )
        {

            _endpointSources = endpointSources;

            //requert for HATEOAS, List of Routes and Methodes
            var apiVersionAttribute = (ApiVersionAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ApiVersionAttribute));

            _routes = listAllEndpoints.ListAllEndpointsAndMethodes(GetType().Name, apiVersionAttribute?.Versions.FirstOrDefault()?.ToString(), this._endpointSources);

            //CQS
            _mediator = mediator;
            _getAllCarsQueryHandler = getAllCarsQueryHandler;
            _getCarByIdQueryHandler = getCarByIdQueryHandler;
            _createCarCommandHandler = createCarCommandHandler;
            _getCarsByBaujahrQueryHandler = getCarsByBaujahrQueryHandler;
            _getCarsByMarkeQueryHandler = getCarsByMarkeQueryHandler;
            _getCarsByModellQueryHandler = getCarsByModellQueryHandler;
            _getCarsByMarkeAndModellQueryHandler = getCarsByMarkeAndModellQueryHandler;
            _getCarsByMarkeAndModellAndBaujahrQueryHandler = getCarsByMarkeAndModellAndBaujahrQueryHandler;
            _getCarsByFitProductQueryHandler = getCarsByFitProductQueryHandler;
            //AutoMapper
            _mapper = mapper;
        }


        [HttpGet("Old")]
        public ActionResult<List<Car>> GetAllCarsAsync() // Auslaufend
        {
            GetAllCarsQuery query = new GetAllCarsQuery
            {
                SortBy = c => c.Marke

            };

            var cars = _mediator.QueryAsync<GetAllCarsQuery, IQueryable<Car>>(query).Result;

            HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();

            return Ok(hb.buildHateoas(cars.ToList(), cars.Select(c => c.Id).ToList(), _routes));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<Car> GetCarbyId(int id)
        {
            try
            {
                var query = new GetCarByIdQuery(id);
                Car? car = _mediator.QueryAsync<GetCarByIdQuery, Car>(query).Result;
                if (car == null)
                {
                    return NotFound();
                }
                HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();
                return Ok(hb.buildHateoas(car, car.Id, _routes));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains($"No Car found with Id: {id}")) { return BadRequest(ex.Message); }
                return BadRequest(ex);
            }
        }

        [HttpGet("byBaujahr")]
        [AllowAnonymous]
        public ActionResult<List<Car>> GetByBaujahr([FromQuery] int year)
        {
            try
            {
                //GetAllCarsQuery query = new GetAllCarsQuery //-- Newer Version
                //{
                //    Filter = c => c.Baujahr.Year == year
                //};

                GetCarsByBaujahrQuery query = new GetCarsByBaujahrQuery(new DateTime(year, 1, 1));

                var result = _mediator.QueryAsync<GetCarsByBaujahrQuery, IQueryable<Car>>(query).Result;

                //var result = _readOnlycarService.GetByBauJahr(new DateTime(year, 1, 1));
                HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();

                return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByMarke")]
        [AllowAnonymous]
        public ActionResult<List<Car>> GetByMarke([FromQuery] string marke)  // Auslaufend
        {
            try
            {
                //GetAllCarsQuery query = new GetAllCarsQuery // Old Version
                //{
                //    Filter = c => c.Marke == marke
                //};

                GetCarsByMarkeQuery query = new GetCarsByMarkeQuery(marke);

                var result = _mediator.QueryAsync<GetCarsByMarkeQuery, IQueryable<Car>>(query).Result;

                HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();

                return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByModel")]
        [AllowAnonymous]
        public ActionResult<List<Car>> GetByModell([FromQuery] string model)
        {
            try
            {
                //GetAllCarsQuery query = new GetAllCarsQuery // Old Version
                //{
                //    Filter = c => c.Modell == model
                //};

                GetCarsByMarkeQuery query = new GetCarsByMarkeQuery(model);


                var result = _mediator.QueryAsync<GetCarsByMarkeQuery, IQueryable<Car>>(query).Result; HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();

                return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByMarkeAndModell")]
        [AllowAnonymous]
        public ActionResult<List<Car>> GetByMarkeAndModell([FromQuery] string? marke, [FromQuery] string? model)
        {
            try
            {
                //GetAllCarsQuery query = new GetAllCarsQuery // Old Version
                //{
                //    Filter = c => c.Marke.Equals(marke) && c.Modell.Equals(model)
                //};

                GetCarsByMarkeAndModellQuery query = new GetCarsByMarkeAndModellQuery(marke, model);

                var result = _mediator.QueryAsync<GetCarsByMarkeAndModellQuery, IQueryable<Car>>(query).Result;
                HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();

                return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("ByMarkeAndModellAndBaujahr")]
        [AllowAnonymous]
        public ActionResult<List<Car>> GetByMarkeAndModellAndBaujahr([FromQuery] string marke, [FromQuery] string model, [FromQuery] int baujahr)
        {
            try
            {
                //GetAllCarsQuery query = new GetAllCarsQuery // Old Version
                //{
                //    Filter = c => c.Marke == marke
                //};
                GetCarsByMarkeAndModellAndBaujahrQuery query = new GetCarsByMarkeAndModellAndBaujahrQuery(marke, model, new DateTime(baujahr, 1, 1));

                var result = _mediator.QueryAsync<GetCarsByMarkeAndModellAndBaujahrQuery, IQueryable<Car>>(query).Result;
                HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();

                return Ok(hb.buildHateoas(result.ToList(), result.Select(s => s.Id).ToList(), _routes));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("")] // new
        public ActionResult<List<Car>> GetByMarkeAndModellAndBaujahrFilter([FromQuery] string? marke, [FromQuery] string? model, [FromQuery] string? baujahrS)
        {
            HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();
            IEnumerable<Car> cars = new List<Car>();
            try
            {
                int baujahr = -1;
                if (baujahrS is not null) baujahr = int.Parse(baujahrS);
                GetAllCarsQuery query = new GetAllCarsQuery();

                if ((marke.IsEmpty() || marke == null) && (model.IsEmpty() || model == null) && baujahr != -1)
                {
                    query.Filter = c => c.Baujahr.Year == baujahr;
                }
                else if ((marke.IsEmpty() || marke == null) && (baujahr == -1 || baujahr <= 0) && model is not null)
                {
                    query.Filter = c => c.Modell.Equals(model);
                }
                else if ((model.IsEmpty() || model == null) && (baujahr == -1 || baujahr <= 0) && marke is not null)
                {
                    query.Filter = c => c.Marke.Equals(marke);
                }
                else if ((baujahr == -1 || baujahr <= 0) && marke is not null && model is not null)
                {
                    query.Filter = c => c.Marke.Equals(marke) && c.Modell.Equals(model);
                }
                else if ((marke.IsEmpty() || marke == null) && (baujahr == -1 || baujahr <= 0) && (model.IsEmpty() || model == null))
                {
                    // Kein Filter erforderlich
                }
                else
                {
                    query.Filter = c => c.Marke.Equals(marke) && c.Modell.Equals(model) && c.Baujahr.Year == baujahr;
                }

                cars = _mediator.QueryAsync<GetAllCarsQuery, List<Car>>(query).Result;




                return Ok(hb.buildHateoas(cars.ToList(), cars.Select(s => s.Id).ToList(), _routes.ToList()));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "SalesmanOrAdmin")]
        public ActionResult<Car> DeleteCar(int id)
        {
            try
            {

                GetCarByIdQuery query = new GetCarByIdQuery(id);
                DeleteCarCommand command = new DeleteCarCommand(_mediator.QueryAsync<GetCarByIdQuery, Car>(query).Result);
                return Ok(_mediator.ExecuteAsync<DeleteCarCommand, Car>(command).Result);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Car is null")) { return BadRequest(e.Message); }

                return BadRequest();
            }
        }

        [HttpPost("")]
        public ActionResult<Car> AddCar([FromBody] CarDTOPost carDTO)
        {
            try
            {   if (carDTO is null) return BadRequest("CarDto is null ");
                

                Car car = _mapper.Map<Car>(carDTO);
                CreateCarCommand command = new CreateCarCommand(car);
                var result = _mediator.ExecuteAsync<CreateCarCommand, Car>(command);

                HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();

                return Ok(hb.buildHateoas(result.Result, result.Result.Id, _routes));
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Car is null")) { return BadRequest(e.Message); }
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Car> UpdateCar(int id, [FromQuery] CarDTOUpdate carDTO)
        {
            if (id <= 0) return BadRequest("Id must be greater than 0");

            GetCarByIdQuery query = new GetCarByIdQuery(id);
            if (_mediator.QueryAsync<GetCarByIdQuery, Car>(query).Result == null) return NotFound("Car not found");
            try
            {
                Car car = _mapper.Map<Car>(carDTO);
                car.Id = id;

                UpdateCarCommand command = new UpdateCarCommand(car);
                var result = _mediator.ExecuteAsync<UpdateCarCommand, Car>(command).Result;

                HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();
                return Ok(hb.buildHateoas(result, result.Id, _routes));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("AddDelProduct/{delORAdd}")]
        public ActionResult<Car> DelOrAddProductToFitsForCar(int carId, int productId, bool delOrAdd)
        {
            Task<Car> result = null;
            if (carId <= 0 || productId <= 0) return BadRequest("Car-Id or Product-Id is 0 or less");
            if (delOrAdd) // Add
            {
                AddProductCarCommand command = new AddProductCarCommand(carId, productId);
                result = _mediator.ExecuteAsync<AddProductCarCommand, Car>(command);
            }
            else // Remove
            {
                RemoveProductCarCommand command = new RemoveProductCarCommand(carId, productId);
                result = _mediator.ExecuteAsync<RemoveProductCarCommand, Car>(command);
            }
            if (result is null) return BadRequest();
            
            HateoasBuild<Car, int> hb = new HateoasBuild<Car, int>();
            return Ok(hb.buildHateoas(result.Result, result.Result.Id, _routes));
        }
    }

}
