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
        Task<List<Test>> GetAllByTestCreatorAsync(Guid testCreatorId);
		Task<Test> GetByIdForTestTakerAsync(Guid id);
        List<Test> GetBySearchForTestTaker(string search);
        bool IsKeyCodeCorrectForTestTaker(Guid testId, string keyCode);
        bool IsTestAvailableForTestTaker(Guid testId, DateTime currentTime);
        Task<List<String>> GetTestNamesByBatchForTestTaker(string batch);
        List<Test> GetAllByBatchForTestTaker();
        List<Test> GetAllByBatchForTestCreator(Guid testCreatorId);
        Task<Test> GetByTestNameAndBatchForTestTakerAsync(string batch, string name);
        List<Test> GetBySearchForTestCreator(string search, Guid creatorId);
        public Test GetById(Guid id);
        public Task<Test> GetByIdAsync(Guid id);
        public Task<List<Test>> GetAllByName(string name);
		public Task<List<Test>> GetAllByNameAsync(string name, Guid testCreatorId);
		public Task<List<Test>> GetAllByNameAndCreatorId(string name, Guid creatorId);
        public Task<List<Test>> GetAllByCreatorId(Guid creatorId);
        public Task<bool> IsDue(Guid id);
		public Task<List<Test>> GetAllByBatchAsync(string batch);
		public Task<Test> GetByBatch(string batch);
        public Task<List<string>> GetAllUniqueBatchesOfTestCreator(Guid testCreatorId);
    }
}
