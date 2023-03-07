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
    public class TestCategoryRepository : ITestCategoryRepository
    {
        public void Create(TestCategory t)
        {
            TestCategoryDAO.Instance.Create(t);
        }

        public void Delete(TestCategory t)
        {
            TestCategoryDAO.Instance.Delete(t);
        }

        public List<TestCategory> GetAll()
        {
            return TestCategoryDAO.Instance.GetAll();
        }

        public Task<List<TestCategory>> GetAllAsync()
        {
            return TestCategoryDAO.Instance.GetAllAsync();
        }

        //public TestCategory GetById(Guid id)
        //{
        //    return TestCategoryDAO.Instance.GetById(id);
        //}

        //public Task<TestCategory> GetByIdAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<TestCategory> GetByIdAsync(byte id)
        {
            return TestCategoryDAO.Instance.GetByIdAsync(id);
        }

		public void SaveChanges()
		{
            TestCategoryDAO.Instance.SaveChanges();
		}

		public void Update(TestCategory t)
        {
            TestCategoryDAO.Instance.Update(t);
        }
    }
}
