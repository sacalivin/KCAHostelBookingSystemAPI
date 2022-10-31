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
    public class ArmenityService:IDisposable, IAmenityService
    {
        private readonly UnitOfWork _unitOfWork;

        public ArmenityService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        //Get amenity by Amenity Name  
        public Armenity? GetArmenityByArmenityName(string ArmenityName)
        {
            
            return _unitOfWork.ArmenityRepository.Get().Where(x => x.Name == ArmenityName).FirstOrDefault();
        }




        public Armenity Create(Armenity armenity)
        {
           
               var result  = _unitOfWork.ArmenityRepository.Insert(armenity);
            _unitOfWork.Save();
            return result;
        }

        public bool Update(Armenity armenity)
        {
            try
            {

                _unitOfWork.ArmenityRepository.Update(armenity.Id,armenity);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Armenity armenity = _unitOfWork.ArmenityRepository.GetByID(id);
                if (armenity != null)
                {
                    _unitOfWork.ArmenityRepository.Delete(id);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Armenity? GetById(int id) => _unitOfWork.ArmenityRepository.Get().Where(x => x.Id == id).FirstOrDefault();

        public IEnumerable<Armenity> GetAll()
        {
            try
            {
                return _unitOfWork.ArmenityRepository.Get().ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Dispose()
        {
                _unitOfWork.Dispose();
            
        }
    }
}
