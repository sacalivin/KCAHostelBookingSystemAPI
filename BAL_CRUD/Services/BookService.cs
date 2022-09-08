using BAL_CRUD.Interfaces;
using DAL_CRUD.Models;
using DAL_CRUD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_CRUD.Services
{
    public class BookService: IBookService
    {
        private readonly UnitOfWork _unitOfWork;

        public BookService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        //Get Book by Book Name  
        public Book? GetBookByDateBooked(DateTime bookingDate)
        {
            return _unitOfWork.BookRepository.Get().Where(x => x.BookingDate == bookingDate).FirstOrDefault();
        }




        public Book Create(Book book)
        {
            return _unitOfWork.BookRepository.Insert(book);
        }

        public bool Update(Book book)
        {
            try
            {

                _unitOfWork.BookRepository.Update(book);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Book book = _unitOfWork.BookRepository.GetByID(id);
                if (book != null)
                {
                    _unitOfWork.BookRepository.Delete(id);

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Book? GetById(int id) => _unitOfWork.BookRepository.Get().Where(x => x.Id == id).FirstOrDefault();

        public IEnumerable<Book> GetAll()
        {
            try
            {
                return _unitOfWork.BookRepository.Get().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
