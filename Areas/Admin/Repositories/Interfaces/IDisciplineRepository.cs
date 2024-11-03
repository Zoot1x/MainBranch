using Project.Areas.Admin.Models.Parser;

namespace Project.Areas.Admin.Repositories.Interfaces
{
    public interface IDisciplineRepository
    {
        void Add(Discipline discipline);
        void RemoveRange(IEnumerable<Discipline> disciplines);
        void AddRange(IEnumerable<Discipline> disciplines);
        IEnumerable<Discipline> GetAll();
        void SaveChanges();
    }
}
