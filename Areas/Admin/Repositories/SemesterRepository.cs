using Project.Areas.Admin.Models.Parser;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Data.EF;

namespace Project.Areas.Admin.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly ParserDbContext _context;

        public SemesterRepository(ParserDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Semester> GetAll()
        {
            return _context.Semesters.ToList();
        }
    }
}
