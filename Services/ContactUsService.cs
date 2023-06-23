using Microsoft.Extensions.Options;
using urs_api.DbContexts;
using urs_api.Helpers;
using urs_api.Models.Implementaions;

namespace urs_api.Services
{
    public interface IContactUsService
    {
        ContactUs Get(int contactUsId);
        IEnumerable<ContactUs> GetAll();
        ContactUs Delete(int contactUsId);
        ContactUs Put(ContactUs body);
        ContactUs Post(ContactUs body);
    }
    public class ContactUsService : IContactUsService
    {

        private URSDbContext _context;
        private readonly AppSettings _appSettings;

        public ContactUsService(
            URSDbContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public ContactUs Delete(int contactUsId)
        {
            ContactUs obj = _context.ContactUs.Find(contactUsId);
            return (ContactUs)_context.ContactUs.Remove(obj);
        }

        public ContactUs Get(int contactUsId)
        {
            return _context.ContactUs.Find(contactUsId);
        }

        public IEnumerable<ContactUs> GetAll()
        {
            return _context.ContactUs.ToList();
        }

        public ContactUs Post(ContactUs body)
        {
            return (ContactUs) _context.ContactUs.AddAsync(body);
        }

        public ContactUs Put(ContactUs body)
        {
            return (ContactUs)_context.ContactUs.Update(body);
        }
    }
}
