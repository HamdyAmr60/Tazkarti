﻿using Tazkarti.Core;
using Tazkarti.Core.Repositories;
using Tazkarti.Repository;
using Tazkarti.Repository.Repositories;

namespace Tazkarti.PL.Extentions
{
    public static class ApplicationServices
    {
        public static IServiceCollection ApplicationService(this IServiceCollection Services)
        {
            Services.AddScoped<IUnitOfWork , UnitOfWork>();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return Services;
        }
    }
}
