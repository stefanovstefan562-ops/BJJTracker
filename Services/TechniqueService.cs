
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public class TechniqueService : ITechniqueService
    {
        private readonly ApplicationDbContext _context;

        public TechniqueService (ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Technique technique)
        {
            _context.Techniques.Add(technique);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var technique = _context.Techniques.Find(id);

            if (technique == null)
            { return; }
            _context.Techniques.Remove(technique);
            _context.SaveChanges();
        }

        public List<Technique> GetAll()
        {
            return _context.Techniques.ToList();
        }

        public Technique? GetById(int id)
        {
            return _context.Techniques.Find(id);
        }

        public void Update(Technique technique)
        {
            _context.Techniques.Update(technique);
            _context.SaveChanges();
        }
    }
}
