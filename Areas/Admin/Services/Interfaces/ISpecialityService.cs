namespace Project.Areas.Admin.Services.Interfaces
{
    public interface ISpecialityService
    {
        void ClearAll();
        void UploadExcel(IFormFile file);
    }
}
