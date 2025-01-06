using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;
using Tazkarti.Core.Specifications;

namespace Tazkarti.Repository.Helpers
{
    public static class SpecEvalutor <T> where T : BaseModel
    {
        public static IQueryable<T> BuildQuery(IQueryable<T> query , ISpecifications<T> specifications)
        {
            var Query = query;
            if(specifications.Criteria != null) 
                Query = Query.Where(specifications.Criteria);
            if (specifications.ApplyPagination)
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            Query = specifications.Includes.Aggregate(Query , (CurrentQuery , IncludExp) =>CurrentQuery.Include(IncludExp));

            return Query;
        }
    }
}
