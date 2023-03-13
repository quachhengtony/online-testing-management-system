using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISubmissionRepository : IRepository<Submission>
    {
        Task<List<Submission>> GetByTestTakerIdAsync(Guid id);
        Task<Submission> GetByIdAsync(Guid id);
    }
}
