using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IRepository<T>
    {
        public void Create(T t);
        public List<T> GetAll();
        public Task<List<T>> GetAllAsync();
        public T GetById(Guid id);
        public void Update(T t);
        public void Delete(T t);
		public void SaveChanges();
	}
}
