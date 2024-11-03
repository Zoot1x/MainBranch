using Project.Areas.Admin.Models.Parser;

namespace Project.Areas.Admin.Repositories.Interfaces
{
    public interface ISpecialityRepository
    {
        void Add(Speciality speciality);
        void RemoveRange(IEnumerable<Speciality> specialities);
        void AddRange(IEnumerable<Speciality> specialities);
        IEnumerable<Speciality> GetAll();
        void SaveChanges();
    }
}
