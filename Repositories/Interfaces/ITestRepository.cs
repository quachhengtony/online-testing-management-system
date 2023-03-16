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
        Task<Test> GetByIdForTestTakerAsync(Guid id);
        List<Test> GetBySearchForTestTaker(string search);
        bool IsKeyCodeCorrectForTestTaker(Guid testId, string keyCode);
        bool IsTestAvailableForTestTaker(Guid testId, DateTime currentTime);
        Task<List<String>> GetTestNamesByBatchForTestTaker(string batch);
        List<Test> GetAllByBatchForTestTaker();
        Task<Test> GetByTestNameAndBatchForTestTakerAsync(string batch, string name);
        public Test GetById(Guid id);
        public Task<Test> GetByIdAsync(Guid id);
        public Task<List<Test>> GetAllByName(string name);
        public Task<bool> IsDue(Guid id);
        public Task<Test> GetByBatch(string batch);
        public Task<List<string>> GetAllUniqueBatchesOfTestCreator(Guid testCreatorId); 
    }
}
