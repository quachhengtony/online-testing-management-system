using BusinessObjects.Models;
using DAO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TestRepository : ITestRepository
    {
        public void Create(Test t)
        {
            TestDAO.Instance.Create(t);
        }

        public void Delete(Test t)
        {
            TestDAO.Instance.Delete(t);
        }

        public List<Test> GetAll()
        {
            return TestDAO.Instance.GetAll();
        }

        public Task<List<Test>> GetAllAsync()
        {
            return TestDAO.Instance.GetAllAsync();
        }

		public Task<List<Test>> GetAllByName(string name)
        public List<Test> GetAllByBatchForTestTaker()
		{
            return TestDAO.Instance.GetAllByName(name);
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

        public Task<Test> GetByIdAsync(Guid id)
        public Task<Test> GetByIdForTestTakerAsync(Guid id)
        {
            return TestDAO.Instance.GetByIdAsync(id);
            return TestDAO.Instance.GetByIdForTestTakerAsync(id);
        }

        public Task<Test> GetByIdAsync(byte id)
        public Task<List<Test>> GetBySearchForTestTakerAsync(string search)
        {
            throw new NotImplementedException();
            return TestDAO.Instance.GetBySearchForTestTakerAsync(search);
        }

        public async Task<bool> IsDue(Guid id)
        {
            var test = await TestDAO.Instance.GetByIdAsync(id);
            if (test == null)
        public Task<Test> GetByTestNameAndBatchForTestTakerAsync(string batch, string name)
            {
                return false;
            return TestDAO.Instance.GetByTestNameAndBatchForTestTakerAsync(batch, name);
            }
            if (DateTime.Now < test.StartTime)

        public Task<List<string>> GetTestNamesByBatchForTestTaker(string batch)
            {
                return false;
            return TestDAO.Instance.GetTestNamesByBatchForTestTaker(batch);
            }

        public bool IsKeyCodeCorrectForTestTaker(Guid testId, string keyCode)
        {
            return TestDAO.Instance.IsKeyCodeCorrectForTestTaker(testId, keyCode);
            return true;
        }

        public void SaveChanges()
        public bool IsTestAvailableForTestTaker(Guid testId, DateTime currentTime)
		{
            TestDAO.Instance.SaveChanges();
            return TestDAO.Instance.IsTestAvailableForTestTaker(testId, currentTime);
		}

		public void Update(Test t)
        {
            TestDAO.Instance.Update(t);
        }
    }
}
