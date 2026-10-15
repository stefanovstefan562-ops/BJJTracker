using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface IPractitionerService
    {
        List<Practitioner> GetAll();
        Practitioner? GetById(int id);
        void Create(Practitioner practitioner);
        void Update(Practitioner practitioner);
        void Delete(int id);
    }
}
