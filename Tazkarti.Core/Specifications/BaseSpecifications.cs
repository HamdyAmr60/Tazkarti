using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;

namespace Tazkarti.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>> ();
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool ApplyPagination { get; set; }

        public BaseSpecifications(Expression<Func<T,bool>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecifications()
        {
            
        }
        public void ApplyOfPagination(int take, int skip)
        {
            ApplyPagination = true;
            Take = take;
            Skip = skip;
        }
    }
}
