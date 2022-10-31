using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace kcauHosteslAdmin.Services
{
    public interface IRequestsService<T> where T : new()
    {

        public  Task<bool> Add(T entity);

        

        public  Task<bool> Delete(int id);

        public  Task<T> Get(int? id);


        public  Task<IEnumerable<T>> GetAll();
       
        public  Task<bool> Update(int id, T entity);
       
    }
}