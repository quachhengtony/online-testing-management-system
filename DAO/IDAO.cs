﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    interface IDAO<T>
    {
        public void Create(T t);
        public List<T> GetAll();
        public T GetById(string id);
        public void Update(T t);
        public void Delete(T t);
        public void SaveChanges();
    }
}
