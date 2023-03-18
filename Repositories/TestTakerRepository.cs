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
        public TestTaker CheckDuplicateEmailOrUserName(string username, string email)
        {
            return TestTakerDAO.Instance.CheckDuplicateUserNameOrEmail(username, email);
        }

        public void Create(TestTaker t)
        {
            TestTakerDAO.Instance.Create(t);
        }

        public void Delete(TestTaker t)
        {
            TestTakerDAO.Instance.Delete(t);
        }

        public List<TestTaker> GetAll()
        {
            return TestTakerDAO.Instance.GetAll();
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
            return TestTakerDAO.Instance.GetById(id);
        }

        public List<TestTaker> GetTestTakersByName(string name)
        {
            return TestTakerDAO.Instance.GetTakersByName(name);
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
            TestTakerDAO.Instance.Update(t);
        }
    }
}
