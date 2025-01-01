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
        }
    }
}
