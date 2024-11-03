namespace Project.Areas.Admin.ViewModels
{
    public class EditRolesViewModel
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? AssignedRoles { get; set; }
        public List<string>? AllRoles { get; set; }
    }
}
