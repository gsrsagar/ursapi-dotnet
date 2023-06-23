using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewEmployeeNotificationServices.ResponseModel;
using NLog;
using urs_api.DbContexts;
using urs_api.Models;
using urs_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace urs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private URSDbContext _context;
        private IUserService _service;
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

        public UserController(URSDbContext context ,IUserService service )
        {
            _context = context;
            _service = service;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get All Users");
                IEnumerable<User> result = _service.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        try
        {
            _context = InitializingContext();
            logger.Info("Received request for Get User by Id" + " " + id);
            User result = _service.Get(id);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.Error(ex, ex.Message);
            throw ex;
        }
    }

    // POST api/<UserController>
    [HttpPost]
        [AllowAnonymous]
        public dynamic Post([FromBody] User body)    
        {
            try
            {
                //_context = InitializingContext();
                logger.Info("Received request for Get User by Id");
                dynamic result = _service.Post(body);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User body)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get User by Id");
                User result = _service.Put(body);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult<dynamic> Delete(int id)
        {
            try
            {
                _context = InitializingContext();
                logger.Info("Received request for Get User by Id");
                User result = _service.Delete(id);

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
