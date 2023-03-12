using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ITestRepository : IRepository<Test>
    {
        public Test GetById(Guid id);
        public Task<Test> GetByIdAsync(Guid id);
        public Task<List<Test>> GetAllByName(string name);
        public Task<bool> IsDue(Guid id);
    }
}
