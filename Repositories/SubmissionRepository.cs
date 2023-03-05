using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using DAO;
using Repositories.Interfaces;

namespace OnlineTestingManagementSystem.Repository
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly SubmissionDAO _submissionDAO;

        public SubmissionRepository()
        {
            _submissionDAO = SubmissionDAO.Instance;
        }

        public void Create(Submission submission)
        {
            _submissionDAO.Create(submission);
        }

        public void Update(Submission submission)
        {
            _submissionDAO.Update(submission);
        }

        public void Delete(String id)
        {
            _submissionDAO.Delete(GetById(id));
        }

        public Submission GetById(string id)
        {
            return _submissionDAO.GetById(id);
        }

        public List<Submission> GetAll()
        {
            return _submissionDAO.GetAll();
        }

        public List<Submission> GetByTestTakerId(string testTakerId)
        {
            return _submissionDAO.GetByTestTakerId(testTakerId);
        }

        public List<Submission> GetByTestId(string testId)
        {
            return _submissionDAO.GetByTestId(testId);
        }

        public List<Submission> GetByTestIdAndSubmittedDateRange(string testId, DateTime startDate, DateTime endDate)
        {
            return _submissionDAO.GetByTestIdAndSubmittedDateRange(testId, startDate, endDate);
        }

        public void SaveChanges()
        {
            _submissionDAO.SaveChanges();
        }

    }
}