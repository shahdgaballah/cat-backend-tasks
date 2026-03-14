using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace AuthenticatedClubManagerMVC.Models
{
    public class Club
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Remote("IsClubNameExist", "Club", HttpMethod ="Post", ErrorMessage ="Name already exits")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Precision(10, 2)]
        public decimal Price { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
        public string? ImagePath { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        //navigation prop
        public Category? Category { get; set; }


    }
}
