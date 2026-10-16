using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface ITechniqueService
    {
        List<Technique> GetAll();
        Technique? GetById(int id);
        void Create(Technique technique);
        void Update(Technique technique);
        void Delete(int id);
    }
}
