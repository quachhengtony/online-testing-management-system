using BusinessObjects.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public void Create(Question t)
        {
            throw new NotImplementedException();
        }

        public void Delete(Question t)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Question>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Question GetById(Guid id)
        {
            return QuestionDAO.Instance.GetById(id);
        }

        public void Update(Question t)
        {
            throw new NotImplementedException();
        }
    }
}
