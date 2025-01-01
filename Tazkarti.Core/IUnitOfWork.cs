using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;
using Tazkarti.Core.Repositories;

namespace Tazkarti.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SavaAsync();
        IGenericRepository<T> repository<T>() where T : BaseModel;
    }
}
