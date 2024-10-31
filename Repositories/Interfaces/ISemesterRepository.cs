using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Models.Parser;

namespace Project.Repositories.Interfaces
{
    public interface ISemesterRepository
    {
        IEnumerable<Semester> GetAll();
    }
}