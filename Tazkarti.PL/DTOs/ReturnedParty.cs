namespace Tazkarti.PL.DTOs
{
    public class ReturnedParty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Address { get; set; }
        public string invitationUrl { get; set; }
        public List<ReturnedGuest> Guests { get; set; } = new List<ReturnedGuest>();
    }
}
