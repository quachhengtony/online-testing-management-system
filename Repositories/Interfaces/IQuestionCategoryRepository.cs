﻿using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IQuestionCategoryRepository : IRepository<QuestionCategory>
    {
        public Task<QuestionCategory> GetByIdAsync(byte id);
    }
}
