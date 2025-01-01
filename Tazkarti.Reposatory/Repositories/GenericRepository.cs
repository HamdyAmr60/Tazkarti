using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;
using Tazkarti.Core.Repositories;
using Tazkarti.Repository.Data;

namespace Tazkarti.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly TazkartiDbContext _dbContext;

        public GenericRepository(TazkartiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        => await _dbContext.Set<T>().AddAsync(entity);
        

        public void Delete(T entity)
        => _dbContext.Set<T>().Remove(entity);
        
        public async Task<IReadOnlyList<T>> GetAllAsync()
          =>  await _dbContext.Set<T>().ToListAsync();
        

        public async Task<T> GetByIdAsync(int id)
        => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T entity)
        => _dbContext.Set<T>().Update(entity);
    }
}
