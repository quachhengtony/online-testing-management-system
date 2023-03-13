using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class TestTakerDAO : IDAO<TestTaker>
    {
        private static TestTakerDAO instance = null;
        private static readonly object instanceLock = new object();
        private TestTakerDAO() { }
        public static TestTakerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TestTakerDAO();
                    }
                    return instance;
                }
            }
        }

        public void Create(TestTaker t)
        {
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                dbContext.TestTakers.Add(t);
                dbContext.SaveChanges();
            }
        }

        public void Delete(TestTaker t)
        {
            throw new NotImplementedException();
        }

        public List<TestTaker> GetAll()
        {
            throw new NotImplementedException();
        }

        public TestTaker GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(TestTaker t)
        {
            throw new NotImplementedException();
        }

        public TestTaker Login(string email, string password)
        {
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                return dbContext.TestTakers.SingleOrDefault(c => c.Email == email && c.Password == password);
            }
        }

        public Task<List<TestTaker>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TestTaker GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TestTaker> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
