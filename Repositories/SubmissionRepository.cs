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
    public class SubmissionRepository : ISubmissionRepository
    {
        public void Create(Submission t)
        {
            SubmissionDAO.Instance.Create(t);
            SaveChanges();
        }

        public void Delete(Submission t)
        {
            throw new NotImplementedException();
        }

        public Task<List<Submission>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Submission GetById(Guid id)
        {
            return SubmissionDAO.Instance.GetById(id);
        }

        public List<Submission> GetAll()
        {
            return SubmissionDAO.Instance.GetAll();
        }

        public List<Submission> GetByTestTakerId(Guid testTakerId)
        {
            return SubmissionDAO.Instance.GetByTestTakerId(testTakerId);
        }

        public List<Submission> GetByTestId(Guid testId)
        {
            return SubmissionDAO.Instance.GetByTestId(testId);
        }

        public List<Submission> GetByTestIdAndSubmittedDateRange(Guid testId, DateTime startDate, DateTime endDate)
        {
            return SubmissionDAO.Instance.GetByTestIdAndSubmittedDateRange(testId, startDate, endDate);
        }


        public Task<Submission> GetByIdAsync(Guid id)
        {
            return SubmissionDAO.Instance.GetByIdAsync(id);
        }

        public Task<List<Submission>> GetByTestTakerIdAndBatchAsync(Guid id, string batch)
        {
            return SubmissionDAO.Instance.GetByTestTakerIdAndBatchAsync(id, batch);
        }

        public Task<List<Submission>> GetByTestTakerIdAsync(Guid id)
        {
            return SubmissionDAO.Instance.GetByTestTakerIdAsync(id);
        }

        public void SaveChanges()
        {
            SubmissionDAO.Instance.SaveChanges();
        }

        public void Update(Submission submission)
        {
            SubmissionDAO.Instance.Update(submission);
        }

        public void Delete(Guid id)
        {
            SubmissionDAO.Instance.Delete(GetById(id));
        }
    }
}
