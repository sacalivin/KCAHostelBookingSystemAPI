using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CRUD.Interfaces
{
    internal interface IRepository<T>
    {
        public Task<T> Create(T entity);
        public Task<bool> Update(T entity);
        public Task<bool> Delete(T entity);
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetAll();

    }
}
