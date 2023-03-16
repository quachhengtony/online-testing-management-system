using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
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
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                dbContext.TestCreators.Remove(t);
                dbContext.SaveChanges();
            }
        }

        public List<TestCreator> GetAll()
        {
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                return dbContext.TestCreators.ToList();
            }
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
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                dbContext.Entry<TestCreator>(t).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
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
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                return dbContext.TestCreators.SingleOrDefault(t => t.Id == id);
            }
        }

        public Task<TestCreator> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<TestCreator> GetCreatorsByName(String name)
        {
            using (var dbContext = new OnlineTestingManagementSystemDbContext())
            {
                return dbContext.TestCreators.Where(c => c.FirstName.Contains(name) || c.LastName.Contains(name)).ToList();
            }
        }
    }
}
