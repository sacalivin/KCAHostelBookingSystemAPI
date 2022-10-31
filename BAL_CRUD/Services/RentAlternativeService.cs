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
    public class RentAlternativeService : IRentAlternativeService
    {
        private readonly UnitOfWork _unitOfWork;

        public RentAlternativeService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        //Get RentAlternative by RentAlternative Name  
        public RentAlternative? GetRentAlternativeByRentAlternativeName(string RentAlternativeName)
        {
            return _unitOfWork.RentAlternativeRepository.Get().Where(x => x.Name == RentAlternativeName).FirstOrDefault();
        }




        public RentAlternative Create(RentAlternative rentAlternative)
        {
            var result = _unitOfWork.RentAlternativeRepository.Insert(rentAlternative);
            _unitOfWork.Save();
            return result; 
        }

        public bool Update(RentAlternative rentAlternative)
        {
            try
            {

                _unitOfWork.RentAlternativeRepository.Update(rentAlternative.Id, rentAlternative);
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
                RentAlternative rentAlternative = _unitOfWork.RentAlternativeRepository.GetByID(id);
                if (rentAlternative != null)
                {
                    _unitOfWork.RentAlternativeRepository.Delete(id);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public RentAlternative? GetById(int id) => _unitOfWork.RentAlternativeRepository.Get().Where(x => x.Id == id).FirstOrDefault();

        public IEnumerable<RentAlternative> GetAll()
        {
            try
            {
                return _unitOfWork.RentAlternativeRepository.Get().ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
