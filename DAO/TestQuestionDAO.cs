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
    public class TestQuestionDAO : IDAO<TestQuestion>
    {
        private static TestQuestionDAO instance;
        private static readonly object instaneLock = new object();
        private static OnlineTestingManagementSystemDbContext dbContext;

        public static TestQuestionDAO Instance
        {
            get
            {
                lock (instaneLock)
                {
                    if (instance == null)
                    {
                        instance = new TestQuestionDAO();
                        dbContext = new OnlineTestingManagementSystemDbContext();
                    }
                    return instance;
                }
            }
        }

        private TestQuestionDAO() { }

        public void Create(TestQuestion t)
        {
            dbContext.TestQuestions.Add(t);
        }

        public void Delete(TestQuestion t)
        {
            dbContext.TestQuestions.Remove(t);
        }

        public List<TestQuestion> GetAll()
        {
            return dbContext.TestQuestions.Include(tq => tq.Test).Include(tq => tq.Question).ToList();
        }

        public TestQuestion GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public TestQuestion GetByTestId(string testId)
        {
            return dbContext.TestQuestions.Where(tq => tq.TestId.Equals(testId)).FirstOrDefault();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(TestQuestion t)
        {
            dbContext.TestQuestions.Update(t);
        }

        public Task<List<TestQuestion>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TestQuestion> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TestQuestion> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }
    }
}
