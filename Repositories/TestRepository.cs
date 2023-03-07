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
            throw new NotImplementedException();
        }

        public Test GetById(Guid id)
        {
            return TestDAO.Instance.GetById(id);
        }

        public Task<Test> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }

        public void Update(Test t)
        {
            TestDAO.Instance.Update(t);
        }
    }
}
