using BusinessObjects.Models;
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
        public void Create(TestCreator t)
        {
            throw new NotImplementedException();
        }

        public void Delete(TestCreator t)
        {
            throw new NotImplementedException();
        }

        public List<TestCreator> GetAll()
        {
            throw new NotImplementedException();
        }

        public TestCreator GetById(string id)
        {
            throw new NotImplementedException();
        }

        public List<TestCreator> GetByNameOrEmail(string searchString)
        {
            throw new NotImplementedException();
        }

        public void Update(TestCreator t)
        {
            throw new NotImplementedException();
        }
    }
}
