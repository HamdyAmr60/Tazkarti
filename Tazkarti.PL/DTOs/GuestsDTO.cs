using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tazkarti.Core.Models;

namespace Tazkarti.PL.DTOs
{
    public class GuestsDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public int PartyId { get; set; }
    }
}
