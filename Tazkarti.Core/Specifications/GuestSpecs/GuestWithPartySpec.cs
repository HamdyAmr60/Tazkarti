using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;

namespace Tazkarti.Core.Specifications.GuestSpecs
{
    public class GuestWithPartySpec : BaseSpecifications<Guest>
    {
        public GuestWithPartySpec(GuestParam guestParam):base(g=>g.Name == guestParam.Search)
        {
            Includes.Add(g=>g.Party);
            ApplyOfPagination(guestParam.PageSize, (guestParam.PageIndex - 1) * guestParam.PageSize);
        }
        public GuestWithPartySpec(int id):base(g=>g.Id ==  id)
        {
            Includes.Add(g => g.Party);
        }
    }
}
