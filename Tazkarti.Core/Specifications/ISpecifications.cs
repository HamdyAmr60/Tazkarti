﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tazkarti.Core.Models;

namespace Tazkarti.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseModel
    {
        public Expression<Func<T ,bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public bool ApplyPagination { get; set; }
        public int Take {  get; set; }
        public int Skip { get; set; }
    }
}
