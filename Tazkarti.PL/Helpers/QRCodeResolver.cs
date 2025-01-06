using AutoMapper;
using Tazkarti.Core.Models;
using Tazkarti.PL.DTOs;

namespace Tazkarti.PL.Helpers
{
    public class QRCodeResolver : IValueResolver<Guest, ReturnedGuest, string>
    {
        private readonly IConfiguration _configuration;

        public QRCodeResolver(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Resolve(Guest source, ReturnedGuest destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.QrCodeUrl))
            {
                return $"{_configuration["AppUrl"]}{source.QrCodeUrl}";
            }
            return string.Empty;
        }
    }
}
