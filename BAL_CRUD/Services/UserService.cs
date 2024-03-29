﻿using BAL_CRUD.Interfaces;
using DAL_CRUD.Models;
using DAL_CRUD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_CRUD.Services
{
    public class UserService : IUserService
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
            var result = _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Save();
            return result;
        }

        public bool Update(User user)
        {
            try
            {

                _unitOfWork.UserRepository.Update(user.Id, user);
                _unitOfWork.Save();
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
                    _unitOfWork.Save();
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
                return null;
            }
        }
    }
}
