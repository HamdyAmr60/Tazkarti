using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;
using Tazkarti.Core.Repositories;
using Tazkarti.Core.Specifications;
using Tazkarti.Repository.Data;
using Tazkarti.Repository.Helpers;

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
        
        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecifications<T> specifications)
          =>  await SpecEvalutor<T>.BuildQuery(_dbContext.Set<T>() , specifications).ToListAsync();
        

        public async Task<T> GetByIdAsync(ISpecifications<T> specifications)
        => await SpecEvalutor<T>.BuildQuery(_dbContext.Set<T>() , specifications).FirstOrDefaultAsync();

        public async Task<T> GetByIdOnlyAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        => _dbContext.Set<T>().Update(entity);
    }
}
