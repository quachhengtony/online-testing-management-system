using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IQuestionRepository : IRepository<Question>
    {
        //Task<List<Question>> GetByTestNameAndBatchAsync(string batch, string name);

    }
}
