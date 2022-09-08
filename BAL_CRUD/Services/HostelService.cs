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
    public class HostelService : IHostelService
    {
        private readonly UnitOfWork _unitOfWork;

        public HostelService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        //Get Hostel by Hostel Name  
        public Hostel? GetHostelByHostelName(string HostelName)
        {
            return _unitOfWork.HostelRepository.Get().Where(x => x.Name == HostelName).FirstOrDefault();
        }




        public Hostel Create(Hostel hostel)
        {
            return _unitOfWork.HostelRepository.Insert(hostel);
        }

        public bool Update(Hostel hostel)
        {
            try
            {

                _unitOfWork.HostelRepository.Update(hostel);

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
                Hostel hostel = _unitOfWork.HostelRepository.GetByID(id);
                if (hostel != null)
                {
                    _unitOfWork.HostelRepository.Delete(id);

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Hostel? GetById(int id) => _unitOfWork.HostelRepository.Get().Where(x => x.Id == id).FirstOrDefault();

        public IEnumerable<Hostel> GetAll()
        {
            try
            {
                return _unitOfWork.HostelRepository.Get().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
