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
        private static readonly object instaneLock = new object();
        private static OnlineTestingManagementSystemDbContext dbContext;

        public static TestDAO Instance
        {
            get
            {
                lock(instaneLock)
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
            return dbContext.Tests.ToList();
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
    }
}
