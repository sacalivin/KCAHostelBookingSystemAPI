using DAL_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_CRUD.Interfaces
{
    public interface IAmenityService : IService<Armenity>
    {
        public Armenity? GetArmenityByArmenityName(string ArmenityName);
    }
}
