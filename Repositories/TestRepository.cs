using BusinessObjects.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace Repositories
{
    public class TestRepository : ITestRepository
    {
        public void Create(Test t)
        {
            throw new NotImplementedException();
        }

        public void Delete(Test t)
        {
            throw new NotImplementedException();
        }

        public List<Test> GetAll()
        {
            return TestDAO.Instance.GetAll();
        }

        public Task<List<Test>> GetAllAsync()
        {
            return TestDAO.Instance.GetAllAsync();
        }

        public List<Test> GetAllByBatchForTestTaker()
        {
            var tests = TestDAO.Instance.GetAllForTestTakerAsync().Result;
            var batchs = new List<String>();
            foreach (var test in tests.ToList())
            {
                if (batchs != null && batchs.Contains(test.Batch))
                {
                    tests.Remove(test);
                } else
                {
                    batchs.Add(test.Batch);
                }
            }
            return tests;
        }

        public Test GetById(Guid id)
        {
            return TestDAO.Instance.GetById(id);
        }

        public Task<Test> GetByIdForTestTakerAsync(Guid id)
        {
            return TestDAO.Instance.GetByIdForTestTakerAsync(id);
        }

        public Task<List<Test>> GetBySearchForTestTakerAsync(string search)
        {
            return TestDAO.Instance.GetBySearchForTestTakerAsync(search);
        }

        public Task<Test> GetByTestNameAndBatchForTestTakerAsync(string batch, string name)
        {
            return TestDAO.Instance.GetByTestNameAndBatchForTestTakerAsync(batch, name);
        }

        public Task<List<string>> GetTestNamesByBatchForTestTaker(string batch)
        {
            return TestDAO.Instance.GetTestNamesByBatchForTestTaker(batch);
        }

        public bool IsKeyCodeCorrectForTestTaker(Guid testId, string keyCode)
        {
            return TestDAO.Instance.IsKeyCodeCorrectForTestTaker(testId, keyCode);
        }

        public bool IsTestAvailableForTestTaker(Guid testId, DateTime currentTime)
        {
            return TestDAO.Instance.IsTestAvailableForTestTaker(testId, currentTime);
        }

        public void Update(Test t)
        {
            throw new NotImplementedException();
        }
    }
}
