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
        }

        public void Delete(Test t)
        {
            dbContext.Tests.Remove(t);
        }

        public List<Test> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Test>> GetAllForTestTakerAsync()
        {
            return dbContext.Tests.Include(t => t.TestCategory).Include(t => t.TestCreator).ToListAsync();
        }

        public Task<List<Test>> GetBySearchForTestTakerAsync(string search)
        {
            return dbContext.Tests.Include(t => t.TestCategory).Include(t => t.TestCreator).Where(t => t.Batch.Contains(search)).ToListAsync();
        }

        public Test GetByIdForTestTaker(Guid id)
        {
            return dbContext.Tests.Include(t => t.TestCategory).Include(t => t.TestCreator).Where(t => t.Id == id).FirstOrDefault();
        }

        public Task<Test> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetByIdForTestTakerAsync(Guid id)
        {
            return dbContext.Tests.Include(t => t.TestCategory).Include(t => t.TestCreator).Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<String>> GetTestNamesByBatchForTestTaker(string batch)
        {
            return (from t in dbContext.Tests
                    where t.Batch == batch
                    select t.Name).ToListAsync();
        }
        public Test GetById(Guid id)
        {
            return dbContext.Tests.Find(id);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Test t)
        {
            dbContext.Tests.Update(t);
        }

        public bool IsKeyCodeCorrectForTestTaker(Guid testId, string keyCode)
        {
            return dbContext.Tests.Where(t => t.Id == testId && t.KeyCode == keyCode).Count() > 0;
        }

        public bool IsTestAvailableForTestTaker(Guid testId, DateTime currentTime)
        {
            return dbContext.Tests.Where(t => t.Id == testId && t.StartTime < currentTime && t.EndTime > currentTime).Count() > 0;

        }

        public Task<Test> GetByTestNameAndBatchForTestTakerAsync(string batch, string name)
        {
            return dbContext.Tests.Where(t => t.Batch == batch && t.Name == name).Include(t => t.TestQuestions)
                .ThenInclude(tq => tq.Question).ThenInclude(q => q.Answers).FirstOrDefaultAsync();
        }
        public Task<List<Test>> GetAllAsync()
        {
            return dbContext.Tests.Include(t => t.TestCategory).ToListAsync();
        }

        public Task<List<Test>> GetAllByName(string name)
        {
            return dbContext.Tests.Where(t => t.Name.Contains(name)).ToListAsync();
        }

        public Task<List<Test>> GetAllByNameAndCreatorId(string name, Guid creatorId)
        {
            return dbContext.Tests.Where(t => t.Name.Contains(name) && creatorId.Equals(creatorId)).ToListAsync();
        }

        public Task<List<Test>> GetAllByCreatorId(Guid creatorId)
        {
            return dbContext.Tests.Where(t => creatorId.Equals(creatorId)).ToListAsync();
        }

        public Task<Test> GetByIdAsync(Guid id)
        {
            return dbContext.Tests.Where(t => t.Id == id).Include(t => t.TestCategory).Include(t => t.TestQuestions).ThenInclude(tq => tq.Question).FirstOrDefaultAsync();
        }

        public Task<Test> GetByBatch(string batch)
        {
            return dbContext.Tests.Where(t => t.Batch == batch).FirstOrDefaultAsync();
        }

        public Task<List<Test>> GetAllByTestCreatorAsync(Guid testCreatorId)
        {
            return dbContext.Tests.Where(t => t.TestCreatorId == testCreatorId).ToListAsync();
        }
    }
}
