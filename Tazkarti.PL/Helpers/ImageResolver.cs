using AutoMapper;
using Tazkarti.Core.Models;
using Tazkarti.PL.DTOs;

namespace Tazkarti.PL.Helpers
{
    public class ImageResolver : IValueResolver<Party, ReturnedParty, string>
    {
        private readonly IConfiguration _configuration;

        public ImageResolver(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string Resolve(Party source, ReturnedParty destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.invitationUrl))
            {
                return $"{_configuration["AppUrl"]}{source.invitationUrl}";
            }
            return string.Empty ;
        }
    }
}
