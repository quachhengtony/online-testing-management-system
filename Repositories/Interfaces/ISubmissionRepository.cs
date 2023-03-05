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
        Submission GetById(string id);
        List<Submission> GetAll();
        List<Submission> GetByTestId(string testId);
        List<Submission> GetByTestTakerId(string testTakerId);
        List<Submission> GetByTestIdAndSubmittedDateRange(string testId, DateTime startDate, DateTime endDate);
        void Update(Submission submission);
        void Delete(string id);
        void SaveChanges();
    }
}
