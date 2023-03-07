using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        public Answer GetById(Guid id);
        public Task<Answer> GetByIdAsync(Guid id);
        public Task<List<Answer>> GetAllByQuestionId(Guid questionId);
    }
}
