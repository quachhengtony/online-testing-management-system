using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISubmissionRepository
    {
        void Create(Submission submission);
        Submission GetById(Guid id);
        List<Submission> GetAll();
        List<Submission> GetByTestId(Guid testId);
        List<Submission> GetByTestTakerId(Guid testTakerId);
        List<Submission> GetByTestIdAndSubmittedDateRange(Guid testId, DateTime startDate, DateTime endDate);
        void Update(Submission submission);
        void Delete(Guid id);
        void SaveChanges();
    }
}
