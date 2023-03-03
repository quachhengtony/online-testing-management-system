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
        public Task<List<Question>> GetAllByContent(string content);
    }
}
