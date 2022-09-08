using DAL_CRUD.Data;
using DAL_CRUD.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CRUD.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<T> Create(T entity)
        {
            var obj = await _dbContext.AddAsync<T>(entity);
            await _dbContext.SaveChangesAsync();
            return obj.Entity;

        }

        public async Task<bool> Delete(T entity)
        {
            _dbContext.Remove(entity);
            var recordsAffected = await _dbContext.SaveChangesAsync();

            if (recordsAffected > 0)
            {
                return true;
            }

            return false;
        }



        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                
                //return await Task.Run(() => (_dbContext.).Where(x => x.Id != 0).ToArray()));
            }
            catch (Exception e)
            {

                var x = e.Message;
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<T> GetById(int id)
        {

            return await _dbContext.FindAsync<T>(id);
        }

        public async Task<bool> Update(T entity)
        {
            _dbContext.Update<T>(entity);
            var recordsAffected = await _dbContext.SaveChangesAsync();

            return recordsAffected > 0 ? true : false;
        }
    }
}
