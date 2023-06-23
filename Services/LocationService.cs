using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using urs_api.DbContexts;
using urs_api.Helpers;
using urs_api.Models.Implementaions;

namespace urs_api.Services
{

    public interface ILocationService
    {
        Location Get(int locationId);
        IEnumerable<Location> GetAll();
        Location Delete(int locationId);
        Location Put(Location body);
        Location Post(Location body);
    }
    public class LocationService : ILocationService
    {


        private URSDbContext _context;
        private readonly AppSettings _appSettings;

        public LocationService(
            URSDbContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public Location Delete(int locationId)
        {
            Location obj = _context.Location.Find(locationId);
            return (Location)_context.Location.Remove(obj);
        }

        public Location Get(int locationId)
        {
            return _context.Location.Find(locationId);
        }

        public IEnumerable<Location> GetAll()
        {
            return _context.Location.ToList();
        }

        public Location Post(Location body)
        {
            return (Location)_context.Location.AddAsync(body);
        }

        public Location Put(Location body)
        {
            return (Location)_context.Location.Update(body);
        }
    }
}
