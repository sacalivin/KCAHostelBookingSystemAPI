using DAL_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_CRUD.Interfaces
{
    public interface IUserService : IService<User>
    {
        public User? GetUserByUserName(string UserName);
       
        }
}
