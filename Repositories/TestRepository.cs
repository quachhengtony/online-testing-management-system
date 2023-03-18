using BusinessObjects.Models;
using DAO;
using Repositories.Interfaces;
using System;
using System.Collections;
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
        {
            return TestDAO.Instance.GetAllByName(name);
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
                }
                else
                {
                    batchs.Add(test.Batch);
                }
            }
            return tests;
        }
        public List<Test> GetAllByBatchForTestCreator(Guid testCreatorId)
        {
            var tests = TestDAO.Instance.GetAllForTestCreatorAsync(testCreatorId).Result;
            var batchs = new List<String>();
            foreach (var test in tests.ToList())
            {
                if (batchs != null && batchs.Contains(test.Batch))
                {
                    tests.Remove(test);
                }
                else
                {
                    batchs.Add(test.Batch);
                }
            }
            return tests;
        }

        public Task<List<Test>> GetAllByCreatorId(Guid creatorId)
        {
            return TestDAO.Instance.GetAllByCreatorId(creatorId);
        }

        public Task<List<Test>> GetAllByNameAndCreatorId(string name, Guid creatorId)
        {
            return TestDAO.Instance.GetAllByNameAndCreatorId(name,creatorId);
        }

        public Test GetById(Guid id)
        {
            return TestDAO.Instance.GetById(id);
        }

        public Task<Test> GetByIdAsync(Guid id)
        {
            return TestDAO.Instance.GetByIdAsync(id);
        }

        public Task<Test> GetByIdForTestTakerAsync(Guid id)
        {
            return TestDAO.Instance.GetByIdForTestTakerAsync(id);
        }

        public Task<Test> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }
        public List<Test> GetBySearchForTestTaker(string search)
        {
            var tests = TestDAO.Instance.GetBySearchForTestTakerAsync(search).Result;
            var batchs = new List<String>();
            foreach (var test in tests.ToList())
            {
                if (batchs != null && batchs.Contains(test.Batch))
                {
                    tests.Remove(test);
                }
                else
                {
                    batchs.Add(test.Batch);
                }
            }
            
            return tests;
        }

        public List<Test> GetBySearchForTestCreator(string search, Guid creatorId)
        {
            var tests = TestDAO.Instance.GetBySearchForTestCreatorAsync(search, creatorId).Result;
            var batchs = new List<String>();
            foreach (var test in tests.ToList())
            {
                if (batchs != null && batchs.Contains(test.Batch))
                {
                    tests.Remove(test);
                }
                else
                {
                    batchs.Add(test.Batch);
                }
            }

            return tests;

        }

        public async Task<bool> IsDue(Guid id)
        {
            var test = await TestDAO.Instance.GetByIdAsync(id);
            if (test == null)
            {
                return false;
            }
            if (DateTime.Now < test.StartTime)
            {
                return false;
            }
            return true;
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

        public void SaveChanges()
        {
            TestDAO.Instance.SaveChanges();
        }

        public bool IsTestAvailableForTestTaker(Guid testId, DateTime currentTime)
		{
            return TestDAO.Instance.IsTestAvailableForTestTaker(testId, currentTime);
		}

		public void Update(Test t)
        {
            TestDAO.Instance.Update(t);
        }

        public Task<Test> GetByBatch(string batch)
        {
            return TestDAO.Instance.GetByBatch(batch);
        }

		public async Task<List<string>> GetAllUniqueBatchesOfTestCreator(Guid testCreatorId)
		{
            List<string> list;
            Hashtable hashtable = new();
            var tests = await TestDAO.Instance.GetAllByTestCreatorAsync(testCreatorId);
            if (tests.Count <= 0) 
            {
                return null;
            }
            foreach (var test in tests)
            {
                if (!hashtable.ContainsKey(test.Batch))
                {
                    hashtable.Add(test.Batch, true);
                }
            }
            list = hashtable.Keys.Cast<string>().ToList();
            return list;
		}
    }
}
