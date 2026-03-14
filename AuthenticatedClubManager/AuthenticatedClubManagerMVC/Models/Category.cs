using System.ComponentModel.DataAnnotations;

namespace AuthenticatedClubManagerMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Club> Clubs { get; set; } = new HashSet<Club>();
    }
}
