using Tazkarti.Core;
using Tazkarti.Core.Repositories;
using Tazkarti.Core.Services;
using Tazkarti.PL.Helpers;
using Tazkarti.Repository;
using Tazkarti.Repository.Repositories;
using Tazkarti.Service;

namespace Tazkarti.PL.Extentions
{
    public static class ApplicationServices
    {
        public static IServiceCollection ApplicationService(this IServiceCollection Services)
        {
            Services.AddScoped<IUnitOfWork , UnitOfWork>();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(M => M.AddProfile(typeof(Mapped)));
            Services.AddScoped<ImageResolver, ImageResolver>();
            Services.AddScoped<IQRCodeService,QRCodeService>();
            return Services;
        }
    }
}
