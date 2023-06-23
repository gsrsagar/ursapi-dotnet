using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using urs_api.DbContexts;
using urs_api.Helpers;
using urs_api.Models;
using urs_api.Models.Implementaions;

namespace urs_api.Services
{

    public interface IUserService
    {
        User Get(int Id);
        IEnumerable<User> GetAll();
        dynamic Delete(int Id);
        dynamic Put(User body);
        dynamic Post(User body);
    }
    public class UserService : IUserService
    {

        private URSDbContext _context;
        private readonly AppSettings _appSettings;

        public UserService(
            URSDbContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }


        public dynamic Delete(int Id)
        {
            var result = _context.Users.Find(Id);
            return _context.Users.Remove(result);
        }

        public User Get(int Id)
        {
            return _context.Users.Find(Id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public dynamic Post(User body)
        {
            return _context.Users.AddAsync(body);
        }

        public dynamic Put(User body)
        {
            return _context.Users.Update(body);
        }
    }
}
