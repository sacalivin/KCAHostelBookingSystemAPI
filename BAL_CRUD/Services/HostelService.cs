using BAL_CRUD.Interfaces;
using Bogus;
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
            var result = _unitOfWork.HostelRepository.Insert(hostel);
            _unitOfWork.Save();
            return result;

        }

        public bool Update(Hostel hostel)
        {
            try
            {

                //_unitOfWork.HostelRepository.Update(hostel);
                _unitOfWork.HostelRepository.Update(hostel.Id, hostel);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public User? GetUserByHostelId(int hostelId)
        {
            return _unitOfWork.UserRepository.Get().FirstOrDefault(u => u.HostelId == hostelId);
        }
        public bool Delete(int id)
        {
            try
            {
                Hostel hostel = _unitOfWork.HostelRepository.GetByID(id);
                if (hostel != null)
                {
                    
                    var user = GetUserByHostelId(id);
                    if(user != null)
                    {
                        _unitOfWork.UserRepository.Delete(user.Id);

                    }
                    _unitOfWork.HostelRepository.Delete(id);
                    _unitOfWork.Save();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Hostel? GetById(int id) => _unitOfWork.HostelRepository.Get().Where(x => x.Id == id).FirstOrDefault();

        public IEnumerable<Hostel> GetAll()
        {
            try
            {
                return _unitOfWork.HostelRepository.Get().ToList() ;
                /*var hostels = new Faker<Hostel>()

                //Basic rules using built-in generators
                .RuleFor(h => h.Id, f => f.Random.Number())
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Description, f => f.Name.LastName())
                .RuleFor(u => u.Location, f => f.Name.LastName())
                .RuleFor(u => u.ImageUrl, f => f.Internet.Avatar());

                 return hostels.Generate(20);
                */

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
