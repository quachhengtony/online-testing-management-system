using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    interface ITestCreatorRepository : IRepository<TestCreator>
    {
        public List<TestCreator> GetByNameOrEmail(string searchString);
    }
}
