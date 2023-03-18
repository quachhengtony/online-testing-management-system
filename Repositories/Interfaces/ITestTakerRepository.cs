using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ITestTakerRepository : IRepository<TestTaker>
    {
        public TestTaker Login(string email, string password);

        public List<TestTaker> GetTestTakersByName(String name);

        public TestTaker CheckDuplicateEmailOrUserName(string username, string email);
    }
}
