using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public class AcademyService : IAcademyService
    {
        private readonly ApplicationDbContext _context;

        public AcademyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Academy academy)
        {
             _context.Academies.Add(academy);
             _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var academy = _context.Academies.Find(id);

            if (academy == null)
            { return; }
                _context.Academies.Remove(academy);
                _context.SaveChanges();
        }
        public List<Academy> GetAll()
        {
            return _context.Academies.ToList();
        }

        public Academy? GetById(int id)
        {
            return _context.Academies.Find(id);
        }

        public void Update(Academy academy)
        {
            _context.Academies.Update(academy);
            _context.SaveChanges();
        }
    }
}
