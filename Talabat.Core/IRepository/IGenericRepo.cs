using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specifications;

namespace Talabat.Core.IRepository
{
    public interface IGenericRepo<T>where T : BaseModel
    {
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);

    }
}
