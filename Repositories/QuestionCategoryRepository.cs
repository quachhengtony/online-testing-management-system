using BusinessObjects.Models;
using DAO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class QuestionCategoryRepository : IQuestionCategoryRepository
    {
        public void Create(QuestionCategory t)
        {
            QuestionCategoryDAO.Instance.Create(t);
        }

        public void Delete(QuestionCategory t)
        {
            QuestionCategoryDAO.Instance.Delete(t);
        }

        public List<QuestionCategory> GetAll()
        {
            return QuestionCategoryDAO.Instance.GetAll();
        }

        public Task<List<QuestionCategory>> GetAllAsync()
        {
            return QuestionCategoryDAO.Instance.GetAllAsync();
        }

        public QuestionCategory GetById(Guid id)
        {
            return QuestionCategoryDAO.Instance.GetById(id);
        }

        public Task<QuestionCategory> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionCategory> GetByIdAsync(byte id)
        {
            return QuestionCategoryDAO.Instance.GetByIdAsync(id);
        }

        public void Update(QuestionCategory t)
        {
            QuestionCategoryDAO.Instance.Update(t);
        }
    }
}
