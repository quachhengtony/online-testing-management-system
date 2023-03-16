
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
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
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                dbContext.TestTakers.Remove(t);
                dbContext.SaveChanges();
            }
        }

        public List<TestTaker> GetAll()
        {
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                return dbContext.TestTakers.ToList();
            }
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
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                dbContext.Entry<TestTaker>(t).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
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
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                return dbContext.TestTakers.SingleOrDefault(t => t.Id == id);
            }
        }

        public Task<TestTaker> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<TestTaker> GetTakersByName(String name)
        {
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                return dbContext.TestTakers.Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name)).ToList();
            }
        }
    }
}
