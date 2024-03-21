using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.IRepository;
using Talabat.Core.Models;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepo<T>:IGenericRepo<T>where T : BaseModel
    {
        private readonly StoreContext _storeContext;

        public GenericRepo(StoreContext storeContext)
        {
           _storeContext = storeContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifiction(spec).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifiction(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifiction(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecifiction(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.CreateQuery(_storeContext.Set<T>(), spec);
        }
    }
}
