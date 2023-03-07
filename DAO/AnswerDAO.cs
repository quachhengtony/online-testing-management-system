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
    public class AnswerDAO : IDAO<Answer>
    {
        private static AnswerDAO instance;
        private static readonly object instanceLock = new object();
        private static OnlineTestingManagementSystemDbContext dbContext;

        public static AnswerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AnswerDAO();
                        dbContext = new OnlineTestingManagementSystemDbContext();
                    }
                    return instance;
                }
            }
        }

        private AnswerDAO() { }

        public void Create(Answer t)
        {
            dbContext.Answers.Add(t);
        }

        public void Delete(Answer t)
        {
            dbContext.Answers.Remove(t);
        }

        public List<Answer> GetAll()
        {
            return dbContext.Answers.ToList();
        }

        public Task<List<Answer>> GetAllAsync()
        {
            return dbContext.Answers.ToListAsync();
        }

        public Answer GetById(Guid id)
        {
            return dbContext.Answers.Find(id);
        }

        public Answer GetByQuestionId(Guid questionId)
        {
            return dbContext.Answers.Where(a => a.QuestionId == questionId).FirstOrDefault();
        }

        public Task<List<Answer>> GetAllByQuestionId(Guid questionId)
        {
            return dbContext.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Answer t)
        {
            dbContext.Answers.Update(t);
        }

        public Task<Answer> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Answer> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }
    }
}
