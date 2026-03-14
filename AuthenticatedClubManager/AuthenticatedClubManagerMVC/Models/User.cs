using Microsoft.AspNetCore.Identity;

namespace AuthenticatedClubManagerMVC.Models
{
    public class User : IdentityUser
    {
        
        public string? Address { get; set; }


    }
}
