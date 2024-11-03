using Project.Areas.Admin.Models.Parser;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Data.EF;


namespace Project.Areas.Admin.Repositories
{
    public class SpecialityRepository : ISpecialityRepository
    {
        private readonly ParserDbContext _context;

        public SpecialityRepository(ParserDbContext context)
        {
            _context = context;
        }

        public void Add(Speciality speciality)
        {
            _context.Specialities.Add(speciality);
        }

        public void RemoveRange(IEnumerable<Speciality> specialities)
        {
            _context.RemoveRange(specialities);
        }

        public IEnumerable<Speciality> GetAll()
        {
            return _context.Specialities.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        
        public void AddRange(IEnumerable<Speciality> specialities)
        {
            _context.Specialities.AddRange(specialities);
        }
    }
}
