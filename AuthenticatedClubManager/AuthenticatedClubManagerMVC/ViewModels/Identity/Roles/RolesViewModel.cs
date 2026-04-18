using System.ComponentModel.DataAnnotations;

namespace AuthenticatedClubManagerMVC.ViewModels.Identity.Roles
{
    public class RolesViewModel
    {

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
