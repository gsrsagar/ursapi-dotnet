using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using urs_api.DbContexts;
using urs_api.Models;
using urs_api.Models.Implementaions;
using urs_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace urs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PastProgramsController : ControllerBase
    {

        private URSDbContext _context;
        private IPastProgramService _service;
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

        public PastProgramsController(URSDbContext context , IPastProgramService service)
        {
            _context = context;
            _service = service;
        }
        
        // GET: api/<PastProgramsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<PastProgram>))]
        public ActionResult<IEnumerable<PastProgram>> Get()
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Past Programs");
                IEnumerable<PastProgram> result = _service.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // GET api/<PastProgramsController>/5
        [HttpGet("{id}")]
        public ActionResult<PastProgram> Get(int id)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Users");
                PastProgram result = _service.GetById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // POST api/<PastProgramsController>
        [HttpPost]
        public ActionResult<PastProgram> Post([FromBody] PastProgram body)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Users");
                PastProgram result = _service.Insert(body);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // PUT api/<PastProgramsController>/5
        [HttpPut("{id}")]
        public ActionResult<PastProgram> Put(int id, [FromBody] PastProgram body)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Users");
                PastProgram result = _service.Update(body);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // DELETE api/<PastProgramsController>/5
        [HttpDelete("{id}")]
        public ActionResult<PastProgram> Delete(int id)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Users");
                PastProgram result = _service.Delete(id);

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
