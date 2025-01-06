using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;

namespace Tazkarti.Core.Specifications.PartySpecs
{
    public class PartyWithGuests :BaseSpecifications<Party>
    {
        public PartyWithGuests():base()
        {
            Includes.Add(P=>P.Guests);
        }
        public PartyWithGuests(int id):base(P=>P.Id == id)
        {
            Includes.Add(P => P.Guests);
        }
    }
}
