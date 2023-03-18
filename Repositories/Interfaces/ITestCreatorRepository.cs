using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ITestCreatorRepository : IRepository<TestCreator>
    {
        public List<TestCreator> GetByNameOrEmail(string searchString);

        public TestCreator Login(string email, string password);

        public List<TestCreator> GetTestCreatorsByName(String name);

        public TestCreator CheckDuplicateEmailOrUserName(string username, string email);
    }
}
