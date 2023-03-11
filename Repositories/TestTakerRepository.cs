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
    public class TestTakerRepository : ITestTakerRepository
    {
        public void Create(TestTaker t)
        {
            TestTakerDAO.Instance.Create(t);
        }

        public void Delete(TestTaker t)
        {
            throw new NotImplementedException();
        }

        public List<TestTaker> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<TestTaker>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TestTaker GetById(string id)
        {
            throw new NotImplementedException();
        }

        public TestTaker GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public TestTaker Login(string email, string password)
        {
            return TestTakerDAO.Instance.Login(email, password);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(TestTaker t)
        {
            throw new NotImplementedException();
        }
    }
}
