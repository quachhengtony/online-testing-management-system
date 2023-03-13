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
        Task<List<Test>> GetBySearchAsync(string search);
        bool IsKeyCodeCorrect(Guid testId, string keyCode);
        bool IsTestAvailable(Guid testId, DateTime currentTime);
        Task<List<String>> GetTestNamesByBatch(string batch);
        List<Test> GetAllByBatch();
        Task<Test> GetByTestNameAndBatchAsync(string batch, string name);
    }
}
