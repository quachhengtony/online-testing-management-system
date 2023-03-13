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
    public class TestDAO : IDAO<Test>
    {
        private static TestDAO instance;
        private static readonly object instanceLock = new object();
        private static OnlineTestingManagementSystemDbContext dbContext;

        public static TestDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TestDAO();
                        dbContext = new OnlineTestingManagementSystemDbContext();
                    }
                    return instance;
                }
            }
        }

        private TestDAO() { }

        public void Create(Test t)
        {
            dbContext.Tests.Add(t);
            SaveChanges();
        }

        public void Delete(Test t)
        {
            dbContext.Tests.Remove(t);
            SaveChanges();
        }

        public List<Test> GetAll()
        {
            return dbContext.Tests.Include(t => t.TestCategory).Include(t => t.TestCreator).ToList();
        }

        public Task<List<Test>> GetAllAsync()
        {
            return dbContext.Tests.Include(t => t.TestCategory).Include(t => t.TestCreator).ToListAsync();
        }

        public Task<List<Test>> GetBySearchAsync(string search)
        {
            return dbContext.Tests.Include(t => t.TestCategory).Include(t => t.TestCreator).Where(t => t.Batch.Contains(search)).ToListAsync();
        }

        public Test GetById(Guid id)
        {
            return dbContext.Tests.Include(t => t.TestCategory).Include(t => t.TestCreator).Where(t => t.Id == id).FirstOrDefault();
        }

        public Task<Test> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetByIdAsync(Guid id)
        {
            return dbContext.Tests.Include(t => t.TestCategory).Include(t => t.TestCreator).Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<String>> GetTestNamesByBatch(string batch)
        {
            return (from t in dbContext.Tests
                    where t.Batch == batch
                    select t.Name).ToListAsync();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Test t)
        {
            dbContext.Tests.Update(t);
            SaveChanges();
        }

        public bool IsKeyCodeCorrect(Guid testId, string keyCode)
        {
            return dbContext.Tests.Where(t => t.Id == testId && t.KeyCode == keyCode).Count() > 0;
        }

        public bool IsTestAvailable(Guid testId, DateTime currentTime)
        {
            return dbContext.Tests.Where(t => t.Id == testId && t.StartTime < currentTime && t.EndTime > currentTime).Count() > 0;

        }

        public Task<Test> GetByTestNameAndBatchAsync(string batch, string name)
        {
            return dbContext.Tests.Where(t => t.Batch == batch && t.Name == name).Include(t => t.TestQuestions)
                .ThenInclude(tq => tq.Question).ThenInclude(q => q.Answers).FirstOrDefaultAsync();
        }
                    

    }
}
