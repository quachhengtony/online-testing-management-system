using BusinessObjects.Models;
using DAO;
using Microsoft.EntityFrameworkCore.Storage;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        public void Create(Answer t)
        {
            AnswerDAO.Instance.Create(t);
        }

        public void Delete(Answer t)
        {
            AnswerDAO.Instance.Delete(t);
        }

        public List<Answer> GetAll()
        {
            return AnswerDAO.Instance.GetAll();
        }

        public Task<List<Answer>> GetAllAsync()
        {
            return AnswerDAO.Instance.GetAllAsync();
        }

        public Answer GetById(Guid id)
        {
            return AnswerDAO.Instance.GetById(id);
        }

        public Task<Answer> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Answer> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }

        public Answer GetByQuestionId(Guid questionId)
        {
            return AnswerDAO.Instance.GetByQuestionId(questionId);
        }

        public Task<List<Answer>> GetAllByQuestionId(Guid questionId)
        {
            return AnswerDAO.Instance.GetAllByQuestionId(questionId);
        }

        public void Update(Answer t)
        {
            AnswerDAO.Instance.Update(t);
        }

		public void SaveChanges()
		{
            AnswerDAO.Instance.SaveChanges();
		}
	}
}
