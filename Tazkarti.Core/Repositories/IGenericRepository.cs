using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;
using Tazkarti.Core.Specifications;

namespace Tazkarti.Core.Repositories
{
    public interface IGenericRepository<T> where T :BaseModel
    {
        Task<IReadOnlyList<T>> GetAllAsync(ISpecifications<T> specifications);
        Task<T> GetByIdAsync(ISpecifications<T> specifications);
        Task<T> GetByIdOnlyAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
