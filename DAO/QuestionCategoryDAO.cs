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
    public class QuestionCategoryDAO : IDAO<QuestionCategory>
    {
        private static QuestionCategoryDAO instance;
        private static readonly object instanceLock = new object();
        private static OnlineTestingManagementSystemDbContext dbContext;

        public static QuestionCategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new QuestionCategoryDAO();
                        dbContext = new OnlineTestingManagementSystemDbContext();
                    }
                    return instance;
                }
            }
        }

        private QuestionCategoryDAO() { }

        public void Create(QuestionCategory t)
        {
            dbContext.QuestionCategories.Add(t);
            SaveChanges();
        }

        public void Delete(QuestionCategory t)
        {
            dbContext.QuestionCategories.Remove(t);
            SaveChanges();
        }

        public List<QuestionCategory> GetAll()
        {
            return dbContext.QuestionCategories.ToList();
        }

        public Task<List<QuestionCategory>> GetAllAsync()
        {
            return dbContext.QuestionCategories.ToListAsync();
        }

        public QuestionCategory GetById(Guid id)
        {
            return dbContext.QuestionCategories.Find(id);
        }

        public Task<QuestionCategory> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(QuestionCategory t)
        {
            dbContext.QuestionCategories.Update(t);
            SaveChanges();
        }

        public Task<QuestionCategory> GetByIdAsync(byte id)
        {
            return dbContext.QuestionCategories.Where(qc => qc.Id == id).FirstOrDefaultAsync();
        }
    }
}
