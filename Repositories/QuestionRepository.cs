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
    public class QuestionRepository : IQuestionRepository
    {
        public void Create(Question t)
        {
            QuestionDAO.Instance.Create(t);
        }

        public void Delete(Question t)
        {
            QuestionDAO.Instance.Delete(t);
        }

        public List<Question> GetAll()
        {
            return QuestionDAO.Instance.GetAll();
        }

        public Task<List<Question>> GetAllAsync()
        {
            return QuestionDAO.Instance.GetAllAsync();
        }

        public Task<List<Question>> GetAllByContent(string content)
        {
            return QuestionDAO.Instance.GetAllByContent(content);
        }

        public Question GetById(Guid id)
        {
            return QuestionDAO.Instance.GetById(id);
        }

        public Task<Question> GetByIdAsync(Guid id)
        {
            return QuestionDAO.Instance.GetByIdAsync(id);
        }

        public Task<Question> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }

        public void Update(Question t)
        {
            QuestionDAO.Instance.Update(t);
        }
    }
}
