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
    public class QuestionDAO : IDAO<Question>
    {
        private static QuestionDAO instance;
        private static readonly object instanceLock = new object();
        private static OnlineTestingManagementSystemDbContext dbContext;

        public static QuestionDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new QuestionDAO();
                        dbContext = new OnlineTestingManagementSystemDbContext();
                    }
                    return instance;
                }
            }
        }

        private QuestionDAO() { }

        public void Create(Question t)
        {
            dbContext.Questions.Add(t);
            SaveChanges();
        }

        public void Delete(Question t)
        {
            dbContext.Questions.Remove(t);
            SaveChanges();
        }

        public List<Question> GetAll()
        {
            return dbContext.Questions.Include(q => q.Answers).Include(q => q.QuestionCategory).ToList();
        }

        public Task<List<Question>> GetAllAsync()
        {
            return dbContext.Questions.Include(q => q.Answers).Include(q => q.QuestionCategory).ToListAsync();
        }

        public Question GetById(Guid id)
        {
            return dbContext.Questions.Where(q => q.Id == id).Include(q => q.Answers).Include(q => q.QuestionCategory).FirstOrDefault();
        }

        public Task<Question> GetByIdAsync(Guid id)
        {
            return dbContext.Questions.Where(q => q.Id == id).Include(q => q.Answers).Include(q => q.QuestionCategory).FirstOrDefaultAsync();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Question t)
        {
            dbContext.Questions.Update(t);
            SaveChanges();
        }

        public Task<Question> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }
    }
}
