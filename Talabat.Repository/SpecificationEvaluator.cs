using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator<T>where T : BaseModel
    {
        public static IQueryable<T> CreateQuery(IQueryable<T> inputQuery,ISpecification<T> spec)
        {
            var query = inputQuery;
            if(spec.Criteria != null)
            {
                query= query.Where(spec.Criteria);
            }
            if(spec.IsPaginationEnabled)
                query=query.Skip(spec.Skip).Take(spec.Take);
            if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            if(spec.OrderByDesc is not null)
                query=query.OrderByDescending(spec.OrderByDesc);
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;

        }

    }
}
