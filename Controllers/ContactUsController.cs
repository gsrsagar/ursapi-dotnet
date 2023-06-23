using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using urs_api.DbContexts;
using urs_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace urs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {

        private URSDbContext _context;
        private IContactUsService _service;
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

        public ContactUsController(URSDbContext context , IContactUsService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/<ContactUsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ContactUsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContactUsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContactUsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactUsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
