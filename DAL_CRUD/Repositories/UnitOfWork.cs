using DAL_CRUD.Data;
using DAL_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CRUD.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        private GenericRepository<RentAlternative> rentAlternativeRepository;
        public GenericRepository<RentAlternative> RentAlternativeRepository
        {
            get
            {
                return this.rentAlternativeRepository ?? new GenericRepository<RentAlternative>(_context);
            }
        }
        private GenericRepository<User> userRepository;
        public GenericRepository<User> UserRepository
        {
            get
            {
                return this.UserRepository ?? new GenericRepository<User>(_context);
            }
        }
        private GenericRepository<Hostel> hostelRepository;
        public GenericRepository<Hostel> HostelRepository
        {
            get
            {
                return this.hostelRepository ?? new GenericRepository<Hostel>(_context);
            }
        }
        private GenericRepository<Armenity> armenityRepository;
        public GenericRepository<Armenity> ArmenityRepository
        {
            get
            {
                return this.armenityRepository ?? new GenericRepository<Armenity>(_context);
            }
        }

        private GenericRepository<Book> bookRepository;
        public GenericRepository<Book> BookRepository
        {
            get
            {
                return this.bookRepository ?? new GenericRepository<Book>(_context);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
