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
        Task<List<Test>> GetBySearchForTestTakerAsync(string search);
        bool IsKeyCodeCorrectForTestTaker(Guid testId, string keyCode);
        bool IsTestAvailableForTestTaker(Guid testId, DateTime currentTime);
        Task<List<String>> GetTestNamesByBatchForTestTaker(string batch);
        List<Test> GetAllByBatchForTestTaker();
        Task<Test> GetByTestNameAndBatchForTestTakerAsync(string batch, string name);
    }
}
