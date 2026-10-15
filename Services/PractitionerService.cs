using MyMvcApp.Data;
using MyMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MyMvcApp.Services
{
    public class PractitionerService : IPractitionerService
    {
        private readonly ApplicationDbContext _context;

        public PractitionerService (ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Practitioner practitioner)
        {
            _context.Practitioners.Add(practitioner);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var practitioner = _context.Practitioners.Find(id);

            if (practitioner == null)
            { return; }
            _context.Practitioners.Remove(practitioner);
            _context.SaveChanges();
        }

        public List<Practitioner> GetAll()
        {
           return _context.Practitioners.Include(p => p.Academy).ToList();
        }

        public Practitioner? GetById(int id)
        {
            return _context.Practitioners.Find(id);
        }

        public void Update(Practitioner practitioner)
        {
            _context.Practitioners.Update(practitioner);
            _context.SaveChanges();
        }
    }
}
