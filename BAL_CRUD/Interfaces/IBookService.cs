using DAL_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_CRUD.Interfaces
{
    public interface IBookService : IService<Book>
    {
        public Book? GetBookByDateBooked(DateTime bookingDate);
    }
}
