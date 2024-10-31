using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Data.EF;
using Project.Models.Parser;
using Project.Repositories.Interfaces;

namespace Project.Repositories
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
