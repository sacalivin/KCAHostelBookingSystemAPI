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
    public class UserService : IService<User>
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Get User by User Name  
        public User? GetUserByUserName(string UserName)
        {
            return _unitOfWork.UserRepository.Get().Where(x => x.Email == UserName).FirstOrDefault();
        }
       
        public User Create(User user)
        {
            return _unitOfWork.UserRepository.Insert(user);
        }

        public bool Update(User user)
        {
            try
            {

                _unitOfWork.UserRepository.Update(user);

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
                User user = _unitOfWork.UserRepository.GetByID(id);
                if (user != null)
                {
                    _unitOfWork.UserRepository.Delete(id);

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User? GetById(int id) => _unitOfWork.UserRepository.Get().Where(x => x.Id == id).FirstOrDefault();

        public IEnumerable<User> GetAll()
        {
            try
            {
                return _unitOfWork.UserRepository.Get().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
