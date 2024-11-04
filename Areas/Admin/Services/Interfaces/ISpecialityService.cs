namespace Project.Areas.Admin.Services.Interfaces
{
    public interface ISpecialityService
    {
        void ClearAll();
        bool UploadExcel(IFormFile file);
    }
}
