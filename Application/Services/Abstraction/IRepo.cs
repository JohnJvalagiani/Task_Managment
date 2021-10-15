using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task_Managment_System.Core.Models;

namespace Core.Services.Abstraction
{
    public interface IRepo<Tentity> where Tentity : class
    {
        Task<Tentity> Add(Tentity entity);
        void AddRangeAsync(IEnumerable<Tentity> entities);

        Task<Tentity> GetById(string Id);
        Task<IEnumerable<Tentity>> GetAll();
        Task<IEnumerable<Tentity>> GetByQueryAsync(Expression<Func<Tentity, bool>> predicate = null,
            Func<IQueryable<Tentity>, IOrderedQueryable<Tentity>> OrderBy = null);

        Task<bool> Remove(string Id);
        Task<bool> RemoveRange(IEnumerable<Tentity> entities);

        Task<Tentity> Update(Tentity entity);




    }
}
