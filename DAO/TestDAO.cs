using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class TestDAO : IDAO<Test>
    {
        private static TestDAO instance;
        private static readonly object instaneLock = new object();
        private static OnlineTestingManagementSystemDbContext dbContext;

        public static TestDAO Instance
        {
            get
            {
                lock(instaneLock)
                {
                    if (instance == null)
                    {
                        instance = new TestDAO();
                        dbContext = new OnlineTestingManagementSystemDbContext();
                    }
                    return instance;
                }
            }
        }

        private TestDAO() { }

        public void Create(Test t)
        {
            dbContext.Tests.Add(t);
            SaveChanges();
        }

        public void Delete(Test t)
        {
            dbContext.Tests.Remove(t);
            SaveChanges();
        }

        public List<Test> GetAll()
        {
            return dbContext.Tests.ToList();
        }

        public Test GetById(Guid id)
        {
            return dbContext.Tests.Find(id);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Update(Test t)
        {
            dbContext.Tests.Update(t);
        }

        public Task<List<Test>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetByIdAsync(byte id)
        {
            throw new NotImplementedException();
        }
    }
}
