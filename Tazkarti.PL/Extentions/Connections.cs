using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Tazkarti.Repository.Data;

namespace Tazkarti.PL.Extentions
{
    public static class Connections
    {
        public static IServiceCollection ConnectDB(this IServiceCollection Services , WebApplicationBuilder builder)
        {
            Services.AddDbContext<TazkartiDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            return Services;
        }
    }
}
