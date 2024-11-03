using Project.Areas.Admin.Models.Parser;

namespace Project.Areas.Admin.Repositories.Interfaces
{
    public interface ISemesterRepository
    {
        IEnumerable<Semester> GetAll();
    }
}
