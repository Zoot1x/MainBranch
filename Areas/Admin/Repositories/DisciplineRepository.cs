using Microsoft.EntityFrameworkCore;
using Project.Areas.Admin.Models.Parser;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Data.EF;

namespace Project.Areas.Admin.Repositories
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly ParserDbContext _context;

        public DisciplineRepository(ParserDbContext context)
        {
            _context = context;
        }

        public void Add(Discipline discipline)
        {
            _context.Disciplines.Add(discipline);
        }

        public void RemoveRange(IEnumerable<Discipline> disciplines)
        {
            _context.RemoveRange(disciplines);
        }

        public IEnumerable<Discipline> GetAll()
        {
            return _context.Disciplines
            .Include(d => d.Semesters)
            .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<Discipline> disciplines)
        {
            _context.Disciplines.AddRange(disciplines);
        }
    }
}
