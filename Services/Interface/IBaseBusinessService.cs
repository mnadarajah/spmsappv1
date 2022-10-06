using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMSCAV1.Services.Interface
{
    public interface IBaseBusinessService<T> : IDisposable where T : class
    {
        //CRUD operation for API endpoint
        Task<List<T>> GetListAsync();
        Task<T> GetByKeyAsync(long? id);
        Task AddAndSaveAsync(T entity);
        Task AttachAndSaveAsync(T entity, long id);
        Task DeleteAndSaveAsync(long id);

    }
}
