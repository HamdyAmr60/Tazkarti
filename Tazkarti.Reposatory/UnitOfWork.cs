using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core;
using Tazkarti.Core.Models;
using Tazkarti.Core.Repositories;
using Tazkarti.Repository.Data;
using Tazkarti.Repository.Repositories;

namespace Tazkarti.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly TazkartiDbContext _dbContext;

        public UnitOfWork(TazkartiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ValueTask DisposeAsync()
        =>   _dbContext.DisposeAsync();
       

        public IGenericRepository<T> repository<T>() where T : BaseModel
        {
            var Type = typeof(T).Name;
            if (!_repositories.ContainsKey(Type))
            { 
                var repo = new GenericRepository<T>(_dbContext);
                _repositories.Add(Type, repo);
            }
            return _repositories[Type] as IGenericRepository<T>;
        }

        public Task<int> SavaAsync()
        => _dbContext.SaveChangesAsync();
        
    }
}
