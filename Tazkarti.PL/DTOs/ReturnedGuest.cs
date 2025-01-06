using System.ComponentModel.DataAnnotations;

namespace Tazkarti.PL.DTOs
{
    public class ReturnedGuest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public string QRCodeUrl { get; set; }
    }
}
