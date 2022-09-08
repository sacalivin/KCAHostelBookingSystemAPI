using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_CRUD.Interfaces
{
    public interface IService<TEntity>
    {
        public TEntity Create(TEntity entity);
        public bool Update(TEntity entity);
        public bool Delete(int id);
        public TEntity? GetById(int id);
        public IEnumerable<TEntity> GetAll();
    }
}
