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
        }

        public void Delete(Question t)
        {
            dbContext.Questions.Remove(t);
        }

        public List<Question> GetAll()
        {
            return dbContext.Questions.Include(q => q.Answers).Include(q => q.QuestionCategory).ToList();
        }

        public Task<List<Question>> GetAllAsync()
        {
            return dbContext.Questions.Include(q => q.Answers).Include(q => q.QuestionCategory).ToListAsync();
        }

		public Task<List<Question>> GetAllAsync(Guid questionCreatorId)
		{
			return dbContext.Questions.Where(q => q.QuestionCreatorId == questionCreatorId).Include(q => q.Answers).Include(q => q.QuestionCategory).ToListAsync();
		}

		public Question GetById(Guid id)
        {
            return dbContext.Questions.Where(q => q.Id == id).Include(q => q.Answers).Include(q => q.QuestionCategory).FirstOrDefault();
        }

        public Task<List<Question>> GetAllByContent(string content)
        {
            return dbContext.Questions.Where(q => q.Content.Contains(content)).ToListAsync();
        }

		public Task<List<Question>> GetAllByContentAsync(string content, Guid questionCreatorId)
		{
			return dbContext.Questions.Where(q => q.Content.Contains(content) && q.QuestionCreatorId == questionCreatorId).ToListAsync();
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
        }

        public Task<Question> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }
    }
}
