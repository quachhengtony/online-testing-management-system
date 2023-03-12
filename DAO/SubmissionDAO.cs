using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class SubmissionDAO : IDAO<Submission>
    {
        private static SubmissionDAO instance;
        private static readonly object instaneLock = new object();
        private static  OnlineTestingManagementSystemDbContext _context;

        public SubmissionDAO()
        {
        }

        public static SubmissionDAO Instance
        {
            get
            {
                lock (instaneLock)
                {
                    if (instance == null)
                    {
                        instance = new SubmissionDAO();
                        _context = new OnlineTestingManagementSystemDbContext();
                    }
                    return instance;
                }
            }
        }

        public void Create(Submission submission)
        {
            _context.Submissions.Add(submission);
        }

        public List<Submission> GetAll()
        {
            return _context.Submissions.ToList();
        }

        public Submission GetById(Guid id)
        {
            return _context.Submissions.FirstOrDefault(s => s.Id == id);
        }

        public void Update(Submission submission)
        {
            var submissionToUpdate = _context.Submissions.FirstOrDefault(s => s.Id == submission.Id);
            if (submissionToUpdate != null)
            {
                submissionToUpdate.Content = submission.Content;
                submissionToUpdate.Feedback = submission.Feedback;
                submissionToUpdate.Score = submission.Score;
                submissionToUpdate.GradedDate = submission.GradedDate;
            }
        }

        public void Delete(Submission submission)
        {
            _context.Submissions.Remove(submission);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public List<Submission> GetByTestTakerId(Guid testTakerId)
        {
            return _context.Submissions.Where(s => s.TestTakerId == testTakerId).ToList();
        }

        public List<Submission> GetByTestId(Guid testId)
        {
            return _context.Submissions.Where(s => s.TestId == testId).ToList();
        }

        public List<Submission> GetByTestIdAndSubmittedDateRange(Guid testId, DateTime startDate, DateTime endDate)
        {
            return _context.Submissions
                .Where(s => s.TestId == testId && s.SubmittedDate >= startDate && s.SubmittedDate <= endDate)
                .ToList();
        }

        public Task<List<Submission>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Submission> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
