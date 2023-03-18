
﻿using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
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
        private static readonly object instanceLock = new object();
        private static BusinessObjects.DbContexts.OnlineTestingManagementSystemDbContext dbContext;

        public static SubmissionDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SubmissionDAO();
                        dbContext = new BusinessObjects.DbContexts.OnlineTestingManagementSystemDbContext();
                    }
                    return instance;
                }
            }
        }

        private SubmissionDAO() { }

        public void Create(Submission t)
        {
            dbContext.Submissions.Add(t);
            SaveChanges();
        }

        public void Delete(Submission t)
        {
            dbContext.Submissions.Remove(t);
            SaveChanges();
        }

        public List<Submission> GetAll()
        {
            return dbContext.Submissions.ToList();
        }
        
        public List<Submission> GetByTestId(Guid testId)
        {
            return dbContext.Submissions.Where(s => s.TestId == testId).Include(t => t.TestTaker).Include(t => t.Test).ToList();
        }

        public List<Submission> GetByTestIdAndSubmittedDateRange(Guid testId, DateTime startDate, DateTime endDate)
        {
            return dbContext.Submissions
                .Where(s => s.TestId == testId && s.SubmittedDate >= startDate && s.SubmittedDate <= endDate)
                .ToList();
        }
        
        public Task<List<Submission>> GetAllAsync()
        {
            return dbContext.Submissions.ToListAsync();
        }

        public Task<List<Submission>> GetSubmissionByTestTaker(Guid testTakerId)
        {
            return dbContext.Submissions.Where(s => s.TestTakerId == testTakerId).ToListAsync();
        }

        public Submission GetById(Guid id)
        {
            return dbContext.Submissions.Where(s => s.Id == id).Include(s => s.TestTaker).Include(s => s.Test).FirstOrDefault();
        }

        public Task<Submission> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }

        public Task<Submission> GetByIdAsync(Guid id)
        {
            return dbContext.Submissions.Include(s => s.Test).Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Submission>> GetByTestTakerIdAsync(Guid id)
        {
            return dbContext.Submissions.Where(s => s.TestTakerId == id).Include(t => t.Test).ToListAsync();
        }

        public Task<List<Submission>> GetByTestTakerIdAndBatchAsync(Guid id, string batch)
        {
            return dbContext.Submissions.Include(t => t.Test).Where(s => s.TestTakerId == id && s.Test.Batch.Contains(batch)).ToListAsync();
        }

        public Task<List<Submission>> GetAllByBatchForTestCreatorAsync(string batch)
        {
            return dbContext.Submissions.Include(t => t.Test).Include(t => t.TestTaker).Where(s => s.Test.Batch == batch).ToListAsync();
        }


        public bool IsBatchTakenByTestTaker(Guid testTakerId, string batch)
        {
            return dbContext.Submissions.Include(s => s.Test)
                .Where(s => s.TestTakerId == testTakerId && s.Test.Batch == batch).Count() > 0;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Submission t)
        {
            dbContext.Submissions.Update(t);
            SaveChanges();
        }

        public List<Submission> GetByTestTakerId(Guid testTakerId)
        {
            return dbContext.Submissions.Where(s => s.TestTakerId == testTakerId).ToList();
        }

        public decimal GetAverageScoreByBatch(string batch)
        {
            if (dbContext.Submissions.Include(s => s.Test).Where(s => s.Test.Batch == batch).Count() == 0)
            {
                return 0m;
            }
            return dbContext.Submissions.Include(s => s.Test).Where(s => s.Test.Batch == batch).Average(s => s.Score);
        }

    }
}
