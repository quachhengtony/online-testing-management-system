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

        public List<Submission> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Submission>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Submission GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Submission> GetByIdAsync(Guid id)
        {
            return SubmissionDAO.Instance.GetByIdAsync(id);
        }

        public Task<List<Submission>> GetByTestTakerIdAsync(Guid id)
        {
            return SubmissionDAO.Instance.GetByTestTakerIdAsync(id);
        }

        public void SaveChanges()
        {
            SubmissionDAO.Instance.SaveChanges();
        }

        public void Update(Submission t)
        {
            throw new NotImplementedException();
        }
    }
}
