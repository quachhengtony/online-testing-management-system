using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        public Question GetById(Guid id);
        public Task<Question> GetByIdAsync(Guid id);
        public Task<List<Question>> GetAllAsync(Guid questionCreatorId);
        public Task<List<Question>> GetAllByContent(string content);
		public Task<List<Question>> GetAllByContentAsync(string content, Guid questionCreatorId);

	}
}
