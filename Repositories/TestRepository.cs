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

        public List<Test> GetAllByBatch()
        {
            var tests = TestDAO.Instance.GetAllAsync().Result;
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

        public Task<List<Test>> GetBySearchAsync(string search)
        {
            return TestDAO.Instance.GetBySearchAsync(search);
        }

        public Task<Test> GetByTestNameAndBatchAsync(string batch, string name)
        {
            return TestDAO.Instance.GetByTestNameAndBatchAsync(batch, name);
        }

        public Task<List<string>> GetTestNamesByBatch(string batch)
        {
            return TestDAO.Instance.GetTestNamesByBatch(batch);
        }

        public bool IsKeyCodeCorrect(Guid testId, string keyCode)
        {
            return TestDAO.Instance.IsKeyCodeCorrect(testId, keyCode);
        }

        public bool IsTestAvailable(Guid testId, DateTime currentTime)
        {
            return TestDAO.Instance.IsTestAvailable(testId, currentTime);
        }

        public void Update(Test t)
        {
            throw new NotImplementedException();
        }
    }
}
