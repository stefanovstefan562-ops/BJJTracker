using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface IAcademyService
    {
        List<Academy> GetAll();
        Academy? GetById(int id);
        void Create(Academy  academy);
        void Update(Academy academy);
        void Delete(int id);

    }
}
