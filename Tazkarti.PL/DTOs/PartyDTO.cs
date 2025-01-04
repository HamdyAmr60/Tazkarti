using System.ComponentModel.DataAnnotations;
using Tazkarti.Core.Models;

namespace Tazkarti.PL.DTOs
{
    public class PartyDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Time { get; set; }
        [Required]
        public string Address { get; set; }
        public IFormFile? PictureUrl { get; set; }
    }
}
