using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tazkarti.Core.Models
{
    public class Guest:BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey("Party")]
        public int PartyId { get; set; }
        public Party Party { get; set; }
        public string QrCodeUrl { get; set; }
    }
}
