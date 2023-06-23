using Microsoft.Extensions.Options;
using System.Xml.Linq;
using urs_api.DbContexts;
using urs_api.Helpers;
using urs_api.Models;
using urs_api.Models.Implementaions;

namespace urs_api.Services
{

    public interface IPastProgramService
    {
        PastProgram GetById(int Id);
        PastProgram Delete(int Id);
        IEnumerable<PastProgram> GetAll();
        PastProgram Insert(PastProgram body);
        PastProgram Update(PastProgram body);
    }
    public class PastProgramsService : IPastProgramService
    {

        private URSDbContext _context;
        private readonly AppSettings _appSettings;

        public PastProgramsService(
            URSDbContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public PastProgram Delete(int Id)
        {
            PastProgram result = _context.PastPrograms.Find(Id);
            return (PastProgram)_context.PastPrograms.Remove(result);
           
        }

        public IEnumerable<PastProgram> GetAll()
        {
            return _context.PastPrograms.ToList();
        }

        public PastProgram GetById(int Id)
        {
           return _context.PastPrograms.Find(Id);
        }

        public PastProgram Insert(PastProgram body)
        {
            return (PastProgram)_context.PastPrograms.Add(body);
        }

        public PastProgram Update(PastProgram body)
        {
            return (PastProgram)_context.PastPrograms.Update(body);
            
        }
    }
}
