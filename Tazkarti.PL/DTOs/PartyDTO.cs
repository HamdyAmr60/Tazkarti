using Tazkarti.Core.Models;

namespace Tazkarti.PL.DTOs
{
    public class PartyDTO
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public string Address { get; set; }
        public IFormFile PictureUrl { get; set; }
    }
}
