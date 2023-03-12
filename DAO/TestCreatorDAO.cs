using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class TestCreatorDAO : IDAO<TestCreator>
    {
        private static TestCreatorDAO instance = null;
        private static readonly object instanceLock = new object();
        private TestCreatorDAO() { }
        public static TestCreatorDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TestCreatorDAO();
                    }
                    return instance;
                }
            }
        }

        public void Create(TestCreator t)
        {
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                dbContext.TestCreators.Add(t);
                dbContext.SaveChanges();
            }
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

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(TestCreator t)
        {
            throw new NotImplementedException();
        }

        public TestCreator Login(string email, string password)
        {
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                return dbContext.TestCreators.SingleOrDefault(c => c.Email == email && c.Password == password);
            }
        }

        public Task<List<TestCreator>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TestCreator GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TestCreator> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
