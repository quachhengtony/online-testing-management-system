using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO
{
    interface IDAO<T>
    {
        public void Create(T t);
        public List<T> GetAll();
        public Task<List<T>> GetAllAsync();
        public T GetById(Guid id);
        public Task<T> GetByIdAsync(Guid id);
        public void Update(T t);
        public void Delete(T t);
        public void SaveChanges();
    }
}
