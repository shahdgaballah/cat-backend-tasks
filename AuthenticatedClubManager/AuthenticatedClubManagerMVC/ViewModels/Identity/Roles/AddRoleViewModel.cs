using System.ComponentModel.DataAnnotations;

namespace AuthenticatedClubManagerMVC.ViewModels.Identity.Roles
{
    public class AddRoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
