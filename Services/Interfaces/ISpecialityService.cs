using Microsoft.AspNetCore.Http;
using Project.Models.Parser;

namespace Project.Services
{

    public interface ISpecialityService
    {
        void ClearAll();
        void UploadExcel(IFormFile file);
    }

}