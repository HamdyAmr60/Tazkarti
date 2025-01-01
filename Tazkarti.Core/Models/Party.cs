using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazkarti.Core.Models
{
    public class Party:BaseModel
    {
        public string Name { get; set; }
        public string Time {  get; set; }
        public string Address { get; set; }
        public string invitationUrl { get; set; }
        public List<Guest> Guests { get; set; } = new List<Guest>();
    }
}
