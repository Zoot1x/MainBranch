using Project.Models.Parser;

namespace Project.Repositories
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
