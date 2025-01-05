using AutoMapper;
using Tazkarti.Core.Models;
using Tazkarti.PL.DTOs;

namespace Tazkarti.PL.Helpers
{
    public class Mapped : Profile
    {
        public Mapped()
        {
            CreateMap<PartyDTO, Party>();
            CreateMap<Party, ReturnedParty>().ForMember(p=>p.invitationUrl , o=>o.MapFrom<ImageResolver>());
            CreateMap<GuestsDTO, Guest>();
            CreateMap<Guest, ReturnedGuest>();
        }
    }
}
