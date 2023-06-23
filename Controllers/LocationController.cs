using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using urs_api.DbContexts;
using urs_api.Models.Implementaions;
using urs_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace urs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly URSDbContext _attachmentContext;
        private ILocationService _service;
        private URSDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IConfiguration GetConfig()
        {
            var Config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            return Config.Build();
        }

        private URSDbContext InitializingContext()
        {
            URSDbContext Context = new URSDbContext();
            var Config = GetConfig();
            Context.Database.GetDbConnection().ConnectionString = Config.GetSection("ConnectionStrings").GetSection("OnlineFormDatabase").Value;
            return Context;
        }

        public LocationController(URSDbContext context , ILocationService service)
        {
            _context = context;
            _service = service;
        }


        // GET: api/<LocationController>
        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Locations");
                IEnumerable<Location> result = _service.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        public ActionResult<Location> Get(int id)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Locations");
                Location result = _service.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // POST api/<LocationController>
        [HttpPost]
        public ActionResult<Location> Post([FromBody] Location body)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Locations");
                Location result = _service.Post(body);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public ActionResult<Location> Put(int id, [FromBody] Location body)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Locations");
                Location result = _service.Put(body);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public ActionResult<Location> Delete(int id)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Locations");
                Location result = _service.Delete(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }
    }
}
