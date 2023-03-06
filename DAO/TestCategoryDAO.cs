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
    public class TestCategoryDAO : IDAO<TestCategory>
    {
        private static TestCategoryDAO instance;
        private static readonly object instanceLock = new object();
        private static OnlineTestingManagementSystemDbContext dbContext;

        public static TestCategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TestCategoryDAO();
                        dbContext = new OnlineTestingManagementSystemDbContext();
                    }
                    return instance;
                }
            }
        }

        private TestCategoryDAO() { }

        public void Create(TestCategory t)
        {
            dbContext.TestCategories.Add(t);
        }

        public void Delete(TestCategory t)
        {
            dbContext.TestCategories.Remove(t);
        }

        public List<TestCategory> GetAll()
        {
            return dbContext.TestCategories.ToList();
        }

        public Task<List<TestCategory>> GetAllAsync()
        {
            return dbContext.TestCategories.ToListAsync();
        }

        public TestCategory GetById(Guid id)
        {
            return dbContext.TestCategories.Find(id);
        }

        public Task<TestCategory> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(TestCategory t)
        {
            dbContext.TestCategories.Update(t);
        }

        public Task<TestCategory> GetByIdAsync(byte id)
        {
            return dbContext.TestCategories.Where(qc => qc.Id == id).FirstOrDefaultAsync();
        }
    }
}
