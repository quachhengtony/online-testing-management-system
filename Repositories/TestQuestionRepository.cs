using BusinessObjects.Models;
using DAO;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TestQuestionRepository : ITestQuestionRepository
    {
        public void Create(TestQuestion t)
        {
            TestQuestionDAO.Instance.Create(t);
        }

        public void Delete(TestQuestion t)
        {
            TestQuestionDAO.Instance.Delete(t);
        }

        public List<TestQuestion> GetAll()
        {
            return TestQuestionDAO.Instance.GetAll();
        }

        public Task<List<TestQuestion>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TestQuestion GetById(Guid id)
        {
            return TestQuestionDAO.Instance.GetById(id);    
        }

        public Task<TestQuestion> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TestQuestion> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }

        public TestQuestion GetByTestId(string testId)
        {
            return TestQuestionDAO.Instance.GetByTestId(testId);
        }

        public async Task<List<Guid>> GetAllTestsByQuestionId(Guid questionId)
        {
            List<Guid> testGuids = new();
            var testQuestions = await TestQuestionDAO.Instance.GetAllTestsByQuestionId(questionId);
            foreach (var test in testQuestions)
            {
                testGuids.Add(test.TestId);
            }
            return testGuids;
        }

        public void SaveChanges()
		{
            TestQuestionDAO.Instance.SaveChanges();
		}

		public void Update(TestQuestion t)
        {
            TestQuestionDAO.Instance.Update(t); 
        }
    }
}
