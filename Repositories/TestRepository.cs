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
		{
            return TestDAO.Instance.GetAllByName(name);
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

        public Task<Test> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
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

        public void SaveChanges()
		{
            TestDAO.Instance.SaveChanges();
		}

		public void Update(Test t)
        {
            TestDAO.Instance.Update(t);
        }
    }
}
