using DAL_CRUD.Data;
using DAL_CRUD.Interfaces;
using DAL_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CRUD.Repositories
{
    public class HostelRepository : IRepository<Hostel>
    {
        readonly ApplicationDbContext _dbContext;
        public HostelRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<Hostel> Create(Hostel entity)
        {
            var obj = await _dbContext.Hostels.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return obj.Entity;

        }

        public async Task<bool> Delete(Hostel entity)
        {
            _dbContext.Remove(entity);
            var recordsAffected = await _dbContext.SaveChangesAsync();

            if (recordsAffected > 0)
            {
                return true;
            }

            return false;
        }



        public async Task<IEnumerable<Hostel>> GetAll()
        {
            try
            {
                return await Task.Run(() => (_dbContext.Hostels.Where(x => x.Id != 0).ToArray()));
            }
            catch (Exception e)
            {

                var x = e.Message;
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<Hostel> GetById(int id)
        {

            return await _dbContext.Hostels.FindAsync(id);
        }

        public async Task<bool> Update(Hostel entity)
        {
            _dbContext.Hostels.Update(entity);
            var recordsAffected = await _dbContext.SaveChangesAsync();

            return recordsAffected > 0 ? true : false;
        }
    }
}
