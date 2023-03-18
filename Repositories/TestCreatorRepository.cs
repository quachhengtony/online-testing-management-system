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
    public class TestCreatorRepository : ITestCreatorRepository
    {
        public TestCreator CheckDuplicateEmailOrUserName(string username, string email)
        {
            return TestCreatorDAO.Instance.CheckDuplicateUserNameOrEmail(username, email);
        }

        public void Create(TestCreator t)
        {
            TestCreatorDAO.Instance.Create(t);
        }

        public void Delete(TestCreator t)
        {
            TestCreatorDAO.Instance.Delete(t);
        }

        public List<TestCreator> GetAll()
        {
            return TestCreatorDAO.Instance.GetAll();
        }

        public Task<List<TestCreator>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TestCreator GetById(Guid id)
        {
            return TestCreatorDAO.Instance.GetById(id);
        }

        public Task<TestCreator> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TestCreator> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }

        public List<TestCreator> GetByNameOrEmail(string searchString)
        {
            throw new NotImplementedException();
        }

        public List<TestCreator> GetTestCreatorsByName(string name)
        {
            return TestCreatorDAO.Instance.GetCreatorsByName(name);
        }

        public TestCreator Login(string email, string password)
        {
            return TestCreatorDAO.Instance.Login(email, password);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(TestCreator t)
        {
            TestCreatorDAO.Instance.Update(t);
        }
    }
}
