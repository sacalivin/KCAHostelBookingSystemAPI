using DAL_CRUD.Models;
using kcauHosteslAdmin.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace kcauHosteslAdmin.Services
{
    public class RequestsService<T> : IRequestsService<T> where T : new()
    {
        private readonly string baseUrl = "https://localhost:7119/api/";
        private readonly string specificUrl = "";

        public RequestsService(string specificUrl)
        {
            this.specificUrl = specificUrl;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> books = new List<T>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                HttpResponseMessage getData = await client.GetAsync(specificUrl);

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<List<T>>(results);
                    return books;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<T> Get(int? id)
        {
            T books = new T();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync(specificUrl + "/" + id);

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<T>(results);
                    return books;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<bool> Add(T entity)
        {
            var hostel = entity as HostelFormViewModel;
            IFormFile image = null;
            if (hostel != null)
            {
                image = hostel.Image;
            }
            T books = new T();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

                var fileName = image.FileName;
                using var content = new MultipartFormDataContent();
                using var fileStream = image.OpenReadStream();

                
                //Hostel pHostel = new Hostel() { Id = hostel.Id, ImageUrl = hostel.ImageUrl, Name = hostel.Name, Description = hostel.Description, Location = hostel.Location };
                content.Add(new StringContent( JsonConvert.SerializeObject(new { Name = "dsfd",  Description = "fdsdfs", Location = "djlfjsla", ImageUrl = "kljljl" })));
                content.Add(new StreamContent(fileStream), "image", fileName);
               

                HttpResponseMessage getData = await client.PostAsync(specificUrl, content);

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<T>(results);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> Add(T entity, IFormFile image)
        {
            T books = new T();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                
                
                var fileName = image.FileName;
                using var content = new MultipartFormDataContent();
                using var fileStream = image.OpenReadStream() ;
                content.Add(new StreamContent(fileStream), "file");
                content.Add(new StringContent(JsonConvert.SerializeObject(entity)));



               

                HttpResponseMessage getData = await client.PostAsync( specificUrl, content);

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    books = JsonConvert.DeserializeObject<T>(results);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public async Task<bool> Update(int id, T entity)
        {
            T books = new T();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.PutAsJsonAsync<T>(specificUrl+ "/"  + id, entity);

                if (getData.IsSuccessStatusCode)
                {
                   // string results = getData.Content.ReadAsStringAsync().Result;
                    //books = JsonConvert.DeserializeObject<T>(results);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            
            T books = new T();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.DeleteAsync(specificUrl + "/" + id);

                if (getData.IsSuccessStatusCode)
                {
                   // string results = getData.Content.ReadAsStringAsync().Result;
                    //books = JsonConvert.DeserializeObject<T>(results);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

       
    }
}
